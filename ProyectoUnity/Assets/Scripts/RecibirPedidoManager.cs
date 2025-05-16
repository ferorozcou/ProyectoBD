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
    public TextMeshProUGUI textoPedido;
    public string[] nombreCliente = { "Capibara", "Conejo", "Gato", "Koala", "Pato" };
    int indice = -1;
    int dif = 1;
    public string[] pedido;
    int nivel = 1;

    void EscribirPedido()
    {
        string v = pedido[indice];
        textoPedido.text = v;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animales = new GameObject[] { capibara, conejo, gato, koala, pato };
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
        if (GameData.Restaurante == "Venezolano")
        {
            fondoVen.SetActive(true);
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

        animales[GameData.clienteNum].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SiguienteElemento()
    {
        if (indice < pedido.Length - 1)
        {
            indice++;
            EscribirPedido();
        }
    }
    void AnterioElemento()
    {
        if (indice > 0)
        {
            indice--;
            EscribirPedido();
        }
    }
    void Cocinar()
    {
        SceneManager.LoadScene(6);
    }

}
