using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MexClientesManager : MonoBehaviour
{
    
    public GameObject capibara, conejo, gato, koala, pato;
    public GameObject[] animales;
    public PedidoGenerator GeneradorPedidos;
    public TextMeshProUGUI textoPedido;
    public string[] nombreCliente = { "Capibara", "Conejo", "Gato", "Koala", "Pato" };
    public int indice = -1;
    public int dif = 2;
    public string[] pedido;
    public int Facil;
    public int Medio;
    public int Dificil;
    public int nivel = GameData.Nivel;
    void EscribirPedido()
    {
        string v = pedido[indice];
        textoPedido.text = v;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var dbManager = DBManager.Instance;
        animales = new GameObject[] { capibara, conejo, gato, koala, pato };
        capibara.SetActive(false);
        conejo.SetActive(false);
        gato.SetActive(false);
        koala.SetActive(false);
        pato.SetActive(false);
        int clientenum = UnityEngine.Random.Range(0, 5);
        animales[clientenum].SetActive(true);
        GameData.cliente = nombreCliente[clientenum];
        GameData.clienteNum = clientenum;
        textoPedido.text = $"Hola soy {nombreCliente[clientenum]}";
        pedido = GeneradorPedidos.GenerarPedido(dif, 1, nombreCliente[clientenum],nivel);


        if (nivel == 1)
        {
            Facil = dbManager.GetCantidadPedidosPorNivelYTipo(GameData.Nivel, "Fácil");
            if (GameData.NumPediddoActual <= Facil)
            {
                pedido = GeneradorPedidos.GenerarPedido(0, 1,nombreCliente[clientenum], nivel);
            }
            else
            {
                pedido = GeneradorPedidos.GenerarPedido(1, 1, nombreCliente[clientenum], nivel);
            }
        }
        else
        {
            Facil = dbManager.GetCantidadPedidosPorNivelYTipo(nivel, "Fácil");
            Medio = dbManager.GetCantidadPedidosPorNivelYTipo(nivel, "Medio");
            Dificil = dbManager.GetCantidadPedidosPorNivelYTipo(nivel, "Difícil");
            if (GameData.NumPediddoActual <= Facil)
            {
                pedido = GeneradorPedidos.GenerarPedido(0, 1, nombreCliente[clientenum], nivel);
            }
            else if (GameData.NumPediddoActual <= Facil + Medio)
            {
                pedido = GeneradorPedidos.GenerarPedido(1, 1, nombreCliente[clientenum], nivel);
            }
            else
            {
                pedido = GeneradorPedidos.GenerarPedido(2, 1, nombreCliente[clientenum], nivel);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SiguienteElemento();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AnterioElemento();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) //Si hemos llegado a la última línea y presionamos Enter inicia el juego.
        {
            Cocinar();
        }
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
