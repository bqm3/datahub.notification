using Minio;
using Minio.DataModel.Args;
using Minio.DataModel;
using Minio.Exceptions;

namespace microservice.mess.Services.Storage;

public interface IStorageService
{
    Task<string> UploadPdfAsync(string localFilePath, string bucketName, string objectName);
}

public class MinioStorageService : IStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioStorageService> _logger;

    public MinioStorageService(IMinioClient minioClient, ILogger<MinioStorageService> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<string> UploadPdfAsync(string filePath, string bucketName, string objectName)
{
    try
    {
        var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!bucketExists)
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType("application/pdf")); 

        var url = $"http://localhost:9000/{bucketName}/{objectName}";
        _logger.LogInformation($"Uploaded PDF to MinIO: {url}");
        return url;
    }
    catch (MinioException ex)
    {
        _logger.LogError(ex, "Failed to upload PDF to MinIO");
        throw;
    }
}

}
