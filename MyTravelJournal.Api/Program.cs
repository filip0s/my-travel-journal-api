var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// CORS Policy setup
var AllowedOriginPolicy = "_MyLocalhostPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: AllowedOriginPolicy,
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        }
    );
});


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.UseCors(AllowedOriginPolicy);

app.Run();