using BenchmarkDotNet.Attributes;
using Capa_Negocio;
using Capa_Entidad;

[MemoryDiagnoser]
public class UsuariosBenchmark
{
    private CN_Usuario _capaUsuario;

    [GlobalSetup]
    public void Setup()
    {
        _capaUsuario = new CN_Usuario();
    }

    [Benchmark]
    public void MetricarListarUsuarios()
    {
        // Borra el punto y elige tu método real (ej. Listar(), ObtenerUsuarios(), etc.)
        _capaUsuario.Listar();
    }
}