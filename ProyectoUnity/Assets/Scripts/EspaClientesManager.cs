using JetBrains.Annotations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EspaClientesManager : MonoBehaviour
{
    public GameObject capibara, conejo, gato, koala, pato;
    public GameObject[] animales;
    public PedidoGenerator GeneradorPedidos;
    public TextMeshProUGUI textoPedido;
    public string[] nombreCliente = { "Capibara", "Conejo", "Gato", "Koala", "Pato" };
    int indice = -1;
    public string[] pedido;
    public int nivel = GameData.Nivel;
    public int Facil;
    public int Medio;
    public int Dificil;
    void EscribirPedido()
    {
        string v = pedido[indice];
        textoPedido.text = v;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return null; // Espera un frame para asegurar que DBManager está listo

        if (DBManager.Instance == null)
        {
            Debug.LogError("DBManager.Instance no está disponible");
            yield break;
        }
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

        if (nivel == 1)
        {
            Facil = dbManager.GetCantidadPedidosPorNivelYTipo(GameData.Nivel, "Fácil");
            if (GameData.NumPediddoActual<= Facil)
            {
                pedido = GeneradorPedidos.GenerarPedido(0, 2, nombreCliente[clientenum], nivel);
            }
            else
            {
                pedido= GeneradorPedidos.GenerarPedido(1, 2, nombreCliente[clientenum], nivel);
            }
        }
        else
        {
            Facil = dbManager.GetCantidadPedidosPorNivelYTipo(nivel, "Fácil");
            Medio = dbManager.GetCantidadPedidosPorNivelYTipo(nivel, "Medio");
            Dificil = dbManager.GetCantidadPedidosPorNivelYTipo(nivel, "Difícil");
            if (GameData.NumPediddoActual <= Facil)
            {
                pedido = GeneradorPedidos.GenerarPedido(0, 2, nombreCliente[clientenum], nivel);
            }
            else if(GameData.NumPediddoActual <= Facil + Medio){
                pedido = GeneradorPedidos.GenerarPedido(1, 2, nombreCliente[clientenum], nivel);
            }
            else
            {
                pedido = GeneradorPedidos.GenerarPedido(2, 2, nombreCliente[clientenum], nivel);
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
        SceneManager.LoadScene(5);
    }

}
