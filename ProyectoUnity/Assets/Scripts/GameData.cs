using UnityEngine;

public static class GameData
{
    public static int Nivel;
    public static int NumPediddoActual;
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
    public static int nivelAnterior = -1;
    public static string ultimaEscena = "";
    public static float tiempoRestante = -1f;
    public static int numRestaurante;
    public static int ultimoRestauranteCargado = -1;
    public static int ultimoNivelCargado = -1;
    public static int ultimoNivelCocina = -1;
    public static int ultimoRestauranteCocina = -1;


    public static string[][] Elementos = new string[5][];
    public static string[][] ElementosJugador = new string[5][];



    public static float ObtenerTiempoLimite()
    {
        try
        {
            int tiempo = DBManager.Instance.ObtenerTiempoPorNivel(Nivel); // <-- Usa nivel actual
            return tiempo;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al obtener tiempo para nivel " + Nivel + ": " + e.Message);
            return 120f;
        }
    }



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
