using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class EspaClientesManager : MonoBehaviour
{
    public GameObject  capibara, conejo, gato, koala, pato;
    public GameObject[] animales;
    public PedidoGenerator GeneradorPedidos;
   


    void EscribirPedido()
    {

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animales = new GameObject[] { capibara, conejo, gato, koala, pato };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
