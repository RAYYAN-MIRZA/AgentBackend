using AgentBackend;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200")  // Angular dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// ✅ Must come between UseRouting and MapHub
app.UseCors("AllowAngular");

app.UseAuthorization();

// ✅ Enable WebSockets before mapping hubs
app.UseWebSockets();

// ✅ Map hub and controllers
app.MapControllers();
app.MapHub<AgentHub>("/agenthub");  // make sure path matches frontend

app.Run();
