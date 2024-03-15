using Microsoft.AspNetCore.Mvc;

namespace MiniMart.Application.DTOs
{
    public class RequestModel
    {
        public int Draw { get; set; }
        [BindProperty(Name = "length")]
        public int PageSize { get; set; }
        [BindProperty(Name = "start")]
        public int SkipIndex { get; set; }
        [BindProperty(Name = "search[value]")]
        public string? Keyword { get; set; }
    }
}
