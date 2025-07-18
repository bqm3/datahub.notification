using MongoDB.Driver;
using FirebaseAdmin;
using Minio;
using QuestPDF.Infrastructure;
using Aspose.Cells;
using Aspose.Words;
using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Options;
// using Serilog;
// using Serilog.Sinks.Elasticsearch;
// using Serilog.Formatting.Elasticsearch;
// using Serilog.Enrichers;
using microservice.mess.Configurations;
using microservice.mess.Repositories;
using microservice.mess.Schedules;
using microservice.mess.Interfaces;
using microservice.mess.Kafka.Consumer;
using microservice.mess.Hubs;
using Microsoft.OpenApi.Models;
using microservice.mess.Filters;
using microservice.mess.Models;
using microservice.mess.Services;
using microservice.mess.Services.Storage;
using microservice.mess.Kafka;
using microservice.mess.Documents;

var builder = WebApplication.CreateBuilder(args);

var licensePath = Path.Combine(AppContext.BaseDirectory, "#License#", "License.lic");

var cellsLicense = new Aspose.Cells.License();
cellsLicense.SetLicense(licensePath);

var wordsLicense = new Aspose.Words.License();
wordsLicense.SetLicense(licensePath);

try
{
    var doc = new Aspose.Words.Document();
    var workbook = new Aspose.Cells.Workbook();
    Console.WriteLine("Aspose.Words and Aspose.Cells license likely OK.");
}
catch (Exception ex)
{
    Console.WriteLine("Aspose.Words license NOT loaded.");
}


// Load config từ global.setting.json
builder.Configuration.AddJsonFile("config/global/global.setting.json", optional: false, reloadOnChange: true);

// MongoDB setup
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connStr = builder.Configuration["MongoSettings:ConnectionString"];
    return new MongoClient(connStr);
});
builder.Services.AddSingleton<IMongoClientFactory, MongoClientFactory>();

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.ZaloDatabase); 
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
builder.Services.AddSingleton<ScheduledAllRepository>();
builder.Services.AddSingleton<ScheduleQueryRepository>();

builder.Services.AddScoped<MailRepository>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<SignetService>();
builder.Services.AddScoped<ZaloService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<SignalRService>();
builder.Services.AddScoped<KafkaProducerService>();
builder.Services.AddScoped<SlackService>();

// documents
builder.Services.AddScoped<SgiPdfChart>();
// builder.Services.AddScoped<ChartMapper>();


builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();
builder.Services.AddScoped<IStorageService, MinioStorageService>();
builder.Services.AddSingleton<ILogMessage, LogMessageRepository>();

//kafka
builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();

// scheduler 
builder.Services.AddHostedService<MailSchedulerService>();
builder.Services.AddHostedService<AllSchedulerService>();
builder.Services.AddScoped<StepRunnerService>();

builder.Services.AddTransient<IMessageStep, QueryDataStep>();
builder.Services.AddTransient<IMessageStep, FormatDataSignetStep>();
builder.Services.AddTransient<IMessageStep, FormatDataMailStep>();
builder.Services.AddTransient<IMessageStep, GeneratePdfStep>();
// builder.Services.AddTransient<IMessageStep, UploadSignetStep>();
builder.Services.AddTransient<IMessageStep, GenerateHashStep>();
builder.Services.AddTransient<IMessageStep, SendToSignetStep>();
builder.Services.AddTransient<IMessageStep, SendToMailStep>();

await EnsureKafkaTopic.EnsureKafkaTopicsAsync("host.docker.internal:9092", new[] {
    "topic-mail", "topic-zalo", "topic-signet"
});
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

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("App started.");
});

app.Run();
