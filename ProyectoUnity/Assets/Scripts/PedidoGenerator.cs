using System;
using System.Collections.Generic;
using UnityEngine;

public class PedidoGenerator : MonoBehaviour
{
    public DBManager dbManager; // Asignalo desde el Inspector si es necesario

    void Start()
    {

    }

    public string[] GenerarPedido(int dif, int rest, string cliente, int nivel)
    {
        if (dbManager == null)
        {
            Debug.LogError("DBManager no asignado en PedidoGenerator.");
            return new string[] { "Error: no se pudo generar el pedido porque DBManager no está disponible." };
        }

        // Selección de restaurante y dificultad a partir de los índices
        string[] restaurantes = { "Venezolano", "Mexicano", "Español" };
        string[] dificultades = { "Fácil", "Medio", "Difícil" };

        int restIndex = rest;
        int difIndex = dif;
        int difFrase = 0;

        string restaurante = restaurantes[restIndex];
        string dificultad = dificultades[difIndex];

        // Guardamos la dificultad actual en GameData
        GameData.dificultad = dificultad;

        // Obtener cantidad de elementos y guardarlo en GameData
        int numElementos = dbManager.GetNumeroElementosPorDificultad(dificultad);
        GameData.numElementos = numElementos;

        // Obtener ingredientes del restaurante
        List<DBManager.Ingrediente> ingredientes = dbManager.GetIngredientesPorRestaurante(restaurante);

        // Separar ingredientes por tipo
        List<DBManager.Ingrediente> bases = ingredientes.FindAll(i => i.Tipo == "Base");
        List<DBManager.Ingrediente> rellenos = ingredientes.FindAll(i => i.Tipo == "Relleno");
        List<DBManager.Ingrediente> extras = ingredientes.FindAll(i => i.Tipo != "Base" && i.Tipo != "Relleno");

        string[] Pedido = new string[numElementos + 3];

        // Primera línea descriptiva según restaurante
        if (restaurante == "Venezolano")
        {
            Pedido[0] = $"Quiero un pedido con {numElementos} arepa(s).";
        }
        if (restaurante == "Mexicano")
        {
            Pedido[0] = $"Quiero un pedido con {numElementos} sope(s).";
        }
        if (restaurante == "Español")
        {
            Pedido[0] = $"Quiero un pedido con {numElementos} tortilla(s).";
        }

        // Generar los elementos del pedido
        for (int i = 0; i < numElementos; i++)
        {
            string baseNombre = bases[UnityEngine.Random.Range(0, bases.Count)].Nombre;
            string rellenoNombre = rellenos[UnityEngine.Random.Range(0, rellenos.Count)].Nombre;
            string extraNombre = extras[UnityEngine.Random.Range(0, extras.Count)].Nombre;

            // Guardamos la descripción en el pedido mostrado al jugador
            Pedido[i + 1] = $"Base: {baseNombre};\n Relleno: {rellenoNombre};\n Topping/Extra: {extraNombre}";

            // Guardamos también los ingredientes como datos para evaluación futura
            GameData.Elementos[i] = new string[] { baseNombre, rellenoNombre, extraNombre };
            Debug.Log($"Pedido generado [{i}]: {baseNombre}, {rellenoNombre}, {extraNombre}");
        }

        // Frase del cliente
        Pedido[numElementos + 1] = dbManager.GetFraseCliente(difFrase, cliente);

        // Instrucción final
        Pedido[numElementos + 2] = "Presiona Enter para ir a la cocina";

        // Guardamos la bebida y registramos el pedido en la base de datos
        GameData.bebida = dbManager.GetBebida(cliente);
        dbManager.AnadirPedidoDB(dificultad, GameData.bebida, restaurante, nivel);

        return Pedido;
    }
}