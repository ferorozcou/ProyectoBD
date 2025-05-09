using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void CargarNivel(int indexEscena)
    {
        SceneManager.LoadScene(indexEscena);
    }
}

