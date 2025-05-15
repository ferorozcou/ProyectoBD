using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
public class VenClientesManager : MonoBehaviour
{
    public GameObject capibara, conejo, gato, koala, pato;
    public GameObject[] animales;
    public PedidoGenerator GeneradorPedidos;
    public TextMeshProUGUI textoPedido;
    public string[] nombreCliente = { "Capibara", "Conejo", "Gato", "Koala", "Pato" };
    int indice = -1;
    int dif = 1;
    int nivel = 1;
    public string[] pedido;
    
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
        int clientenum = UnityEngine.Random.Range(0, 5);
        animales[clientenum].SetActive(true);
        textoPedido.text = $"Hola soy {nombreCliente[clientenum]}";
        pedido = GeneradorPedidos.GenerarPedido(dif, 0, nombreCliente[clientenum],nivel);

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
        else if (Input.GetKeyDown(KeyCode.Return)) //Si hemos llegado a la �ltima l�nea y presionamos Enter inicia el juego.
        {
            Cocinar();
        }
    }

    void SiguienteElemento()
    {
        if (indice < pedido.Length-1)
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
        SceneManager.LoadScene(9);
    }
}
