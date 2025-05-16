using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
public class BebidasManager2 : MonoBehaviour
{
    public GameObject Arepas1;
    public GameObject Arepas2;
    public GameObject Arepas3;
    public GameObject Tortillas1;
    public GameObject Tortillas2;
    public GameObject Tortillas3;
    public GameObject Sopes1;
    public GameObject Sopes2;
    public GameObject Sopes3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Arepas1.SetActive(false);
        Arepas2.SetActive(false);
        Arepas3.SetActive(false);
        Tortillas1.SetActive(false);
        Tortillas2.SetActive(false);
        Tortillas3.SetActive(false);
        Sopes1.SetActive(false);
        Sopes2.SetActive(false);
        Sopes3.SetActive(false);
        if (GameData.Restaurante == "Venezolano")
        {
            if (GameData.numElementos == 1)
            {
                Arepas1.SetActive(true);
            }
            else if (GameData.numElementos == 3)
            {
                Arepas2.SetActive(true);
            }
            else if (GameData.numElementos == 5)
            {
                Arepas3.SetActive(true);
            }
        }
        else if (GameData.Restaurante == "Español")
        {
            if (GameData.numElementos == 1)
            {
                Tortillas1.SetActive(true);
            }
            else if (GameData.numElementos == 3)
            {
                Tortillas2.SetActive(true);
            }
            else if (GameData.numElementos == 5)
            {
                Tortillas3.SetActive(true);
            }
        }
        else if (GameData.Restaurante == "Mexicano")
        {
            if (GameData.numElementos == 1)
            {
                Sopes1.SetActive(true);
            }
            else if (GameData.numElementos == 3)
            {
                Sopes2.SetActive(true);
            }
            else if (GameData.numElementos == 5)
            {
                Sopes3.SetActive(true);
            }
        }
        else
        {
            Debug.Log($"No se encontró el plato :(, El restaurante era {GameData.Restaurante} y el numero de elementos era {GameData.numElementos}");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PasarBebidas()
    {
        SceneManager.LoadScene(9);
    }
}
