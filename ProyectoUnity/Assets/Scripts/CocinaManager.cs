// === CocinaManager.cs ===
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CocinaManager : MonoBehaviour
{
    public static CocinaManager Instance;
    private List<DragManager> ingredientes = new List<DragManager>();
    public EvaluarPuntosPedido evaluadorPuntos;

    public GameObject nuevaImagenUI; // Asigna esta imagen desde el inspector
    public GameObject mensajeErrorUI; // Imagen que se muestra si se supera el límite de clics
    public GameObject Ayuda, BotonCerrar; // Referencias para mostrar y ocultar la ayuda
    public GameObject botonEntregar; // Botón de entregar asignado desde el inspector

    private int contadorClicks = 0; // Lleva el conteo de cuántas veces se ha hecho clic

    private Vector3 posicionOriginal; // Guarda la posición original del plato final

    public bool HaTerminado => contadorClicks >= GameData.numElementos; // Nueva propiedad pública

    void Awake() // Hacemos un Singleton de CocinaManager
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Asignamos numElementos según dificultad
        GameData.ActualizarNumElementos();
    }

    void Start()
    {
        if (Ayuda != null) Ayuda.SetActive(false);
        if (BotonCerrar != null) BotonCerrar.SetActive(false);

        if (nuevaImagenUI != null)
        {
            posicionOriginal = nuevaImagenUI.transform.localPosition; // Guarda la posición original del plato final
            nuevaImagenUI.SetActive(false); // Oculta la imagen al iniciar el juego
        }

        if (mensajeErrorUI != null)
        {
            mensajeErrorUI.SetActive(false); // Oculta el mensaje de error al iniciar
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

        // Guardar los ingredientes actuales en el plato como un string[]
        List<string> ingredientesDelJugador = new List<string>();
        ingredientes.RemoveAll(i => i.fueEliminado && !i.estaDentroDelPlato); // Limpieza de ingredientes eliminados fuera del plato

        foreach (DragManager ingrediente in ingredientes)
        {
            if (ingrediente.estaDentroDelPlato) // Usamos flag en lugar de cálculo manual
            {
                ingredientesDelJugador.Add(ingrediente.nombreIngrediente);
                if (ingredientesDelJugador.Count == 3)
                    break;
            }
        }

        // Si no hay suficientes ingredientes, mostrar advertencia y NO guardar, pero continuar con el resto
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

        // SIEMPRE se deben resetear los ingredientes
        foreach (DragManager ingrediente in ingredientes)
        {
            ingrediente.ResetearIngrediente();
        }

        // SIEMPRE se debe mostrar la imagen animada si existe
        if (nuevaImagenUI != null)
        {
            nuevaImagenUI.SetActive(true);
            nuevaImagenUI.transform.SetAsLastSibling();
            nuevaImagenUI.transform.localPosition = posicionOriginal;
            StartCoroutine(AnimarImagen());
        }
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
            nuevaImagenUI.transform.localPosition = Vector3.Lerp(inicio, posicionFinal, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        nuevaImagenUI.transform.localPosition = posicionFinal;

        // Oculta la imagen
        nuevaImagenUI.SetActive(false);

        // Restablece la posición original
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
        SceneManager.LoadScene(8); // Puedes cambiar a 8 si usas la escena antigua
    }
}
