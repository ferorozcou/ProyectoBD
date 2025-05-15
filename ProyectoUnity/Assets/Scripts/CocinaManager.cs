using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CocinaManager : MonoBehaviour
{
    public static CocinaManager Instance;
    private List<DragManager> ingredientes = new List<DragManager>();

    public GameObject nuevaImagenUI; // Asigna esta imagen desde el inspector

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
{
    if (nuevaImagenUI != null)
        nuevaImagenUI.SetActive(false); // Oculta la imagen al iniciar el juego
}

    public void RegistrarIngrediente(DragManager ingrediente)
    {
        if (!ingredientes.Contains(ingrediente))
            ingredientes.Add(ingrediente);
    }

    public void ResetearTodo()
    {
        foreach (DragManager ingrediente in ingredientes)
        {
            ingrediente.ResetearIngrediente();
        }

        if (nuevaImagenUI != null)
            nuevaImagenUI.SetActive(true); // Muestra la nueva imagen en la UI
    }
}
