using UnityEngine;
using System.Collections.Generic;

public class PedidoGenerator : MonoBehaviour
{
    public DBManager dbManager; // Asignalo desde el Inspector si es necesario

    void Start()
    {
        GenerarPedido(1, 2);
    }

    public string[] GenerarPedido(int dif, int rest)
    {
        // Selección aleatoria de restaurante y dificultad
        string[] restaurantes = { "Venezolano", "Mexicano", "Español" };
        string[] dificultades = { "Fácil", "Medio", "Difícil" };

        int restIndex = rest;
        int difIndex = dif;

        string restaurante = restaurantes[restIndex];
        string dificultad = dificultades[difIndex];

        int numElementos = dbManager.GetNumeroElementosPorDificultad(dificultad);
        List<DBManager.Ingrediente> ingredientes = dbManager.GetIngredientesPorRestaurante(restaurante);

        // Separar por tipo
        List<DBManager.Ingrediente> bases = ingredientes.FindAll(i => i.Tipo == "Base");
        List<DBManager.Ingrediente> rellenos = ingredientes.FindAll(i => i.Tipo == "Relleno");
        List<DBManager.Ingrediente> extras = ingredientes.FindAll(i => i.Tipo != "Base" && i.Tipo != "Relleno");
        string[] Pedido = new string[numElementos + 1];
        Pedido[0] = $"Pedido del restaurante {restaurante} con {numElementos} elementos. Dificultad: {dificultad}";

        for (int i = 0; i < numElementos; i++)
        {
            string baseNombre = bases[Random.Range(0, bases.Count)].Nombre;
            string rellenoNombre = rellenos[Random.Range(0, rellenos.Count)].Nombre;
            string extraNombre = extras[Random.Range(0, extras.Count)].Nombre;

            Pedido[i+1]=$"Base: {baseNombre}; Relleno: {rellenoNombre}; Topping/Extra: {extraNombre}";
        }
        return Pedido;
    }
}