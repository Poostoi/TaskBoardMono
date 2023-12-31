﻿using Microsoft.AspNetCore.Http;

namespace Services.Request.Board;

public class SprintUpdateRequest
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Comment { get; set; }
    public List<IFormFile> Files { get; set; }
}