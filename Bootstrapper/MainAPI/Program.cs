using Departments;
using Shared.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddAppointmentsModule(builder.Configuration);
builder.Services.AddDepartmentsModule(builder.Configuration);
builder.Services.AddDoctorsModule(builder.Configuration);
builder.Services.AddPatientsModule(builder.Configuration);
builder.Services.AddCarter(typeof(DoctorsModule).Assembly);

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
