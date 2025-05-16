using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int Nivel;
    public static string Restaurante;
    public static int puntaje;
    public static string cliente;
    public static int numElementosHechos;
    public static int puntosNivel;
    public static string dificultad;
    public static int puntos;
    public static int clienteNum;
    public static string bebida;
    public static string bebidaSeleccionada;
    public static int numElementos;
    public static int errores, erroresTotales;

    public static string[][] Elementos = new string[5][];
    public static string[][] ElementosJugador = new string[5][];

    public static void EvaluarPedidos()
    {
        Debug.Log("Evaluando pedidos");

        if (DBManager.Instance == null)
        {
            Debug.LogError("DBManager.Instance no está inicializado.");
            return;
        }

        puntos = 0;
        erroresTotales = 0;
        var db = DBManager.Instance;

       
        erroresTotales = ContarErrores(Elementos, ElementosJugador, numElementos);

        if (erroresTotales == 0)
            puntos = db.GetPuntajePedidoPerfecto(dificultad);
        else if (erroresTotales == 1)
            puntos = db.GetPuntajePedidoUnError(dificultad);
        else if (erroresTotales == 2)
            puntos = db.GetPuntajePedidoDosErrores(dificultad);
        else
            puntos = db.GetPuntajePedidoTresErrores(dificultad);

        Debug.Log($"\nTOTAL DE ERRORES: {erroresTotales}");
        Debug.Log($"\nPUNTOS TOTALES DEL NIVEL: {puntos}\n");
    }

    public static int ContarErrores(string[][] platos, string[][] platosJugador, int numPlatos)
    {
        int errores = 0;

        for (int i = 0; i < numPlatos; i++)
        {
            for (int j = 0; j < platos[i].Length; j++)
            {
                string ingrediente = platos[i][j];
                bool encontrado = false;

                for (int k = 0; k < platosJugador[i].Length; k++)
                {
                    if (ingrediente == platosJugador[i][k])
                    {
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    errores++;
                }
            }
        }

        return errores;
    }
}
