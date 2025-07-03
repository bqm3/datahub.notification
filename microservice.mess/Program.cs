using MongoDB.Driver;
using FirebaseAdmin;
using microservice.mess.Configurations;
using microservice.mess.Repositories;
using microservice.mess.Interfaces;
using microservice.mess.Hubs;
using Microsoft.OpenApi.Models;
using microservice.mess.Services;
using microservice.mess.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Load config từ global.setting.json
builder.Configuration.AddJsonFile("config/global/global.setting.json", optional: false, reloadOnChange: true);

// MongoDB setup
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connStr = builder.Configuration["MongoSettings:ConnectionString"];
    return new MongoClient(connStr);
});
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

// Cấu hình các dịch vụ khác
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("STMP"));
builder.Services.Configure<ZaloSettings>(builder.Configuration.GetSection("ZALO"));

builder.Services.AddScoped<ZaloTokenRepository>();
builder.Services.AddScoped<ZaloEventRepository>();
builder.Services.AddScoped<LogMessageRepository>();
builder.Services.AddScoped<ZaloMemberRepository>();
builder.Services.AddScoped<ZaloPromotionRepository>();
builder.Services.AddSingleton<ScheduledEmailRepository>();

builder.Services.AddScoped<MailRepository>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<ZaloService>();
builder.Services.AddScoped<SignalRService>();
builder.Services.AddScoped<KafkaProducerService>();
builder.Services.AddScoped<SlackService>();

builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();
builder.Services.AddSingleton<ILogMessageRepository, LogMessageRepository>();

//kafka
builder.Services.AddHostedService<ZaloKafkaConsumerHostedService>();
builder.Services.AddHostedService<MailKafkaConsumerHostedService>();
// scheduler mail
builder.Services.AddHostedService<MailSchedulerService>();
//http client
builder.Services.AddHttpClient();
builder.Services.AddControllers().AddNewtonsoftJson();;
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();

//  Cấu hình Session
builder.Services.AddDistributedMemoryCache(); // Bộ nhớ cache nội bộ

//  Thêm dòng này để tránh lỗi DataProtection
builder.Services.AddDataProtection();

//  Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification API", Version = "v1" });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5237); // HTTP
    serverOptions.ListenAnyIP(7225, listenOptions => listenOptions.UseHttps()); // HTTPS
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "https://5225-101-99-6-230.ngrok-free.app", 
            "https://localhost:7225"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); 
    });
});
builder.Configuration.AddJsonFile("config/global/global.setting.json", optional: false, reloadOnChange: true);

// builder.Services.AddSingleton(provider =>
//     {
//         return FirebaseApp.Create(new AppOptions()
//         {
//             Credential = GoogleCredential.FromFile("firebase-setting.json"),
//             ProjectId = "native-noti",
//         });
//     });


var app = builder.Build();

// Swagger trong môi trường dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification API V1");
    });
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

// Nếu là SPA frontend (React/Angular)
app.MapFallbackToFile("index.html");

app.Run();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("App started on: " + string.Join(", ", builder.Configuration["urls"]));
});
