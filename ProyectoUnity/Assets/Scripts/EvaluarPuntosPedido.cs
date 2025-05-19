
using UnityEngine;

public class EvaluarPuntosPedido : MonoBehaviour
{
    public int erroresTotales;
    public int puntos;
    public void EvaluarPedidos()
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


        erroresTotales = ContarErrores(GameData.Elementos, GameData.ElementosJugador, GameData.numElementos);

        if (erroresTotales == 0)
            puntos = db.GetPuntajePedidoPerfecto(GameData.dificultad);
        else if (erroresTotales == 1)
            puntos = db.GetPuntajePedidoUnError(GameData.dificultad);
        else if (erroresTotales == 2)
            puntos = db.GetPuntajePedidoDosErrores(GameData.dificultad);
        else
            puntos = db.GetPuntajePedidoTresErrores(GameData.dificultad);

        Debug.Log($"\nTOTAL DE ERRORES: {erroresTotales}");
        Debug.Log($"\nPUNTOS TOTALES DEL PEDIDO: {puntos}\n");
        puntos = puntos + GameData.puntosPapelera;
        db.ActualizarPuntosObtenidos(GameData.idPedidoActual, puntos);
        GameData.puntosPedidoActual = puntos;
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
