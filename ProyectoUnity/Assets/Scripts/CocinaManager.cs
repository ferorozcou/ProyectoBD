using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CocinaManager : MonoBehaviour
{
    public static CocinaManager Instance;
    private List<DragManager> ingredientes = new List<DragManager>();

    public GameObject nuevaImagenUI; // Asigna esta imagen desde el inspector
    public GameObject mensajeErrorUI; // Imagen que se muestra si se supera el límite de clics

    public int numeroElementos = 3; // Número máximo de veces que se puede hacer clic
    private int contadorClicks = 0; // Lleva el conteo de cuántas veces se ha hecho clic

    private Vector3 posicionOriginal; // Guarda la posición original del plato final

    void Awake() // Hacemos un Singleton de CocinaManager
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (nuevaImagenUI != null)
        {
            posicionOriginal = nuevaImagenUI.transform.localPosition; // Guarda la posición original del plato final
            nuevaImagenUI.SetActive(false); // Oculta la imagen al iniciar el juego
        }

        if (mensajeErrorUI != null)
        {
            mensajeErrorUI.SetActive(false); // Oculta el mensaje de error al iniciar
        }
    }

    public void RegistrarIngrediente(DragManager ingrediente) // Agregamos los ingredientes del plato a la lista
    {
        if (!ingredientes.Contains(ingrediente))
            ingredientes.Add(ingrediente);
    }

    public void ResetearTodo()
    {
        // Si ya se ha hecho clic el número máximo de veces, mostramos el mensaje de error y salimos
        if (contadorClicks >= numeroElementos)
        {
            if (mensajeErrorUI != null)
            {
                mensajeErrorUI.SetActive(true); // Muestra el mensaje de error
                mensajeErrorUI.transform.SetAsLastSibling(); // Lo ponemos al frente de todo
            }
            return;
        }

        // Reseteamos todos los ingredientes a su posición original
        foreach (DragManager ingrediente in ingredientes)
        {
            ingrediente.ResetearIngrediente();
        }

        // Si existe la imagen del plato final
        if (nuevaImagenUI != null)
        {
            nuevaImagenUI.SetActive(true); // Muestra la imagen
            nuevaImagenUI.transform.SetAsLastSibling(); // Asegura que se muestre por encima de todos los demás elementos
            nuevaImagenUI.transform.localPosition = posicionOriginal; // Asegura que empiece en su posición original
            StartCoroutine(AnimarImagen()); // Corutina para que la imagen salga del encuadre
        }

        contadorClicks++; // Aumentamos el contador de clics
    }

    private IEnumerator AnimarImagen()
    {
        // Espera 0.7 segundos sin moverse
        yield return new WaitForSeconds(0.7f);

        // Posición final hacia arriba fuera del encuadre del canvas
        Vector3 posicionFinal = posicionOriginal + new Vector3(0, 500, 0); 

        float duracion = 0.5f;
        float tiempo = 0;

        Vector3 inicio = nuevaImagenUI.transform.localPosition;

        // Movimiento hacia arriba durante 0.5 segundos
        while (tiempo < duracion)
        {
            nuevaImagenUI.transform.localPosition = Vector3.Lerp(inicio, posicionFinal, tiempo / duracion); // Lerp es una función para interpolar entre la posición inicial y final
            tiempo += Time.deltaTime;
            yield return null; // Pausa la ejecución de la corutina hasta el siguiente frame
        }

        nuevaImagenUI.transform.localPosition = posicionFinal;

        // Oculta la imagen
        nuevaImagenUI.SetActive(false);

        // Restablece la posición original
        nuevaImagenUI.transform.localPosition = posicionOriginal;
    }
}
