using Robotic.Web.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

StudentsRoutes.AddStudentsRoutes(app);
ProfessionalRoutes.AddProfessionalRoutes(app);
InstitutionalRoutes.AddInstitutionalRoutes(app);
ActivityRoutes.AddActivityRoutes(app);
TrailRoutes.AddTrailRoutes(app);

app.Run();