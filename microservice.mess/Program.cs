using MongoDB.Driver;
using FirebaseAdmin;
using microservice.mess.Configurations;
using microservice.mess.Repositories;
using microservice.mess.Interfaces;
using microservice.mess.Hubs;
using microservice.mess.Hubs.Handler;
using Microsoft.OpenApi.Models;
using microservice.mess.Services;
using microservice.mess.Cronjobs;
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

builder.Services.AddHostedService<ScheduledPromotionBackgroundService>();


// Cấu hình các dịch vụ khác
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("STMP"));
builder.Services.Configure<ZaloSettings>(builder.Configuration.GetSection("ZALO"));

builder.Services.AddScoped<TokenRepository>();
builder.Services.AddScoped<ScheduledPromotionSender>();

builder.Services.AddScoped<ZaloEventRepository>();
builder.Services.AddScoped<GroupMemberRepository>();
builder.Services.AddScoped<ZaloPromotionRepository>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<ZaloService>();
builder.Services.AddHttpClient<SlackService>();
builder.Services.AddScoped<SignalRService>();
builder.Services.AddScoped<KafkaProducerService>();
builder.Services.AddScoped<INotificationChannelHandler, MailChannelHandler>();

//kafka
builder.Services.AddHostedService<ZaloKafkaConsumerHostedService>();
builder.Services.AddHostedService<MailKafkaConsumerHostedService>();

//http client
builder.Services.AddHttpClient();
builder.Services.AddControllers();
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
