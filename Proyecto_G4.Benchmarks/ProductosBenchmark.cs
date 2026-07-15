using BenchmarkDotNet.Attributes;
using Capa_Negocio;

[MemoryDiagnoser]
public class ProductosBenchmark
{
    private CN_Producto _capaProducto;

    [GlobalSetup]
    public void Setup()
    {
        _capaProducto = new CN_Producto();
    }

    [Benchmark]
    public void MetricarCargaProductos()
    {
        // Cambia 'Listar()' por el nombre del método real de tu CN_Producto
        _capaProducto.Listar();
    }
}