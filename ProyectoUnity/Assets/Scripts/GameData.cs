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

    public static float tiempoRestante = -1f; // Guarda el tiempo entre escenas. Si es -1, significa que aun no se ha empezado.

    public static float ObtenerTiempoLimite()
    {
        // Revisa que se ha elegido un restaurante
        if (string.IsNullOrEmpty(Restaurante))
        {
            Debug.LogWarning("GameData.Restaurante no est� definido."); // Si no, lanza un mensaje de error
            return 120f; //  y retorna el tiempo por defecto
        }

        // Usamos el valor nivel para determinar la dificultad actual
        string dificultadActual;

        switch (Nivel)
        {
            case 1:
                dificultadActual = "F�cil";
                break;
            case 2:
                dificultadActual = "Medio";
                break;
            case 3:
                dificultadActual = "Dif�cil";
                break;
            default:
                Debug.LogWarning("Nivel fuera de rango (1-3): " + Nivel);
                dificultadActual = "F�cil";
                break;
        }

        dificultad = dificultadActual; // Asigna la dificultad a la variable global detectada

        if (dificultadActual == "Dificil")
            return 130f; // Si la dificultad es dif�cil se devuelven 130 segundos, si no 120...
        else
            return 120f;
    }


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
