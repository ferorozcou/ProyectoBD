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

        for (int i = 0; i < numElementos; i++)
        {
            string[] pedido = Elementos[i];
            string[] preparado = ElementosJugador[i];

            if (pedido == null || preparado == null)
            {
                Debug.Log($"Elemento {i + 1}: No se pudo evaluar.");
                continue;
            }
            int errores = SonListasIguales(pedido, preparado);
            erroresTotales= erroresTotales + errores;

        }

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

    static int SonListasIguales(string[] pedido, string[] preparado)
        {
            if (pedido.Length != preparado.Length) return 2;
        int errores = 0;

        for (int i = 0; i < 3; i++)
        {
            if (preparado[i] == pedido[0])
            {

            }
            else if (preparado[i] == pedido[1])
            {

            }
            else if (preparado[i] == pedido[2])
            {

            }
            else
            {
                errores++;
            }
        }

            return errores;
        }
}
