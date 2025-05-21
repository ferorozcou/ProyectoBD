using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //Hereda de esas clases que definen qué pasa cuando empiezas, mientras drageas y cuando terminas.
{
    private RectTransform rectTransform; //Componente rect transform (con la posición del objeto)
    private CanvasGroup canvasGroup; //Controla cosas como visibilidad y raycasts
    private Vector3 posicionAnterior; //Almacena la posición anterior válida (por ejemplo, dentro del plato)
    private Vector3 escalaInicial; //Almacena la escala original para resetearla tras reducirla a 0
    private Vector3 posicionOriginal; //Almacena la posicion inicial incluso si lo hemos soltado en el plato
    public RectTransform transformPlato; //El RectTransform del objeto plato
    public RectTransform transformPapelera; //El RectTransform del objeto papelera
    public Canvas canvas; //Objeto para el canvas que necesitamos para convertir las coordenadas correctamente

    public string nombreIngrediente; //Variable para los nombres de los ingredientes (así nos aseguramos de que coincida con los de la base de datos)

    public bool estaDentroDelPlato { get; private set; }

    public int indicePlato;

    public bool fueEliminado { get; private set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //Obtenemos y almacenamos en la variable el RectTransform del objeto
        canvasGroup = GetComponent<CanvasGroup>(); //Obtenemos el canvas group

        if (canvasGroup == null) //Si no existe un canvas group lo creamos
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        escalaInicial = rectTransform.localScale; //Guardamos la escala original al inicio
        posicionAnterior = rectTransform.position; //Guardamos la posición inicial del objeto
        posicionOriginal = posicionAnterior; //Guardamos la posición original del objeto
    }

    void Start()
    {
        CocinaManager.Instance.RegistrarIngrediente(this);
    }

    public void ResetearIngrediente()
    {
        fueEliminado = false;
        estaDentroDelPlato = false; //Ya no está dentro del plato
        rectTransform.position = posicionOriginal;
        rectTransform.localScale = escalaInicial;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CocinaManager.Instance != null && CocinaManager.Instance.HaTerminado)
        {
            Debug.Log("Ya no se puede arrastrar.");
            return;
        }

        transform.SetAsLastSibling(); //Mueve el objeto a la capa de delante del todo en el canvas
        canvasGroup.blocksRaycasts = false; //Permitimos que el plato o papelera reciban eventos de click para detectar si hemos soltado el ratón
        posicionAnterior = rectTransform.position; //Guardamos la última posición válida antes de arrastrar
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle( //Método para convertir la posición del ratón a una dentro del canvas para que el arrastre sea correcto en cualquier escala
            canvas.transform as RectTransform, //RectTransform del Canvas donde está la UI
            eventData.position, //Posición del ratón en la pantalla
            canvas.worldCamera, //Cámara que usamos para la conversión de coordenadas
            out Vector2 localPoint //Variable de salida donde se guarda el resultado en coordenadas locales
        );

        rectTransform.localPosition = localPoint; //Igualamos la posición a la del ratón
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //Ya no recibe eventos de click

        //Si el ratón está sobre la papelera
        if (RectTransformUtility.RectangleContainsScreenPoint(transformPapelera, Input.mousePosition, canvas.worldCamera))
        {
            GameData.puntosPapelera -= 15;
            Debug.Log("Ingrediente tirado a la papelera. PuntosPapelera = " + GameData.puntosPapelera);
            fueEliminado = true; // Bool que regula si eliminamos o no el ingrediente
            estaDentroDelPlato = false; // Ya no está en el plato
            CocinaManager.Instance.EliminarIngrediente(this); // Lo quitamos del array de CocinaManager
            StartCoroutine(ReduceAndReset()); //Iniciamos la animación para reducir su tamaño y que luego vuelva al original
        }
        //Si el ingrediente está completamente dentro del plato
        else if (EstaCompletamenteDentro(rectTransform, transformPlato))
        {
            Debug.Log("Ingrediente soltado completamente dentro del plato");
            posicionAnterior = rectTransform.position; //Actualizamos la última posición válida
            if (rectTransform.localScale == escalaInicial)
            {
                rectTransform.localScale += Vector3.one * 0.7f; //Aumentamos su escala en 0.7
            }

            estaDentroDelPlato = true;
            fueEliminado = false; //Para que el ingrediente se vuelva a habilitar si lo arrastramos otra vez al plato
            CocinaManager.Instance.RegistrarIngrediente(this); // Volvemos a agregarlo si no está
        }
        else //Si no se suelta sobre la papelera o el plato vuelve a su última posición válida
        {
            rectTransform.position = posicionAnterior;
        }
    }

    private IEnumerator ReduceAndReset() //Corrutina que reduce la escala a cero, espera un momento y luego resetea posición y escala
    {
        rectTransform.localScale = Vector3.zero; //Reducimos el tamaño a 0
        yield return new WaitForSeconds(0.5f); //Espera
        rectTransform.position = posicionOriginal; //Volvemos a la posición del principio
        rectTransform.localScale = escalaInicial; //Reseteamos a la escala original
    }

    private bool EstaCompletamenteDentro(RectTransform pequeño, RectTransform grande) //Comprueba si un RectTransform está completamente dentro de otro
    {
        //Arrays con las posiciones de las 4 esquinas de cada elemento
        Vector3[] esquinasPequeño = new Vector3[4];
        Vector3[] esquinasGrande = new Vector3[4];

        pequeño.GetWorldCorners(esquinasPequeño); //Obtenemos las esquinas del objeto pequeño y las metemos en el array
        grande.GetWorldCorners(esquinasGrande); //Obtenemos las esquinas del objeto grande y las metemos en el array

        //Solo necesitamos estas 2 esquinas para comprobar la posición
        Vector3 esquinaInferiorIzquierdaGrande = esquinasGrande[0];
        Vector3 esquinaSuperiorDerechaGrande = esquinasGrande[2];

        //Comprobamos si cada esquina del pequeño está dentro del rectángulo del grande
        foreach (Vector3 esquina in esquinasPequeño)
        {
            if (esquina.x < esquinaInferiorIzquierdaGrande.x || esquina.x > esquinaSuperiorDerechaGrande.x ||
                esquina.y < esquinaInferiorIzquierdaGrande.y || esquina.y > esquinaSuperiorDerechaGrande.y)
            {
                return false; //Al menos una esquina está fuera
            }
        }

        return true; //Todas las esquinas están dentro
    }
}
