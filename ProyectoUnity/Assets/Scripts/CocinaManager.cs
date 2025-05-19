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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Se presionó la tecla B");
            evaluadorPuntos.EvaluarPedidos();
        }
    }

    public void RegistrarIngrediente(DragManager ingrediente) // Agregamos los ingredientes del plato a la lista
    {
        ingredientes.Add(ingrediente);
    }

    public void ResetearTodo()
    {
        bool puedeGuardar = contadorClicks < numeroElementos;
        int clicks = 0;

        // Si ya se alcanzó el límite, mostramos el mensaje de error, pero igual continuamos con el resto del reseteo
        if (!puedeGuardar && mensajeErrorUI != null)
        {
            mensajeErrorUI.SetActive(true);
            mensajeErrorUI.transform.SetAsLastSibling(); // Lo ponemos al frente de todo
        }

        if (puedeGuardar)
        {
            // Guardar los ingredientes actuales en el plato como un string[]
            List<string> ingredientesDelJugador = new List<string>();
            ingredientes.RemoveAll(i => i.fueEliminado && !i.estaDentroDelPlato);

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


            clicks++;
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
        SceneManager.LoadScene(8); // Puedes cambiar a 8 si usas la escena antigua
    }
}
