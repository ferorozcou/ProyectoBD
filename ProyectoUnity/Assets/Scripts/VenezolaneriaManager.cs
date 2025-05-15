using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class VenezolaneriaManager : MonoBehaviour
{
    //Game objects para cada elemento que necesitamos.
    public GameObject jefe; 
    public GameObject bocadillo;
    //Objeto tipo TextMeshPro.
    public TextMeshProUGUI textoInstrucciones; 

    private string[] instrucciones = { //Cadena de strings con las instrucciones.
        "¡Bienvenido a Marico's Venezolaneria! ¿Qué desea pedir?",
        "¿Cómo? ¿Que no vienes a pedir nada?",
        "¡Ah, que eres el nuevo! ¡Qué despiste! Encantado de conocerte. Soy Carlino, tu jefe y el dueño de este imperio de restaurantes. " +
            "Verás... te explico un poco cómo van las cosas por aquí.",
        "A lo largo del día, irán llegando diversos clientes, cada uno con un pedido distinto. Tu labor es atenderles y preparar dichos pedidos.",
        "Cada pedido estará conformado de una o varias arepas, así como una bebida. Arrastra al plato los ingredientes de cada arepa y haz click sobre el botón" +
            " con el icono del fuego cuando esté lista.",
        "Si te equivocas de ingredientes puedes tirarlos a la basura. Sin embargo, recuerda que el desperdicio de alimentos supone una gran pérdida para el restaurante´, así que" +
            " serás penalizado y perderás puntos.",
        "Cada cliente siempre pedirá la misma bebida. Asegúrate de recordar cuál pide cada uno porque al principio te lo dirán, pero poco a poco dejarán de hacerlo.",
        "Una vez tengas todas las arepas y la bebida preparadas haz click sobre el botón con el tick para entregarlas.",
        "Eso es todo. ¡Mucha suerte, joven! Presiona enter si estás listo."
    };

    private int indiceActual = 0; //int para guardar el índice de la línea en la que nos encontramos.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActualizarTexto(); //Mostramos la primera línea de instrucciones
    }

    // Update is called once per frame
    void Update()
    {

        //Si presionamos las flechas pasamos de instrucción.
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            SiguienteInstruccion();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnteriorInstruccion();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && indiceActual == instrucciones.Length - 1) //Si hemos llegado a la última línea y presionamos Enter inicia el juego.
        {
            IniciarJuego();
        }
    }

    //Funciones para pasar de instrucciones.
    void SiguienteInstruccion() 
    {
        if (indiceActual < instrucciones.Length - 1)
        {
            indiceActual++;
            ActualizarTexto();
        }
    }

    void AnteriorInstruccion()
    {
        if (indiceActual > 0)
        {
            indiceActual--;
            ActualizarTexto();
        }
    }

    //Funcion para ir cambiando el texto.
    void ActualizarTexto()
    {
        string v = instrucciones[indiceActual];
        textoInstrucciones.text = v;
    }

    void IniciarJuego()
    {
        SceneManager.LoadScene(4);
    }
}
