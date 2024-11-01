using Departments;
using FluentValidation;
using Shared.Exceptions;
using Shared.Messaging.Extentions;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddDepartmentsModule(builder.Configuration);
builder.Services.AddDoctorsModule(builder.Configuration);
builder.Services.AddPatientsModule(builder.Configuration);
builder.Services.AddAppointmentsModule(builder.Configuration);

var doctorsAssembly = typeof(DoctorsModule).Assembly;
var departmentsAssembly = typeof(DepartmentsModule).Assembly;

//add carter
builder.Services.AddCarter(doctorsAssembly, departmentsAssembly);

//add fluent validation
builder.Services.AddValidatorsFromAssemblies([doctorsAssembly,departmentsAssembly]);

//add MediatR
builder.Services.AddMediatRDromAssemblies(doctorsAssembly, departmentsAssembly);

//add masstransit
builder.Services.AddMassTransitForAssemblies(doctorsAssembly, departmentsAssembly);




builder.Services.AddExceptionHandler<CustomExceptionHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
