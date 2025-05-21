using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recompensa : MonoBehaviour
{
    public GameObject Ven, Mex, Esp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ven.SetActive(false);
        Mex.SetActive(false);   
        Esp.SetActive(false);
        if (GameData.Restaurante == "Venezolano")
        {
            Ven.SetActive (true);
        }
        else if (GameData.Restaurante == "Mexicano")
        {
            Mex.SetActive (true);
        }
        else if (GameData.Restaurante == "Español")
        {
            Esp.SetActive (true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SiguienteRestaurante()
    {
        if (GameData.numRestaurante == 3)
        {
            SceneManager.LoadScene(12);
        }
        else
        {
            if (GameData.Restaurante == "Venezolano")
            {
                GameData.Restaurante = "Español";
            }
            else if (GameData.Restaurante == "Mexicano")
            {
                GameData.Restaurante = "Venezolano";

            }
            else if (GameData.Restaurante == "Español")
            {
                GameData.Restaurante = "Mexicano";
            }
            GameData.numRestaurante++;
            SceneManager.LoadScene(1);
        }
    }
}
