using BrewUp.Infrastructure.MongoDb;
using BrewUp.Rest.Modules;
using BrewUp.Rest.Services;
using BrewUp.Rest.Validators.Warehouses;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.Entities;
using BrewUp.Shared.Queries;
using BrewUp.Warehouse.ReadModel.Queries;
using BrewUp.Warehouse.ReadModel.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", corsBuilder => corsBuilder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()); });
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
	Description = "BrewUp",
	Title = "BrewUp API",
	Version = "v1",
	Contact = new OpenApiContact
	{
		Name = "BrewUp"
	}
}));


var mongoDbSettings = builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddMongoDb(mongoDbSettings!);

builder.RegisterModules();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<SetAvailabilityValidator>();
builder.Services.AddSingleton<ValidationHandler>();

var app = builder.Build();

app.UseCors("CorsPolicy");

MapSalesEndpoints(app);
MapWarehouseEndpoints(app);

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
	s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
	s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp");
	s.RoutePrefix = "documentation";
});

await app.RunAsync();

void MapSalesEndpoints(WebApplication webApplication)
{
	//Sales
	var salesGroup = webApplication.MapGroup("/v1/sales/").WithTags("Sales");
	salesGroup.MapPost("/", SalesOrderHandler.HandleCreateSalesOrder)
		.Produces(StatusCodes.Status400BadRequest)
		.Produces(StatusCodes.Status201Created)
		.WithName("CreateSalesOrder");

	salesGroup.MapGet("/", SalesOrderHandler.HandleGetOrders)
		.Produces(StatusCodes.Status404NotFound)
		.Produces(StatusCodes.Status200OK)
		.WithName("GetSalesOrders");
}

void MapWarehouseEndpoints(WebApplication app1)
{
	//Warehouses
	var warehousesGroup = app1.MapGroup("/v1/warehouses/").WithTags("Warehouses");
	warehousesGroup.MapPost("/availabilities", WarehousesService.HandleSetAvailabilities)
		.Produces(StatusCodes.Status400BadRequest)
		.Produces(StatusCodes.Status200OK)
		.WithName("SetAvailabilities");
}