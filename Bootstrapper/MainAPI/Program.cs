using Authentication;
using Departments;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
var patientsAssembly = typeof(PatientsModule).Assembly;
var departmentsAssembly = typeof(DepartmentsModule).Assembly;
var authenticationAssembly = typeof(AuthenticationModule).Assembly;
var emailAssembly = typeof(Shared.Services.EmailService).Assembly;

//add carter
builder.Services.AddCarter(doctorsAssembly, departmentsAssembly, authenticationAssembly, patientsAssembly);

//add fluent validation
builder.Services.AddValidatorsFromAssemblies([doctorsAssembly,departmentsAssembly, authenticationAssembly, patientsAssembly]);

//add MediatR
builder.Services.AddMediatRDromAssemblies(doctorsAssembly, departmentsAssembly, authenticationAssembly, emailAssembly, patientsAssembly);

//add masstransit
builder.Services.AddMassTransitForAssemblies(doctorsAssembly, departmentsAssembly, authenticationAssembly, patientsAssembly);




builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddSharedServices(builder.Configuration);
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

//chat code
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Define the security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    // Apply the security scheme to all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

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
