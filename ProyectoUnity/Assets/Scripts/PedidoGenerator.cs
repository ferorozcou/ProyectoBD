using UnityEngine;
using System.Collections.Generic;

public class PedidoGenerator : MonoBehaviour
{
    public DBManager dbManager; // Asignalo desde el Inspector si es necesario
        
    void Start()
    {

    }

    public string[] GenerarPedido(int dif, int rest, string cliente,int nivel)
    {
        if (dbManager == null)
        {
            Debug.LogError("DBManager no asignado en PedidoGenerator.");
            return new string[] { "Error: no se pudo generar el pedido porque DBManager no est� disponible." };
        }
        // Selecci�n aleatoria de restaurante y dificultad
        string[] restaurantes = { "Venezolano", "Mexicano", "Espa�ol" };
        string[] dificultades = { "F�cil", "Medio", "Dif�cil" };

        int restIndex = rest;
        int difIndex = dif;
        int difFrase = 0;

        string restaurante = restaurantes[restIndex];
        string dificultad = dificultades[difIndex];

        int numElementos = dbManager.GetNumeroElementosPorDificultad(dificultad);
        List<DBManager.Ingrediente> ingredientes = dbManager.GetIngredientesPorRestaurante(restaurante);

        // Separar por tipo
        List<DBManager.Ingrediente> bases = ingredientes.FindAll(i => i.Tipo == "Base");
        List<DBManager.Ingrediente> rellenos = ingredientes.FindAll(i => i.Tipo == "Relleno");
        List<DBManager.Ingrediente> extras = ingredientes.FindAll(i => i.Tipo != "Base" && i.Tipo != "Relleno");
        string[] Pedido = new string[numElementos + 3];
        if (restaurante == "Venezolano")
        {
            Pedido[0] = $"Quiero un pedido con {numElementos} arepa(s).";
        }
        if (restaurante == "Mexicano")
        {
            Pedido[0] = $"Quiero un pedido con {numElementos} sope(s).";
        }
        if (restaurante == "Espa�ol")
        {
            Pedido[0] = $"Quiero un pedido con {numElementos} tortilla(s).";
        }

        for (int i = 0; i < numElementos; i++)
        {
            string baseNombre = bases[Random.Range(0, bases.Count)].Nombre;
            string rellenoNombre = rellenos[Random.Range(0, rellenos.Count)].Nombre;
            string extraNombre = extras[Random.Range(0, extras.Count)].Nombre;

            Pedido[i+1]=$"Base: {baseNombre};\n Relleno: {rellenoNombre};\n Topping/Extra: {extraNombre}";
        }
        Pedido[numElementos + 1] = dbManager.GetFraseCliente(difFrase, cliente);
        Pedido[numElementos + 2] = "Presiona Enter para ir a la cocina";
        dbManager.AnadirPedidoDB(dificultad, dbManager.GetBebida(cliente), restaurante, nivel);
        return Pedido;
    }
}