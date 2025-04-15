using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using backend.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() 
                   .AllowAnyMethod() 
                   .AllowAnyHeader(); 
        });
});
//

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // string

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


// CORS only in development mode
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAllOrigins");
}



if(!app.Environment.IsDevelopment()){
    app.UseHttpsRedirection();
}


app.MapControllers();

app.Run();