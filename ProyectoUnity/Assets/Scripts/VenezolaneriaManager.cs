
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VenezolaneriaManager : MonoBehaviour
{
    //Game objects para cada elemento que necesitamos.
    public GameObject jefe;
    public GameObject bocadillo;
    //Objeto tipo TextMeshPro.
    public TextMeshProUGUI textoInstrucciones;
    public string nombreRest;
    public int escena;
    public string plato;
    public char a_o;

    private string[] instrucciones;

    private int indiceActual = 0; //int para guardar el �ndice de la l�nea en la que nos encontramos.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameData.Restaurante == "Venezolano")
        {
            nombreRest = "Marico's Venezolaneria";
            plato = "arepa";
            a_o = 'a';
            escena = 2;

        }
        else if (GameData.Restaurante == "Mexicano")
        {
            nombreRest = "Wey's Mexicaneria";
            plato = "sope";
            a_o = 'o';
            escena = 3;

        }
        else if (GameData.Restaurante == "Espa�ol")
        {
            nombreRest = "Tio's Espa�oleria";
            plato = "tortilla";
            a_o = 'a';
            escena = 4;
        }
        else
        {
            Debug.Log("No se encontr� el nombre del restaurante");
        }
        instrucciones = new string[]{ //Cadena de strings con las instrucciones.
            $"�Bienvenido a {nombreRest} �Qu� desea pedir?",
        "�C�mo? �Que no vienes a pedir nada?",
        "�Ah, que eres el nuevo! �Qu� despiste! Encantado de conocerte. Soy Carlino, tu jefe y el due�o de este imperio de restaurantes. " +
            "Ver�s... te explico un poco c�mo van las cosas por aqu�.",
        "A lo largo del d�a, ir�n llegando diversos clientes, cada uno con un pedido distinto. Tu labor es atenderles y preparar dichos pedidos.",
        "Cada pedido estar� conformado de un" + a_o + " o vari" + a_o + "s " + plato + "s, as� como una bebida. Arrastra al plato los ingredientes de cada " + plato + " y haz click sobre el bot�n" +
            " con el icono del fuego cuando est� list" + a_o + ".",
        "Si te equivocas de ingredientes puedes tirarlos a la basura. Sin embargo, recuerda que el desperdicio de alimentos supone una gran p�rdida para el restaurante, as� que" +
            " ser�s penalizado y perder�s puntos. Pero bueno, como es tu primer d�a haremos la excepci�n y hoy no tendr� consecuencias.",
        "Cada cliente siempre pedir� la misma bebida. Aseg�rate de recordar cu�l pide cada uno porque al principio te lo dir�n, pero poco a poco dejar�n de hacerlo.",
        "Una vez tengas tod" + a_o + "s l" + a_o + "s " + plato + "s y la bebida preparad" + a_o + "s haz click sobre el bot�n con el tick para entregarl" + a_o + "s.",
        "Eso es todo. �Mucha suerte, joven! Presiona enter si est�s listo."
                };



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
        else if (Input.GetKeyDown(KeyCode.Return)) //Si hemos llegado a la �ltima l�nea y presionamos Enter inicia el juego.
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

        SceneManager.LoadScene(escena);
    }
}
