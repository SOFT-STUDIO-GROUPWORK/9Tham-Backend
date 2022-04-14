using Tham_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Tham_Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add data context.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddTransient<IBloggerRepository, BloggerRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(optBuilder =>
    {
        optBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();