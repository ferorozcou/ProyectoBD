using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;

// Representa un pedido con varios campos relevantes
[Serializable]
public class Pedido
{
    public int Id;
    public string Tipo;
    public string Bebida;
    public string Restaurante;
    public int Nivel;
    public int PuntosObtenidos;
}

// Representa un ingrediente que puede estar asociado a pedidos o menús
[Serializable]
public class Ingrediente
{
    public string Nombre;
    public string Tipo;
    public string Restaurante;
}

[Serializable]
public class Cliente
{
    public string Animal;
    public string FraseFacil;
    public string FraseDificil;
}

[Serializable]
public class TipoPedido
{
    public string Dificultad;
    public int NumeroElementos;
    public int PuntosPerfecto;
    public int Puntos1Error;
    public int Puntos2Errores;
    public int Puntos3Errores;
}

[Serializable]
public class Bebida
{
    public string Cliente;
    public string Nombre;
}

[Serializable]
public class RestauranteDataWrapper
{
    public List<Pedido> pedidos;
    public List<Ingrediente> ingredientes;
    public List<Cliente> clientes;
    public List<TipoPedido> tiposPedidos;
    public List<Bebida> bebidas;
}



// Clase para la exportación de datos desde la base de datos a un archivo JSON
public class JsonManager : MonoBehaviour
{
    public string dbFileName = "restaurante.db"; // Archivo de la base de datos

    void Start()
    {
        ExportarDatosAJson(); // Exporta los datos al iniciar
    }

    // Para exportar los datos desde la base de datos a un JSON
    public void ExportarDatosAJson()
    {
        string dbPath = Path.Combine(Application.dataPath, "Sql", dbFileName);
        string connPath = "URI=file:" + dbPath;

        using (IDbConnection connection = new SqliteConnection(connPath))
        {
            connection.Open();

            var pedidos = ObtenerPedidos(connection);
            var ingredientes = ObtenerIngredientes(connection);
            var clientes = ObtenerClientes(connection);
            var tiposPedidos = ObtenerTiposPedidos(connection);
            var bebidas = ObtenerBebidas(connection);

            RestauranteDataWrapper wrapper = new RestauranteDataWrapper
            {
                pedidos = pedidos,
                ingredientes = ingredientes,
                clientes = clientes,
                tiposPedidos = tiposPedidos,
                bebidas = bebidas
            };

            string json = JsonUtility.ToJson(wrapper, true);

            string exportFolder = Path.Combine(Application.dataPath, "Json");
            Directory.CreateDirectory(exportFolder);

            string exportPath = Path.Combine(exportFolder, "restaurante.json");

            File.WriteAllText(exportPath, json);

            Debug.Log("Datos exportados a JSON automáticamente al iniciar: " + exportPath);
        }
    }


    //Métodos para obtener las listas de cada tabla 

    private List<Pedido> ObtenerPedidos(IDbConnection connection)
    {
        List<Pedido> pedidos = new List<Pedido>();

        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Id, Tipo, Bebida, Restaurante, Nivel, [Puntos obtenidos] FROM Pedidos;";

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Se crea un nuevo objeto Pedido por cada fila leída
                    pedidos.Add(new Pedido
                    {
                        Id = reader.GetInt32(0),
                        Tipo = reader.GetString(1),
                        Bebida = reader.GetString(2),
                        Restaurante = reader.GetString(3),
                        Nivel = reader.GetInt32(4),
                        PuntosObtenidos = reader.IsDBNull(5) ? 0 : reader.GetInt32(5) // Manejo de valores nulos
                    });
                }
            }
        }

        return pedidos;
    }

    private List<Ingrediente> ObtenerIngredientes(IDbConnection connection)
    {
        List<Ingrediente> ingredientes = new List<Ingrediente>();

        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Nombre, Tipo, Restaurante FROM Ingredientes;";

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Se crea un nuevo objeto Ingrediente por cada fila leída
                    ingredientes.Add(new Ingrediente
                    {
                        Nombre = reader.GetString(0),
                        Tipo = reader.GetString(1),
                        Restaurante = reader.GetString(2)
                    });
                }
            }
        }

        return ingredientes;
    }

    private List<Cliente> ObtenerClientes(IDbConnection connection)
    {
        List<Cliente> clientes = new List<Cliente>();

        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Animal, FraseFacil, FraseDificil FROM Clientes;";

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Animal = reader.GetString(0),
                        FraseFacil = reader.GetString(1),
                        FraseDificil = reader.GetString(2)
                    });
                }
            }
        }

        return clientes;
    }

    private List<TipoPedido> ObtenerTiposPedidos(IDbConnection connection)
    {
        List<TipoPedido> tipos = new List<TipoPedido>();

        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Dificultad, NumeroElementos, PuntosPerfecto, Puntos1Error, Puntos2Errores, Puntos3Errores FROM TiposPedidos;";

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tipos.Add(new TipoPedido
                    {
                        Dificultad = reader.GetString(0),
                        NumeroElementos = reader.GetInt32(1),
                        PuntosPerfecto = reader.GetInt32(2),
                        Puntos1Error = reader.GetInt32(3),
                        Puntos2Errores = reader.GetInt32(4),
                        Puntos3Errores = reader.GetInt32(5)
                    });
                }
            }
        }

        return tipos;
    }

    private List<Bebida> ObtenerBebidas(IDbConnection connection)
    {
        List<Bebida> bebidas = new List<Bebida>();

        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Cliente, Nombre FROM Bebidas;";

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bebidas.Add(new Bebida
                    {
                        Cliente = reader.GetString(0),
                        Nombre = reader.GetString(1)
                    });
                }
            }
        }

        return bebidas;
    }

}
