using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TechFood.Api;
using TechFood.Application;
using TechFood.Infra.Data;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.ImageStore.LocalDisk.Configuration;
using TechFood.Infra.Services.MercadoPago;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfraData();
    builder.Services.AddInfraMercadoPagoPayment();
    builder.Services.AddInfraImageStore();
}

var app = builder.Build();
{
    //Run migrations
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<TechFoodContext>();
        dataContext.Database.Migrate();
    }

    app.UseForwardedHeaders();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
        app.UseHttpsRedirection();
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

    app.UseApplication();

    app.UseHealthChecks("/health");

    app.UseStaticFiles(new StaticFileOptions
    {
        RequestPath = app.Configuration["TechFoodStaticImagesUrl"],
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
    });

    app.UseRouting();

    app.UseCors();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
