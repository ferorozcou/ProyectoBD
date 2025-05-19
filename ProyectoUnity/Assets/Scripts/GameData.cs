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

    // M�todo para actualizar el n�mero de elementos seg�n la dificultad
    public static void ActualizarNumElementos()
    {
        switch (dificultad)
        {
            case "F�cil":
                numElementos = 1;
                break;
            case "Medio":
                numElementos = 3;
                break;
            case "Dif�cil":
                numElementos = 5;
                break;
            default:
                numElementos = 3;
                break;
        }
    }
}
