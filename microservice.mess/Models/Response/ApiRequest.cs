using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class UploadFileDto
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; } = default!;
}
