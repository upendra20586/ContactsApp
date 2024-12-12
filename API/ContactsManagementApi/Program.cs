using ContactsManagementApi.Application.Interfaces;
using ContactsManagementApi.Application.Services;
using ContactsManagementApi.Domain.Interfaces;
using ContactsManagementApi.Infrastructure.Repositories;
using ContactsManagementApi.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:4200")  // Allow requests from Angular app
               .AllowAnyMethod()                     // Allow any HTTP method
               .AllowAnyHeader();                    // Allow any header
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.UseCors("AllowLocalhost");
// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
