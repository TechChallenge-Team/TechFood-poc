using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TechFood.Application;
using TechFood.Application.Common.Filters;
using TechFood.Application.Common.NamingPolicy;
using TechFood.Infra.Data;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Services.MercadoPago;
using TechFood.Infra.ImageStore.LocalDisk.Configuration;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();
        options.Filters.Add<ModelStateFilter>();
    })

    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })

    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new UpperCaseNamingPolicy()));
        options.JsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());
    });

    builder.Services.AddCors();

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "TechFood API V1",
            Version = "v1",
            Description = "TechFood API V1",
        });
    });

    builder.Services.AddApplication();
    builder.Services.AddInfraData();
    builder.Services.AddInfraMercadoPagoPayment();
    builder.Services.AddInfraImageStore();
}

var app = builder.Build();

//Run migrations
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<TechFoodContext>();
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(options =>
    {
        options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = app.Configuration.GetSection("TechFoodStaticImagesUrl").Value,
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

