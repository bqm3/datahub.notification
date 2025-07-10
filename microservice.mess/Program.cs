using MongoDB.Driver;
using FirebaseAdmin;
using Minio;
using QuestPDF.Infrastructure;
// using Serilog;
// using Serilog.Sinks.Elasticsearch;
// using Serilog.Formatting.Elasticsearch;
// using Serilog.Enrichers;
using microservice.mess.Configurations;
using microservice.mess.Repositories;
using microservice.mess.Interfaces;
using microservice.mess.Kafka.Consumer;
using microservice.mess.Hubs;
using Microsoft.OpenApi.Models;
using microservice.mess.Filters;
using microservice.mess.Models;
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

builder.Services.AddSingleton<IMinioClient>(sp =>
    new MinioClient()
        .WithEndpoint("localhost:9000")
        .WithCredentials("minio", "minio123")
        .WithSSL(false)
        .Build()
);

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

// Cấu hình các dịch vụ khác
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("STMP"));
builder.Services.Configure<ZaloSettings>(builder.Configuration.GetSection("ZALO"));

builder.Services.AddScoped<SignetRepository>();
builder.Services.AddScoped<ZaloRepository>();
builder.Services.AddScoped<LogMessageRepository>();
builder.Services.AddSingleton<ScheduledEmailRepository>();

builder.Services.AddScoped<MailRepository>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<SignetService>();
builder.Services.AddScoped<ZaloService>();
builder.Services.AddScoped<SignalRService>();
builder.Services.AddScoped<KafkaProducerService>();
builder.Services.AddScoped<SlackService>();

builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();
builder.Services.AddSingleton<ILogMessage, LogMessageRepository>();

//kafka
builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
// scheduler mail
builder.Services.AddHostedService<MailSchedulerService>();
//http client
builder.Services.AddHttpClient();
builder.Services.AddControllers().AddNewtonsoftJson(); ;
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
    options.OperationFilter<FileUploadOperationFilter>();
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5237);
    serverOptions.ListenAnyIP(7225, listenOptions => listenOptions.UseHttps());
    serverOptions.ListenAnyIP(7226);
});
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "https://localhost:7225",
            "http://localhost:3000",
            "http://localhost:7226",
            "http://host.docker.internal:7226",
            "https://54897ee56612.ngrok-free.app",
            "http://192.168.1.77:7226"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 100_000_000; // 100 MB
});

// logger
// Log.Logger = new LoggerConfiguration()
//     .Enrich.FromLogContext()
//     .Enrich.WithMachineName()
//     .Enrich.WithThreadId()
//     .Enrich.WithProperty("service.name", "microservice-notify") // ECS field
//     .WriteTo.Console(new ElasticsearchJsonFormatter()) // log ra console dạng ECS
//     .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
//     {
//         IndexFormat = "microservice-notify-logs-{0:yyyy.MM.dd}",
//         AutoRegisterTemplate = true,
//         CustomFormatter = new ElasticsearchJsonFormatter()
//     })
//     .CreateLogger();

// builder.Host.UseSerilog();

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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification API V1");
    });
}
app.UseCors("AllowReactApp");
app.UseSession();
// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
// app.UseSerilogRequestLogging();
// Nếu là SPA frontend 
app.MapFallbackToFile("index.html");

// var hierarchy = (Hierarchy)LogManager.CreateRepository(Guid.NewGuid().ToString());
// var appender = new ConsoleAppender { Layout = new EcsLayout() };
// hierarchy.Root.AddAppender(appender);
// hierarchy.Root.Level = Level.All;
// hierarchy.Configured = true;

app.Run();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("App started on: " + string.Join(", ", builder.Configuration["urls"]));
});
