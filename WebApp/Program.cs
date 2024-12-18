using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Service;
using Infrastructure.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGenericService<User>, UserService>();
builder.Services.AddScoped<IGenericService<Skill>, SkillService>();
builder.Services.AddScoped<IGenericService<Request>, RequestService>();
builder.Services.AddScoped<IContext, DapperContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


