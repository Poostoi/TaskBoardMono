using Microsoft.AspNetCore.Http;

namespace Services.Request.Board;

public class FileRequest
{
    public string Name { get; set; }
    public IFormFile DataImage { get; set; }
}