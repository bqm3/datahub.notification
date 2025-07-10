using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ApiResponse<T>
{
    public int Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = default!;
    public DateTime Time { get; set; } = DateTime.UtcNow;
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Thành công", int status = 200)
        => new ApiResponse<T> { Status = status, Success = true, Message = message, Data = data };

    public static ApiResponse<T> ErrorResponse(string message, int status = 400)
        => new ApiResponse<T> { Status = status, Success = false, Message = message, Data = default };
}
