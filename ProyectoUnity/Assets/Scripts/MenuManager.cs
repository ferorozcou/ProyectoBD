using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
    public void CargarNivel(int indexEscena)
    {
        GameData.Nivel = 1;
        if (indexEscena == 1) {
            GameData.Restaurante = "Venezolano";
        }
        if (indexEscena == 2) {
            GameData.Restaurante = "Mexicano";
        }
        if (indexEscena == 3)
        {
            GameData.Restaurante = "Español";
        }
        SceneManager.LoadScene(1);
    }
}

