using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

public class DBManager : MonoBehaviour
{
    private string dbBaseUri = "URI=file:";
    IDbConnection dbConnection;

    void Start()
    {
        string path = dbBaseUri + Path.Combine(Application.persistentDataPath, "restaurante.db");
        Debug.Log("Ruta DB: " + path);

        dbConnection = new SqliteConnection(path);
        dbConnection.Open();

        createTables();
        populateDB();

        GenerarPedidoAleatorio();

        dbConnection.Close();
    }

    void GenerarPedidoAleatorio()
    {
        // Selección aleatoria de restaurante
        string[] restaurantes = { "Español", "Venezolano", "Mexicano" };
        int indexRest = UnityEngine.Random.Range(0, 3);
        string restaurante = restaurantes[indexRest];

        // Selección aleatoria de dificultad
        string[] dificultades = { "Fácil", "Medio", "Difícil" };
        int indexDif = UnityEngine.Random.Range(0, 3);
        string dificultad = dificultades[indexDif];

        int cantidadElementos = GetNumeroElementosPorDificultad(dificultad);
        List<Ingrediente> ingredientes = GetIngredientesPorRestaurante(restaurante);

        // Agrupar ingredientes por tipo
        List<Ingrediente> bases = ingredientes.FindAll(i => i.Tipo == "Base");
        List<Ingrediente> rellenos = ingredientes.FindAll(i => i.Tipo == "Relleno");
        List<Ingrediente> extras = ingredientes.FindAll(i => i.Tipo != "Base" && i.Tipo != "Relleno"); // "Extra" o "Toppings"

        Debug.Log($"Pedido del restaurante {restaurante} con {cantidadElementos} elementos. Dificultad: {dificultad}");

        for (int i = 0; i < cantidadElementos; i++)
        {
            string baseElegida = bases[UnityEngine.Random.Range(0, bases.Count)].Nombre;
            string rellenoElegido = rellenos[UnityEngine.Random.Range(0, rellenos.Count)].Nombre;
            string extraElegido = extras[UnityEngine.Random.Range(0, extras.Count)].Nombre;

            Debug.Log($"Elemento {i + 1}: Base: {baseElegida}; Relleno: {rellenoElegido}; Topping/Extra: {extraElegido}");
        }
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
}