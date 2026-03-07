using System.Text.Json.Nodes;

namespace Demo.Api.Infrastructure.OpenApi;

public class LocalizationHeaderTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        foreach (var path in document.Paths.Values)
        {
            if (path.Operations is null)
            {
                continue;
            }

            foreach (var operation in path.Operations.Values)
            {
                operation.Parameters ??= [];

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Accept-Language",
                    In = ParameterLocation.Header,
                    Required = false,
                    Description = "Request culture",
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Enum =
                        [
                            JsonValue.Create("en-US"),
                            JsonValue.Create("pt-BR"),
                            JsonValue.Create("es-ES")
                        ],
                        Default = JsonValue.Create("en-US")
                    }
                });
            }
        }

        return Task.CompletedTask;
    }
}
