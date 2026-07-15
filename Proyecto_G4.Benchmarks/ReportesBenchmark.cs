using BenchmarkDotNet.Attributes;
using Capa_Negocio;
using Capa_Entidad;
using System;
using System.Collections.Generic;

[MemoryDiagnoser]
public class ReportesBenchmark
{
    private CN_Reporte _capaNegocio;
    private DateTime _fechaInicio;
    private DateTime _fechaFin;
    private int _idProveedor;

    [GlobalSetup]
    public void Setup()
    {
        _capaNegocio = new CN_Reporte();

        // Configuramos parámetros válidos para que no lancen excepciones
        _fechaInicio = new DateTime(2026, 1, 1);
        _fechaFin = new DateTime(2026, 7, 1);
        _idProveedor = 0; // Cambia este 0 por un ID de proveedor real que exista en tu BD si es necesario
    }

    [Benchmark]
    public List<ReporteCompra> MetricarReporteCompras()
    {
        // Llamamos al método "Compra" pasándole los parámetros que solicita
        return _capaNegocio.Compra(_fechaInicio, _fechaFin, _idProveedor);
    }

    [Benchmark]
    public List<ReporteVenta> MetricarReporteVenta()
    {
        // Llamamos al método "Venta" pasándole las fechas solicitadas
        return _capaNegocio.Venta(_fechaInicio, _fechaFin);
    }
}