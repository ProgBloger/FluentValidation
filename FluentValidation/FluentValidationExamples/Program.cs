using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidationExamples;
using FluentValidationExamples.ExternalClients;
using FluentValidationExamples.Models;
using FluentValidationExamples.Resources;
using FluentValidationExamples.Validators;
using FluentValidationExamples.Validators.Basics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// By default models are checked for nulls by controllers
// You may remove default validation errors for Null values in the following ways:
// 1. Add ? to the model properties to make them nullable
// 2. Remove the <Nullable>enable</Nullable> node from the .csproj file
// 3. Set SuppressImplicitRequiredAttributeForNonNullableReferenceTypes to true as below
builder.Services.AddControllers(options => { options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; });

ConfigureValidation(builder.Services);

void ConfigureValidation(IServiceCollection services)
{
    // By default registration is Scoped
    services.AddValidatorsFromAssemblyContaining<CustomerValidator>(ServiceLifetime.Transient);

    // Optional filter that can exclude some validators from automatic registration
    //builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>(ServiceLifetime.Transient, filter => filter.ValidatorType != typeof(CustomerValidator));

    // Load using a type reference rather than the generic.
    //services.AddValidatorsFromAssemblyContaining(typeof(UserValidator));

    // Load an assembly reference rather than using a marker type.
    //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

    // Global level configuration for the CascadeMode
    //ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
    //ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
    //ValidatorOptions.Global.LanguageManager = new CustomLanguageManager();

    services.AddTransient<IValidator<Customer>, CustomerValidator>();
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IFactory, Factory>();
builder.Services.AddTransient<SomeExternalWebApiClient>();

// Localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

var app = builder.Build();

ConfigureLocalizationMiddleware(app);

void ConfigureLocalizationMiddleware(WebApplication app)
{
    var supportedCultures = new[] { "en-US", "nb-NO" };
    var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    app.UseRequestLocalization(localizationOptions);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();