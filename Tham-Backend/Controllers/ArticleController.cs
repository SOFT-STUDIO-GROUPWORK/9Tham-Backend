using Microsoft.AspNetCore.Mvc;
using Tham_Backend.Models;

namespace Tham_Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private static readonly List<Article> Articles = new()
    {
        new Article
        {
            Id = 123456, Title = "สาหวาดดีค้าบ ท่านสมาชิก", Content = "จัดมาดิ๊!", Visible = true,
            BloggerId = 987654
        },
        new Article
        {
            Id = 123457, Title = "แล้วผมจะไปแคร์ เหี้ยอะไร", Content = "ออกไป๊!", Visible = true,
            BloggerId = 987654
        }
    };

    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticle()
    {
        return Ok(Articles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = Articles.Find(article => article.Id == id);
        if (article == null) return BadRequest("Article not found!");
        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<List<Article>>> AddArticle([FromBody] Article article)
    {
        Articles.Add(article);
        return Ok(Articles);
    }

    [HttpPut]
    public async Task<ActionResult<List<Article>>> UpdateArticle([FromBody] Article request)
    {
        var article = Articles.Find(article => article.Id == request.Id);
        if (article == null) return BadRequest("Article not found!");
        article.Content = request.Content;
        article.Title = request.Title;
        article.Visible = request.Visible;
        article.BloggerId = request.BloggerId;
        return Ok(article);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Article>> DeleteArticle(int id)
    {
        var article = Articles.Find(article => article.Id == id);
        if (article == null) return BadRequest("Article not found!");
        Articles.Remove(article);
        return Ok(article);
    }
}