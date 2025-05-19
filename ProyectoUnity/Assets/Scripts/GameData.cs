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
    public static int clienteNum;
    public static string bebida;
    public static string bebidaSeleccionada;
    public static int numElementos;
    public static int idPedidoActual;
    public static int puntosPedidoActual;
    public static int puntosPapelera = 0;

    public static string[][] Elementos = new string[5][];
    public static string[][] ElementosJugador = new string[5][];

    // Método para actualizar el número de elementos según la dificultad
    public static void ActualizarNumElementos()
    {
        switch (dificultad)
        {
            case "Fácil":
                numElementos = 1;
                break;
            case "Medio":
                numElementos = 3;
                break;
            case "Difícil":
                numElementos = 5;
                break;
            default:
                numElementos = 3;
                break;
        }
    }
}
