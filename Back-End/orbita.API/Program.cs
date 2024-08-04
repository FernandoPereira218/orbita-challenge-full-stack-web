using Microsoft.EntityFrameworkCore;
using orbita.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("orbita");
builder.Services.AddDbContext<APIDbContext>(x => x.UseMySql(connectionString, ServerVersion.Parse("8.0.32")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(x => x.AddPolicy(name: "corsPolicy", 
    policy => { policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod(); 
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseCors("corsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
