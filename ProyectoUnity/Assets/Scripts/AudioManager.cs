using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public AudioClip papelRuido; // asignar el clip de audio

    void Awake()
    {
        // singleton: una instancia
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
            audioSource = GetComponent<AudioSource>();

            // suscribirse al evento de carga de escena
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // se llama autom�ticamente cuando se destruye este objeto
    private void OnDestroy()
    {
        // quitar la suscripci�n al evento para evitar errores
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // m�todo llamado cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // reproducir el sonido autom�ticamente al cargar una escena
        PlayPapelRuido();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    // usamos solo un clip as� que este m�todo se puede usar como atajo
    public void PlayPapelRuido()
    {
        PlaySFX(papelRuido);
    }
}
