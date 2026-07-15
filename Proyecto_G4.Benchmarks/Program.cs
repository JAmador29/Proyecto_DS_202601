using System;
using BenchmarkDotNet.Running;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("====================================================");
        Console.WriteLine("    SISTEMA DE MÉTRICAS DE RENDIMIENTO (PROYECTO_G4) ");
        Console.WriteLine("====================================================");
        Console.WriteLine("Selecciona el módulo que deseas analizar:");
        Console.WriteLine("1. Módulo de Reportes (Compras/Ventas)");
        Console.WriteLine("2. Módulo de Usuarios y Permisos");
        Console.WriteLine("3. Módulo de Productos e Inventario");
        Console.WriteLine("4. Módulo de Proveedores");
        Console.WriteLine("====================================================");
        Console.Write("Introduce tu opción (1-4): ");

        string opcion = Console.ReadLine();
        Console.WriteLine("\nIniciando análisis estadístico... Por favor, espera.\n");

        switch (opcion)
        {
            case "1":
                BenchmarkRunner.Run<ReportesBenchmark>(); // Ejecuta tu clase de reportes
                break;
            case "2":
                BenchmarkRunner.Run<UsuariosBenchmark>(); // Ejecuta la de usuarios
                break;
            case "3":
                BenchmarkRunner.Run<ProductosBenchmark>(); // Ejecuta la de productos
                break;
            case "4":
                BenchmarkRunner.Run<ProveedoresBenchmark>(); // Ejecuta la de proveedores
                break;
            default:
                Console.WriteLine("Opción no válida. Saliendo del programa.");
                break;
        }
    }

    
}