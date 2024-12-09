using JuiceTip_API;
using JuiceTip_API.Helper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

JuiceTipDBContext dbContextSingleton = null;

builder.Services.AddSingleton(provider =>
{
    if (dbContextSingleton == null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<JuiceTipDBContext>();
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
        dbContextSingleton = new JuiceTipDBContext(optionsBuilder.Options);
    }

    return dbContextSingleton;
});

builder.Services.AddDbContext<JuiceTipDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);

builder.Services.AddScoped<UserHelper>();
builder.Services.AddScoped<RegionHelper>();
builder.Services.AddScoped<ProductHelper>();
builder.Services.AddScoped<CategoryHelper>();
builder.Services.AddScoped<RatingHelper>();
builder.Services.AddScoped<TransactionDetailHelper>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

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

app.UseCors();

app.MapControllers();

app.Run();
