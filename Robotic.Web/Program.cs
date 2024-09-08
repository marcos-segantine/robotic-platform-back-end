using Robotic.Web.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

StudentsRoutes.AddStudentsRoutes(app);
ProfessionalRoutes.AddProfessionalRoutes(app);
InstitutionalRoutes.AddInstitutionalRoutes(app);
ActivityRoutes.AddActivityRoutes(app);
TrailRoutes.AddTrailRoutes(app);

app.Run();