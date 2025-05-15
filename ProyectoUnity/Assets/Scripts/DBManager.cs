using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using UnityEngine;


[DefaultExecutionOrder(-100)]
public class DBManager : MonoBehaviour
{
    private string dbBaseUri = "URI=file:";
    IDbConnection dbConnection;

    void Start()
    {
        string dbFileName = "restaurante.db";
        string dbFolderPath = Path.Combine(Application.dataPath, "Sql"); // Ruta a Assets/Sql
        Directory.CreateDirectory(dbFolderPath); // Asegura que la carpeta existe

        string dbFilePath = Path.Combine(dbFolderPath, dbFileName);
        string path = dbBaseUri + dbFilePath;

        Debug.Log("Ruta DB: " + path);

        dbConnection = new SqliteConnection(path);
        dbConnection.Open();

        createTables();
        populateDB();

    }


    public int GetNumeroElementosPorDificultad(string dificultad)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT NumeroElementos FROM TiposPedidos WHERE Dificultad = '{dificultad}';";
        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetInt32(0);
        }
        return 1; // Valor por defecto si algo falla
    }


    public List<Ingrediente> GetIngredientesPorRestaurante(string restaurante)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT Nombre, Tipo FROM Ingredientes WHERE Restaurante = '{restaurante}';";
        IDataReader reader = command.ExecuteReader();

        List<Ingrediente> ingredientes = new List<Ingrediente>();
        while (reader.Read())
        {
            ingredientes.Add(new Ingrediente
            {
                Nombre = reader.GetString(0),
                Tipo = reader.GetString(1)
            });
        }
        return ingredientes;
    }

    public string GetFraseCliente(int dif, string cliente)
    {
        string columna = dif == 0 ? "FraseFacil" :
                     dif == 1 ? "FraseDificil" : null;

        if (columna == null)
            return "Dificultad no válida";

        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT {columna} FROM Clientes WHERE LOWER(Animal) = LOWER(@cliente)";

        var param = command.CreateParameter();
        param.ParameterName = "@cliente";
        param.Value = cliente;
        command.Parameters.Add(param);

        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetString(0);
        }

        return "Frase no encontrada";
    }

    public void AnadirPedidoDB(string tipo, string bebida, string restaurante, int nivel)
    {
        using (IDbCommand command = dbConnection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Pedidos (Tipo, Bebida, Restaurante, Nivel) " +
                                  "VALUES (@tipo, @bebida, @restaurante, @nivel);";

            // Parámetros

            var paramTipo = command.CreateParameter();
            paramTipo.ParameterName = "@tipo";
            paramTipo.Value = tipo;
            command.Parameters.Add(paramTipo);

            var paramBebida = command.CreateParameter();
            paramBebida.ParameterName = "@bebida";
            paramBebida.Value = bebida;
            command.Parameters.Add(paramBebida);

            var paramRestaurante = command.CreateParameter();
            paramRestaurante.ParameterName = "@restaurante";
            paramRestaurante.Value = restaurante;
            command.Parameters.Add(paramRestaurante);

            var paramNivel = command.CreateParameter();
            paramNivel.ParameterName = "@nivel";
            paramNivel.Value = nivel;
            command.Parameters.Add(paramNivel);

            try
            {
                command.ExecuteNonQuery();
                Debug.Log("Pedido insertado correctamente en la base de datos.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error al insertar el pedido: " + e.Message);
            }
        }
    }
    public string GetBebida(string cliente)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT Nombre FROM Bebidas WHERE LOWER(Cliente) = LOWER(@cliente)";
        var param = command.CreateParameter();
        param.ParameterName = "@cliente";
        param.Value = cliente;
        command.Parameters.Add(param);

        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetString(0);
        }

        return "Bebida no encontrada";

    }
    public int GetPuntajePedidoPerfecto(string tipo)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT PuntosPerfecto FROM TiposPedidos WHERE LOWER(Dificultad) = LOWER(@tipo)";
        var param = command.CreateParameter();
        param.ParameterName = "@tipo";
        param.Value = tipo;
        command.Parameters.Add(param);

        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetInt32(0);
        }

        return 0;

    }
    public int GetPuntajePedidoUnError(string tipo)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT Puntos1Error FROM TiposPedidos WHERE LOWER(Dificultad) = LOWER(@tipo)";
        var param = command.CreateParameter();
        param.ParameterName = "@tipo";
        param.Value = tipo;
        command.Parameters.Add(param);

        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetInt32(0);
        }

        return 0;

    }
    public int GetPuntajePedidoDosErrores(string tipo)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT Puntos2Errores FROM TiposPedidos WHERE LOWER(Dificultad) = LOWER(@tipo)";
        var param = command.CreateParameter();
        param.ParameterName = "@tipo";
        param.Value = tipo;
        command.Parameters.Add(param);

        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetInt32(0);
        }

        return 0;

    }
    public int GetPuntajePedidoTresErrores(string tipo)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT Puntos3Errores FROM TiposPedidos WHERE LOWER(Dificultad) = LOWER(@tipo)";
        var param = command.CreateParameter();
        param.ParameterName = "@tipo";
        param.Value = tipo;
        command.Parameters.Add(param);

        IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return reader.GetInt32(0);
        }

        return 0;

    }


    private void createTables()
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = readFromFile(Path.Combine("Sql", "ddl.sql"));
        command.ExecuteReader();
    }

    private void populateDB()
    {
        int elementos = getNumberOfElements("Ingredientes");
        if (elementos != 0)
        {
            return;
        }

        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = readFromFile(Path.Combine("Sql", "dml.sql"));
        command.ExecuteNonQuery();
    }

    private int getNumberOfElements(string tableName)
    {
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"SELECT count(*) FROM {tableName}";
        IDataReader reader = command.ExecuteReader();
        reader.Read();
        return reader.GetInt32(0);
    }

    private string readFromFile(string fileName)
    {
        string path = Path.Combine(Application.dataPath, fileName);
        return File.ReadAllText(path);
    }

    public class Ingrediente
    {
        public string Nombre;
        public string Tipo;
    }
    void OnApplicationQuit()
    {
        if (dbConnection != null)
        {
            dbConnection.Close();
            Debug.Log("Conexión a la base de datos cerrada.");
        }
    }
}