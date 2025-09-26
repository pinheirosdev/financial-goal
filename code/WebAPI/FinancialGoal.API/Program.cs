using FinancialGoal.Application.Services;
using FinancialGoal.Core.Interfaces;
using FinancialGoal.Core;
using FinancialGoal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region config da injeção de dependência

// config do dbcontext do EF para usar postgre
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// registra as interfaces e as implementações concretas
builder.Services.AddScoped<ITesouroRepository, TesouroRepository>();
builder.Services.AddScoped<ISimulacaoService, SimulacaoService>();

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAnyOrigin");

// endpoint
app.MapPost("/simular", async (ParametrosSimulacao parametros, ISimulacaoService service) =>
{
    if (parametros.MetaValor <= 0 || parametros.AporteMensal < 0 || parametros.AporteInicial < 0)
    {
        return Results.BadRequest("Valores de meta, aporte inicial e mensal devem ser positivos.");
    }

    try
    {
        var resultado = await service.Simular(parametros);
        return Results.Ok(resultado);
    }
    catch (Exception ex)
    {
        return Results.Problem(title: "Ocorreu um erro ao processar a simulação.", detail: ex.Message, statusCode: 500);
    }
})
.WithName("SimularInvestimento")
.WithTags("Simulação");

app.Run();