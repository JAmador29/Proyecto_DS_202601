using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NBomber.Contracts;
using NBomber.CSharp;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = configuration.GetConnectionString("CadenaConexion");

var scenario = Scenario.Create("ConsultarProductos", async context =>
{
    try
    {
        using var conexion = new SqlConnection(connectionString);

        await conexion.OpenAsync();

        string sql = @"
            SELECT IdProducto,
                   Codigo,
                   Nombre,
                   Stock
            FROM Producto";

        var response = await Step.Run("consulta_sql", context, async () =>
        {
            using var cmd = new SqlCommand(sql, conexion);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                // recorrer resultados
            }

            return Response.Ok();
        });

        return Response.Ok();
    }
    catch (Exception ex)
    {
        return Response.Fail(); 
    }
})
.WithoutWarmUp()
.WithLoadSimulations(
    Simulation.KeepConstant(
        copies: 300,
        during: TimeSpan.FromSeconds(30)
    )
);
NBomberRunner
    .RegisterScenarios(scenario)
    .Run();

Console.WriteLine("Prueba terminada. Presiona una tecla para salir...");
Console.ReadKey();