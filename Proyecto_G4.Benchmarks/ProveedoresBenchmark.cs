using BenchmarkDotNet.Attributes;
using Capa_Negocio;

[MemoryDiagnoser]
public class ProveedoresBenchmark
{
    private CN_Proveedor _capaProveedor;

    [GlobalSetup]
    public void Setup()
    {
        _capaProveedor = new CN_Proveedor();
    }

    [Benchmark]
    public void MetricarListarProveedores()
    {
        // Cambia 'Listar()' por el método real que usas en frmReporteCompras
        _capaProveedor.Listar();
    }
}