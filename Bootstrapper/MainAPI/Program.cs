using Authentication;
using Departments;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Shared.Exceptions;
using Shared.Messaging.Extentions;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
builder.Services.AddAuthenticationModule(builder.Configuration);
builder.Services.AddDepartmentsModule(builder.Configuration);
builder.Services.AddDoctorsModule(builder.Configuration);
builder.Services.AddPatientsModule(builder.Configuration);
builder.Services.AddAppointmentsModule(builder.Configuration);

var doctorsAssembly = typeof(DoctorsModule).Assembly;
var departmentsAssembly = typeof(DepartmentsModule).Assembly;
var authenticationAssembly = typeof(AuthenticationModule).Assembly;
var emailAssembly = typeof(Shared.Services.EmailService).Assembly;

//add carter
builder.Services.AddCarter(doctorsAssembly, departmentsAssembly, authenticationAssembly);

//add fluent validation
builder.Services.AddValidatorsFromAssemblies([doctorsAssembly,departmentsAssembly, authenticationAssembly]);

//add MediatR
builder.Services.AddMediatRDromAssemblies(doctorsAssembly, departmentsAssembly, authenticationAssembly, emailAssembly);

//add masstransit
builder.Services.AddMassTransitForAssemblies(doctorsAssembly, departmentsAssembly, authenticationAssembly);




builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddSharedServices(builder.Configuration);
builder.Services.AddHttpClient();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration.GetSection("IdentityServer").Value;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
        };
    });


builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //{
    //    Type = SecuritySchemeType.OAuth2,
    //    Flows = new OpenApiOAuthFlows
    //    {
    //        AuthorizationCode = new OpenApiOAuthFlow
    //        {
    //            AuthorizationUrl = new Uri("https://localhost:5005/connect/authorize"),
    //            TokenUrl = new Uri("https://localhost:5005/connect/token")
    //        }
    //    }
    //});

    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "oauth2"
    //            }
    //        },
    //        new List<string>()
    //    }
    //});

});


var app = builder.Build();
app.MapCarter();

app.UseSwaggerUI(options =>
{
   
});

app.UseSerilogRequestLogging();
app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.
app.UseAuthenticationModule();
app.UseDepartmentsModule();
app.UseDoctorsModule();
app.UsePatientsModule();
app.UseAppointmentsModule();

if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
