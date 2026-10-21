using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController(IAmazonS3 s3Client) : ControllerBase
{
    // Заменяш с името на твоя bucket, който създадохме в предната стъпка
    private readonly string _bucketName = "orders-documents-storage-vasilius";

    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Моля, изберете файл за качване.");
        }

        // Генерираме уникално име на файла в облака, за да няма презаписване
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";

        await using var stream = file.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = file.ContentType
        };

        var response = await s3Client.PutObjectAsync(putRequest);

        return Ok(new 
        { 
            Message = "Файлът е качен успешно в S3!", 
            FileKey = fileName,
            HttpStatusCode = response.HttpStatusCode 
        });
    }
}