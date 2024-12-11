using ContactsManagementApi.Application.Interfaces;
using ContactsManagementApi.Application.Services;
using ContactsManagementApi.Domain.Interfaces;
using ContactsManagementApi.Infrastructure.Repositories;
using ContactsManagementApi.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

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
