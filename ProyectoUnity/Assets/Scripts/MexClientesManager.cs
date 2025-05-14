using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class MexClientesManager : MonoBehaviour
{
    public GameObject capibara, conejo, gato, koala, pato;
    public GameObject[] animales;
    public PedidoGenerator GeneradorPedidos;
    public TextMeshProUGUI textoPedido;
    public string[] nombreCliente = { "capibara", "conejo", "gato", "koala", "pato" };
    int indice = -1;
    int dif = 1;
    void EscribirPedido()
    {
        string[] pedido = GeneradorPedidos.GenerarPedido(dif, 1);
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
        int clientenum = Random.Range(0, 5);
        animales[clientenum].SetActive(true);
        textoPedido.text = $"Hola soy {nombreCliente[clientenum]}";

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
        indice++;
        EscribirPedido();
    }
    void AnterioElemento()
    {
        indice--;
        EscribirPedido();
    }
    void Cocinar()
    {
        SceneManager.LoadScene(8);
    }
}
