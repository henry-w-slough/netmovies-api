using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Adding services to container
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//adding GlobalExceptionHandler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<MovieDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();       
    app.UseSwagger();       
    app.UseSwaggerUI();   
}

//using global exception handler
app.UseExceptionHandler(); 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
