using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecibirPedidoManager : MonoBehaviour
{
    public GameObject capibara, conejo, gato, koala, pato, fondoVen, fondoMex, fondoEsp, tortillas2, arepas2,sopes2, tortillas1, arepas1, sopes1, tortillas3, arepas3, sopes3, agua, cocacola, sprite, zumo, icetea;
    public GameObject[] animales;
    public TextMeshProUGUI textoMensaje;
    public string[] nombreCliente = { "Capibara", "Conejo", "Gato", "Koala", "Pato" };
    public string[] pedido;
    private string[] mensaje;
    private string bienOmal;
    private int puntos;
    int indiceActual;
    int escena;
    string bebidaCorrecta;

    void EscribirMensaje()
    {
        string v = mensaje[indiceActual];
        textoMensaje.text = v;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indiceActual = 0;
        animales = new GameObject[] { capibara, conejo, gato, koala, pato };;
        capibara.SetActive(false);
        conejo.SetActive(false);
        gato.SetActive(false);
        koala.SetActive(false);
        pato.SetActive(false);
        fondoEsp.SetActive(false);
        fondoVen.SetActive(false);
        fondoMex.SetActive(false);
        arepas2.SetActive(false);
        tortillas2.SetActive(false);
        sopes2.SetActive(false);
        arepas1.SetActive(false);
        tortillas1.SetActive(false);
        sopes1.SetActive(false);
        arepas3.SetActive(false);
        tortillas3.SetActive(false);
        sopes3.SetActive(false);
        agua.SetActive(false);
        cocacola.SetActive(false);
        sprite.SetActive(false);
        zumo.SetActive(false);
        icetea.SetActive(false);
        var db = DBManager.Instance;
        bebidaCorrecta = db.GetBebida(nombreCliente[GameData.clienteNum]);
        if (GameData.Restaurante == "Venezolano")
        {
            fondoVen.SetActive(true);
            escena = 2;
            if (GameData.numElementos == 1)
            {
                arepas1.SetActive(true);
            }
            else if (GameData.numElementos == 3)
            {
                arepas2.SetActive(true);
            }
            else if (GameData.numElementos == 5)
            {
                arepas3.SetActive(true);
            }

        }
        else if (GameData.Restaurante == "Mexicano")
        {
            fondoMex.SetActive(true);
            escena = 3;
            if (GameData.numElementos == 1)
            {
                sopes1.SetActive(true);
            }
            else if (GameData.numElementos == 3)
            {
                sopes2.SetActive(true);
            }
            else if (GameData.numElementos == 5)
            {
                sopes3.SetActive(true);
            }

        }
        else if (GameData.Restaurante == "Español")
        {
            fondoEsp.SetActive(true);
            escena = 4;
            if (GameData.numElementos == 1)
            {
                tortillas1.SetActive(true);
            }
            else if (GameData.numElementos == 3)
            {
                tortillas2.SetActive(true);
            }
            else if (GameData.numElementos == 5)
            {
                tortillas3.SetActive(true);
            }
        }
        else
        {
            Debug.Log("No se encontró el nombre del restaurante");
        }
        if (GameData.bebidaSeleccionada == bebidaCorrecta){
            GameData.puntosPedidoActual = GameData.puntosPedidoActual + 15;
        }
        if(GameData.bebidaSeleccionada== "Cocacola")
        {
            cocacola.SetActive(true);
        }
        else if (GameData.bebidaSeleccionada == "Sprite")
        {
            sprite.SetActive(true);
        }
        else if (GameData.bebidaSeleccionada == "Zumo")
        {
            zumo.SetActive(true);
        }
        else if (GameData.bebidaSeleccionada == "Ice Tea")
        {
            icetea.SetActive(true);
        }
        else if (GameData.bebidaSeleccionada == "Agua")
        {
            agua.SetActive(true);
        }
        else
        {
            Debug.Log("No se encontró el nombre de la bebida");
        }
        puntos = GameData.puntosPedidoActual;
        if (puntos > 99)
        {
            bienOmal = "Wow, está muy bueno";
        }
        else
        {
            bienOmal = "Mmmm, creo que podría estar mejor";
        }

        GameData.puntosNivel = GameData.puntosNivel + puntos;
        mensaje = new string[]{ //Cadena de strings con las instrucciones.
            "Wow que bueno luce todo, veamos que tal está",
        "...",
        $"{bienOmal}" ,
            $"Tu puntaje es {puntos} puntos",
        "Presiona Enter para pasar al siguiente cliente",
                };

        animales[GameData.clienteNum].SetActive(true);
        EscribirMensaje();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SiguienteMensaje();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnteriorMensaje();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && indiceActual == mensaje.Length - 1) //Si hemos llegado a la última línea y presionamos Enter inicia el juego.
        {
            OtroPedido();
        }
    }

    void SiguienteMensaje()
    {
        if (indiceActual < mensaje.Length - 1)
        {
            indiceActual++;
            EscribirMensaje();
        }
    }
    void AnteriorMensaje()
    {
        if (indiceActual > 0)
        {
            indiceActual--;
            EscribirMensaje();
        }
    }
    void OtroPedido()
    {
        var db = DBManager.Instance;
        GameData.NumPediddoActual++;
        if (GameData.NumPediddoActual > db.ObtenerPedidosPorNivel(GameData.Nivel))
        {
            if (GameData.Nivel == 3)
            {
                if (GameData.puntosNivel >= db.ObtenerPuntosRequeridosPorNivel(GameData.Nivel))
                {
                    SceneManager.LoadScene(11);
                }
                else SceneManager.LoadScene(10);
            }
            else SceneManager.LoadScene(10);
        }
        else { SceneManager.LoadScene(escena); }
    }

}
