using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class MexicaneriaManager : MonoBehaviour
{
    //Game objects para cada elemento que necesitamos.
    public GameObject jefe; 
    public GameObject bocadillo;
    //Objeto tipo TextMeshPro.
    public TextMeshProUGUI textoInstrucciones; 

    private string[] instrucciones = { //Cadena de strings con las instrucciones.
        "�Bienvenido a Wey's Mexicaneria! �Qu� desea pedir?",
        "�C�mo? �Que no vienes a pedir nada?",
        "�Ah, que eres el nuevo! �Qu� despiste! Encantado de conocerte. Soy Carlino, tu jefe y el due�o de este imperio de restaurantes. " +
            "Ver�s... te explico un poco c�mo van las cosas por aqu�.",
        "A lo largo del d�a, ir�n llegando diversos clientes, cada uno con un pedido distinto. Tu labor es atenderles y preparar dichos pedidos.",
        "Cada pedido estar� conformado de una o varios sopes, as� como una bebida. Arrastra al plato los ingredientes de cada sope y haz click sobre el bot�n" +
            " con el icono del fuego cuando est� lista.",
        "Si te equivocas de ingredientes puedes tirarlos a la basura. Sin embargo, recuerda que el desperdicio de alimentos supone una gran p�rdida para el restaurante�, as� que" +
            " ser�s penalizado y perder�s puntos.",
        "Cada cliente siempre pedir� la misma bebida. Aseg�rate de recordar cu�l pide cada uno porque al principio te lo dir�n, pero poco a poco dejar�n de hacerlo.",
        "Una vez tengas todas los sopes y la bebida preparados haz click sobre el bot�n con el tick para entregarlas.",
        "Eso es todo. �Mucha suerte, joven! Presiona enter si est�s listo."
    };

    private int indiceActual = 0; //int para guardar el �ndice de la l�nea en la que nos encontramos.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActualizarTexto(); //Mostramos la primera l�nea de instrucciones
    }

    // Update is called once per frame
    void Update()
    {

        //Si presionamos las flechas pasamos de instrucci�n.
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            SiguienteInstruccion();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnteriorInstruccion();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && indiceActual == instrucciones.Length - 1) //Si hemos llegado a la �ltima l�nea y presionamos Enter inicia el juego.
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
        SceneManager.LoadScene(5);
    }
}

