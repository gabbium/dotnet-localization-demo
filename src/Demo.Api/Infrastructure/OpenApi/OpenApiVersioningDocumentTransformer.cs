namespace Demo.Api.Infrastructure.OpenApi;

public class OpenApiVersioningDocumentTransformer(
    IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var apiDescription = apiVersionDescriptionProvider.ApiVersionDescriptions
            .SingleOrDefault(description => description.GroupName == context.DocumentName);

        if (apiDescription == null)
        {
            return Task.CompletedTask;
        }

        document.Info.Version = apiDescription.ApiVersion.ToString();
        document.Info.Title = ".NET Localization Demo API";
        document.Info.Description = "Sample ASP.NET Core API demonstrating localization with resource files and IStringLocalizer.";

        return Task.CompletedTask;
    }
}
