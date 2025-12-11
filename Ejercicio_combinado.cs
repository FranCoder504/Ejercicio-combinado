using System;
using System.Collections.Generic;
using System.Linq;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}

public class Pedido
{
    public int ClienteId { get; set; }
    public string Producto { get; set; }
    public int Cantidad { get; set; }
}

class Program
{
    static void Main()
    {
        var clientes = new List<Cliente>
        {
            new Cliente { Id = 1, Nombre = "Juan" },
            new Cliente { Id = 2, Nombre = "María" },
            new Cliente { Id = 3, Nombre = "Carlos" }
        };

        var pedidos = new List<Pedido>
        {
            new Pedido { ClienteId = 1, Producto = "Laptop", Cantidad = 1 },
            new Pedido { ClienteId = 2, Producto = "Mouse", Cantidad = 2 },
            new Pedido { ClienteId = 1, Producto = "Teclado", Cantidad = 1 },
            new Pedido { ClienteId = 2, Producto = "Monitor", Cantidad = 1 },
            new Pedido { ClienteId = 3, Producto = "USB", Cantidad = 2 }
        };

        // Paso 1: Join para relacionar cliente con pedido
        var join = from c in clientes
                   join p in pedidos on c.Id equals p.ClienteId
                   select new { c.Nombre, p.Cantidad };

        // Paso 2: GroupBy por cliente
        var agrupado = join.GroupBy(x => x.Nombre);

        // Paso 3 y 4: Aggregate (suma de cantidades) + Select (formato)
        var resultado = agrupado.Select(g =>
        {
            int total = g.Aggregate(0, (acum, item) => acum + item.Cantidad);
            return new { Cliente = g.Key, TotalProductos = total };
        });

        // Mostrar
        foreach (var r in resultado)
        {
            Console.WriteLine($"Cliente: {r.Cliente}, Total productos: {r.TotalProductos}");
        }
    }
}
