using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CocinaManager : MonoBehaviour
{
    public static CocinaManager Instance;
    private List<DragManager> ingredientes = new List<DragManager>();
    public EvaluarPuntosPedido evaluadorPuntos;

    public GameObject nuevaImagenUI; // Asigna esta imagen desde el inspector
    public GameObject mensajeErrorUI; // Imagen que se muestra si se supera el límite de clics
    public GameObject Ayuda, BotonCerrar; // Referencias para mostrar y ocultar la ayuda
    public GameObject botonEntregar; // Botón de entregar asignado desde el inspector
    public TextMeshProUGUI notaPedido; //Texto con la nota del pedido en la UI
    private int contadorClicks = 0; // Lleva el conteo de cuántas veces se ha hecho clic

    private Vector3 posicionOriginal; // Guarda la posición original del plato final

    public bool HaTerminado => contadorClicks >= GameData.numElementos; //Bool para revisar si terminó o no el pedido

    void Awake() // Hacemos un Singleton de CocinaManager
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Asignamos numElementos según dificultad
        GameData.ActualizarNumElementos();
    }

    void Start()
    {
        ActualizarNotaPedido();
        if (Ayuda != null) Ayuda.SetActive(false);
        if (BotonCerrar != null) BotonCerrar.SetActive(false);

        if (nuevaImagenUI != null)
        {
            posicionOriginal = nuevaImagenUI.transform.localPosition; // Guarda la posición original del plato final
            nuevaImagenUI.SetActive(false); // Oculta la imagen al iniciar el juego
        }

        if (mensajeErrorUI != null)
        {
            mensajeErrorUI.SetActive(false); // Oculta el mensaje de error (vamos el de que has alcanzado el máximo de elementos al iniciar
        }

        if (botonEntregar != null)
        {
            botonEntregar.SetActive(false); // Oculta el botón al iniciar
        }
    }

    void Update()
    {
       
    }

    public void RegistrarIngrediente(DragManager ingrediente) // Agregamos los ingredientes del plato a la lista
    {
        if (!ingredientes.Contains(ingrediente)) // Evitamos duplicados
            ingredientes.Add(ingrediente);
    }

    public void EliminarIngrediente(DragManager ingrediente) // Eliminamos ingredientes tirados a la papelera
    {
        ingredientes.Remove(ingrediente);
    }

    public void ResetearTodo()
    {
        if (HaTerminado)
        {
            if (mensajeErrorUI != null)
            {
                mensajeErrorUI.SetActive(true);
                mensajeErrorUI.transform.SetAsLastSibling(); // Lo ponemos al frente de todo
            }

            if (botonEntregar != null)
            {
                botonEntregar.SetActive(true); // Mostramos el botón de entregar
                botonEntregar.transform.SetAsLastSibling(); // Lo ponemos por encima del mensaje de error
            }
            return;
        }

        // Guardar los ingredientes actuales en el plato como un array de strings
        List<string> ingredientesDelJugador = new List<string>();
        ingredientes.RemoveAll(i => i.fueEliminado && !i.estaDentroDelPlato); // Quitamos los ingredientes eliminados

        foreach (DragManager ingrediente in ingredientes)
        {
            if (ingrediente.estaDentroDelPlato) 
            {
                ingredientesDelJugador.Add(ingrediente.nombreIngrediente);
                if (ingredientesDelJugador.Count == 3)
                    break;
            }
        }

        if (ingredientesDelJugador.Count < 3)
        {
            Debug.LogWarning("No hay suficientes ingredientes en el plato.");
        }
        else
        {
            GameData.ElementosJugador[contadorClicks] = ingredientesDelJugador.ToArray();
            Debug.Log($"Guardado en ElementosJugador[{contadorClicks}]: {string.Join(", ", ingredientesDelJugador)}");
            contadorClicks++;
        }

        foreach (DragManager ingrediente in ingredientes) // Reseteo de todos los ingredientes
        {
            ingrediente.ResetearIngrediente();
        }

        // Si existe mostramos la imagen que animaremos (el plato completado)
        if (nuevaImagenUI != null)
        {
            nuevaImagenUI.SetActive(true);
            nuevaImagenUI.transform.SetAsLastSibling();
            nuevaImagenUI.transform.localPosition = posicionOriginal;
            StartCoroutine(AnimarImagen());
        }
        ActualizarNotaPedido(); // Cuando el pedido se resetee también las notas
    }

    private IEnumerator AnimarImagen()
    {

        yield return new WaitForSeconds(0.7f); // Espera 0.7 segundos sin moverse

        Vector3 posicionFinal = posicionOriginal + new Vector3(0, 500, 0); // Posición final hacia arriba fuera del encuadre del canvas

        float duracion = 0.5f;
        float tiempo = 0;

        Vector3 inicio = nuevaImagenUI.transform.localPosition;

        // Movimiento hacia arriba durante 0.5 segs
        while (tiempo < duracion)
        {
            nuevaImagenUI.transform.localPosition = Vector3.Lerp(inicio, posicionFinal, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        nuevaImagenUI.transform.localPosition = posicionFinal;

        // Oculta la imagen
        nuevaImagenUI.SetActive(false);

        // Vuelve a la posición original
        nuevaImagenUI.transform.localPosition = posicionOriginal;
    }

    public void AyudaActivada()
    {
        Ayuda.transform.SetAsLastSibling();
        Ayuda.SetActive(true);
        BotonCerrar.transform.SetAsLastSibling();
        BotonCerrar.SetActive(true);
    }

    public void AyudaDesactivada()
    {
        Ayuda.SetActive(false);
        BotonCerrar.SetActive(false);
    }

    public void CambiarABebidas()
    {
        evaluadorPuntos.EvaluarPedidos();
        SceneManager.LoadScene(8);
    }


    // Método para actualizar notaPedido con los elementos del pedido
    public void ActualizarNotaPedido()
    {
        if (notaPedido == null) return;  // Por si no está asignado en inspector

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < GameData.numElementos; i++)
        {
            // Comprobamos que no sea null para evitar errores
            if (GameData.Elementos[i] != null)
            {
                sb.Append($"Elemento {i + 1}: ");
                sb.Append(string.Join(", ", GameData.Elementos[i])); //Une todos los strings del array Elementos separados por coma y espacio
                sb.Append("\n"); //Salto de línea para el siguiente elemento
            }
        }

        notaPedido.text = sb.ToString();
    }
}
