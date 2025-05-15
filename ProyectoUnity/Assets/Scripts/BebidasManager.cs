using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class BebidasManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; //Componente rect transform (con la posici�n del objeto)
    private CanvasGroup canvasGroup; //Controla cosas como visibilidad y raycasts
    private Vector3 posicionAnterior; //Almacena la posici�n anterior v�lida (por ejemplo, dentro del plato)
    private Vector3 escalaInicial; //Almacena la escala original para resetearla tras reducirla a 0
    private Vector3 posicionOriginal; //Almacena la posicion inicial incluso si lo hemos soltado en el plato

    public RectTransform transformBandeja; //El RectTransform del objeto plato
    public Canvas canvas; //Objeto para el canvas que necesitamos para convertir las coordenadas correctamente

    GameObject Arepas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); //Obtenemos y almacenamos en la variable el RectTransform del objeto
        canvasGroup = GetComponent<CanvasGroup>(); //Obtenemos el canvas group

        if (canvasGroup == null) //Si no existe un canvas group lo creamos
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        escalaInicial = rectTransform.localScale; //Guardamos la escala original al inicio
        posicionAnterior = rectTransform.position; //Guardamos la posici�n inicial del objeto
        posicionOriginal = posicionAnterior; //Guardamos la posici�n original del objeto
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); //Mueve el objeto a la capa de delante del todo en el canvas
        canvasGroup.blocksRaycasts = false; //Permitimos que el plato o papelera reciban eventos de click para detectar si hemos soltado el rat�n
        posicionAnterior = rectTransform.position; //Guardamos la �ltima posici�n v�lida antes de arrastrar
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle( //M�todo para convertir la posici�n del rat�n a una dentro del canvas para que el arrastre sea correcto en cualquier escala
            canvas.transform as RectTransform, //RectTransform del Canvas donde est� la UI
            eventData.position, //Posici�n del rat�n en la pantalla
            canvas.worldCamera, //C�mara que usamos para la conversi�n de coordenadas
            out Vector2 localPoint //Variable de salida donde se guarda el resultado en coordenadas locales
        );

        rectTransform.localPosition = localPoint; //Igualamos la posici�n a la del rat�n
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //Ya no recibe eventos de click
        //Si el ingrediente est� completamente dentro del plato
        if (EstaCompletamenteDentro(rectTransform, transformBandeja))
        {
            Debug.Log("Ingrediente soltado completamente dentro del plato");
            posicionAnterior = rectTransform.position; //Actualizamos la posici�n v�lida m�s reciente
            if (rectTransform.localScale == escalaInicial)
            {
                rectTransform.localScale += Vector3.one * 0.9f; // Aumentamos su escala en 0.7
            }

        }
        else //Si no se suelta sobre la papelera o el plato vuelve a su �ltima posici�n v�lida
        {
            rectTransform.position = posicionAnterior;
        }
    }


    private bool EstaCompletamenteDentro(RectTransform peque�o, RectTransform grande) //Comprueba si un RectTransform est� completamente dentro de otro
    {
        //Arrays con las posiciones de las 4 esquinas de cada elemento
        Vector3[] esquinasPeque�o = new Vector3[4];
        Vector3[] esquinasGrande = new Vector3[4];

        peque�o.GetWorldCorners(esquinasPeque�o); //Obtenemos las esquinas del objeto peque�o y las metemos en el array
        grande.GetWorldCorners(esquinasGrande); //Obtenemos las esquinas del objeto grande y las metemos en el array

        //Solo necesitamos estas 2 esquinas para comprobar la posici�n
        Vector3 esquinaInferiorIzquierdaGrande = esquinasGrande[0];
        Vector3 esquinaSuperiorDerechaGrande = esquinasGrande[2];

        //Comprobamos si cada esquina del peque�o est� dentro del rect�ngulo del grande
        foreach (Vector3 esquina in esquinasPeque�o)
        {
            if (esquina.x < esquinaInferiorIzquierdaGrande.x || esquina.x > esquinaSuperiorDerechaGrande.x ||
                esquina.y < esquinaInferiorIzquierdaGrande.y || esquina.y > esquinaSuperiorDerechaGrande.y)
            {
                return false; //Al menos una esquina est� fuera
            }
        }

        return true; //Todas las esquinas est�n dentro
    }
}

