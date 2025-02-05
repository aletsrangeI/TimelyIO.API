// using NSwag;
// using NSwag.Generation.Processors.Security;
//
// namespace TimelyIO.Service.WebApi.Modules.Swagger;
//
// public static class SwaggerExtensions
// {
//     public static IServiceCollection AddSwagger(this IServiceCollection services)
//     {
//         services.AddOpenApiDocument(config =>
//         {
//             config.DocumentName = "v1";
//             config.Title = "WebApi for TimelyIO web app";
//             config.Version = "v1";
//             config.Description = "TimelyIO web app API";
//
//             // Agregar comentarios XML para la documentación de API
//             var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
//             var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
//             config.PostProcess = document => document.Info.ExtensionData["x-comment-file"] = xmlPath;
//
//             // Seguridad JWT
//             config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
//             {
//                 Type = OpenApiSecuritySchemeType.ApiKey,
//                 Name = "Authorization",
//                 In = OpenApiSecurityApiKeyLocation.Header,
//                 Description = "Enter JWT Bearer token **_only_** (Ejemplo: 'Bearer eyJhbGciOiJIUzI1...')"
//             });
//
//             config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
//         });
//
//         return services;
//     }
// }