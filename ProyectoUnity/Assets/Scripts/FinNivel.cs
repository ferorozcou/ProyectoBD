using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinNivel : MonoBehaviour
{
    int escena;
    int nuevoNivel;
    public GameObject FondoBien, FondoDefeat, BotonNextLevel, BotonRepetir, BotonSalir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameData.Restaurante == "Venezolano")
        {
            escena = 2;

        }
        else if (GameData.Restaurante == "Mexicano")
        {
            escena = 3;
        }
        else if (GameData.Restaurante == "Español")
        {
            escena = 4;
        }
        BotonSalir.SetActive(true);
        FondoBien.SetActive(false);
        FondoDefeat.SetActive(false);
        BotonNextLevel.SetActive(false);
        BotonRepetir.SetActive(false);
        var db = DBManager.Instance;
        if (GameData.puntosNivel>= db.ObtenerPuntosRequeridosPorNivel(GameData.Nivel))
        {
            FondoBien.SetActive(true);
            BotonNextLevel.SetActive(true);
            GameData.Nivel++;
        }
        else
        {
            FondoDefeat.SetActive(true);
            BotonRepetir.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pasarNivel()
    {
        GameData.NumPediddoActual=0;
        SceneManager.LoadScene(escena);
    }
    public void repetirNivel()
    {
        GameData.NumPediddoActual = 0;
        SceneManager.LoadScene(escena);
    }
    public void Salir()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
