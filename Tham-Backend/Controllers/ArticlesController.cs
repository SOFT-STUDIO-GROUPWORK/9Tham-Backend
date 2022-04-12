using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;

namespace Tham_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Article>>> Get()
    {
        var articles = new List<Article>
        {
            new()
            {
                Id = 123456, Title = "สาหวาดดีค้าบ ท่านสมาชิก", Content = "จัดมาดิ๊!", Visible = true,
                BloggerId = 987654
            },
            new()
            {
                Id = 123457, Title = "แล้วผมจะไปแคร์ เหี้ยอะไร", Content = "จัดมาดิ๊!", Visible = true,
                BloggerId = 987654
            }
        };

        return Ok(articles);
    }
}