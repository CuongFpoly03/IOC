using iocXG.Applications.Interfaces;
using iocXG.Applications.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IOpreration, OperationInput>();
builder.Services.AddScoped<OperationMath>();
builder.Services.AddSingleton<OperationInput>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Configure error handling for non-development environments
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// Configure endpoints
app.UseEndpoints(endpoints =>
{
    endpoints?.MapControllers();
});

app.Run();
