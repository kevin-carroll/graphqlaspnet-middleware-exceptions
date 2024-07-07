using Example;
using GraphQL.AspNet.Configuration;
using Microsoft.AspNetCore.Builder;


// Configure the application
var builder = WebApplication.CreateBuilder(args);
var pipelineBuilder = builder.Services.AddGraphQL();

// extend the field execution pipeline with our middleware component
// to be run at the end of every field on every item.
pipelineBuilder.FieldExecutionPipeline.AddMiddleware<ExceptionInspectorMiddleware>();

// build the app
var app = builder.Build();
app.UseGraphQL();
app.Run();
