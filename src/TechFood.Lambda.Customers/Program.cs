using TechFood.Application;
using TechFood.Common.Extensions;
using TechFood.Infra;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration, new PresentationOptions
    {
        AddSwagger = true,
        SwaggerTitle = "TechFood Lambda Customers API V1",
        SwaggerDescription = "TechFood Lambda Customers API V1",
    });

    builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

    builder.Services.AddApplication();
    builder.Services.AddInfra();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
