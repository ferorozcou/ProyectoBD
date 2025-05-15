using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
public class BebidasManager2 : MonoBehaviour
{
    public GameObject Arepas;
    public GameObject Tortillas;
    public GameObject Sopes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Arepas.SetActive(false);
        Tortillas.SetActive(false);
        Sopes.SetActive(false);
        if (GameData.Restaurante == "Venezolano")
        {
            Arepas.SetActive(true);
        }
        if (GameData.Restaurante == "Español")
        {
            Tortillas.SetActive(true);
        }
        if (GameData.Restaurante == "Mexicano")
        {
            Sopes.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
