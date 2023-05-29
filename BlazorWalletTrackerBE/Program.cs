using BlazorWalletTrackerBL;
using BlazorWalletTrackerBL.Services;
using BlazorWalletTrackerBL.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
ConfigBL.ConfigureServices(builder.Services, builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "BlazorCors",
        policy =>
        {
            policy.WithOrigins("https://localhost:7259")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("BlazorCors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
