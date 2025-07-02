using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TechFood.Api;
using TechFood.Application;
using TechFood.Infra;
using TechFood.Infra.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfra();
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

    app.UseInfra();

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
