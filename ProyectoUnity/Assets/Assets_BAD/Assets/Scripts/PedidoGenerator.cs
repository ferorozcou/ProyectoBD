using UnityEngine;
using System.Collections.Generic;

public class PedidoGenerator : MonoBehaviour
{
    public DBManager dbManager; // Asignalo desde el Inspector si es necesario

    void Start()
    {
        // Asegurar conexión y datos
        dbManager = FindObjectOfType<DBManager>();
        if (dbManager == null)
        {
            Debug.LogError("No se encontró el DBManager en la escena.");
            return;
        }

        GenerarPedido();
    }

    void GenerarPedido()
    {
        // Selección aleatoria de restaurante y dificultad
        string[] restaurantes = { "Español", "Venezolano", "Mexicano" };
        string[] dificultades = { "Fácil", "Medio", "Difícil" };

        int restIndex = Random.Range(0, restaurantes.Length);
        int difIndex = Random.Range(0, dificultades.Length);

        string restaurante = restaurantes[restIndex];
        string dificultad = dificultades[difIndex];

        int numElementos = dbManager.GetNumeroElementosPorDificultad(dificultad);
        List<DBManager.Ingrediente> ingredientes = dbManager.GetIngredientesPorRestaurante(restaurante);

        // Separar por tipo
        List<DBManager.Ingrediente> bases = ingredientes.FindAll(i => i.Tipo == "Base");
        List<DBManager.Ingrediente> rellenos = ingredientes.FindAll(i => i.Tipo == "Relleno");
        List<DBManager.Ingrediente> extras = ingredientes.FindAll(i => i.Tipo != "Base" && i.Tipo != "Relleno");

        Debug.Log($"Pedido del restaurante {restaurante} con {numElementos} elementos. Dificultad: {dificultad}");

        for (int i = 0; i < numElementos; i++)
        {
            string baseNombre = bases[Random.Range(0, bases.Count)].Nombre;
            string rellenoNombre = rellenos[Random.Range(0, rellenos.Count)].Nombre;
            string extraNombre = extras[Random.Range(0, extras.Count)].Nombre;

            Debug.Log($"Base: {baseNombre}; Relleno: {rellenoNombre}; Topping/Extra: {extraNombre}");
        }
    }
}