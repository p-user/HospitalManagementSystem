

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAppointmentsModule(builder.Configuration);
builder.Services.AddDoctorsModule(builder.Configuration);
builder.Services.AddBillingModule(builder.Configuration);
builder.Services.AddPatientsModule(builder.Configuration);
builder.Services.AddCarter(typeof(DoctorsModule).Assembly);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapCarter();

// Configure the HTTP request pipeline.
app.UseDoctorsModule();
app.UsePatientsModule();
app.UseAppointmentsModule();
app.UseBillingModule();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
