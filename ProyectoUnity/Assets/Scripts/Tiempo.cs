using UnityEngine;
using TMPro; // Permite usar texto TextMeshPro
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer instancia; // Declara un singleton (una única instancia de clase, global) para se usado entre escenas.

    public float tiempoLimite = 120f; // Valor por defecto 
    private float tiempoRestante;

    public TextMeshProUGUI textoTemporizador; // UI donde se muestra el tiempo
    public GameObject mensajeTiempoTerminado; // Panel que aparece cuando se acaba el tiempo

    private int ultimoSegundoMostrado = -1; // Guarda el ultimo segundo mostrado para evitar que se impriman los milisegundos.
    private bool tiempoAgotado = false;

    void Awake()
    {
        // Si ya existe otra instancia de LevelTimer que no sea esta...
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); //...la destruye para no duplicarla.
            return;
        }

        // Si no existe, este será la instancia global
        instancia = this;

        DontDestroyOnLoad(gameObject); // No se destruye entre escenas

        // Se suscribe al evento que se activa cuando se carga una nueva escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        InicializarTiempo(); // Comienza el tiempo
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Se desuscribe al destruirse
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Cuando se carga una escena nueva...
    {
        // Buscamos el texto del temporizador de la nueva escena
        if (textoTemporizador == null)
            textoTemporizador = GameObject.Find("TextoTemporizador")?.GetComponent<TextMeshProUGUI>();

        // Buscamos el mensaje que indica que se ha acabado el tiempo
        if (mensajeTiempoTerminado == null)
            mensajeTiempoTerminado = GameObject.Find("MensajeTiempoAcabado");

        ActualizarTextoTemporizador(); // Forzar actualización al cargar nueva escena
    }

    void InicializarTiempo()
    {
        // Si ya había tiempo guardado, se usa ese. Si no, se obtiene uno nuevo
        tiempoRestante = (GameData.tiempoRestante >= 0.5f) // <-- agregado: protección contra valores muy bajos
            ? GameData.tiempoRestante
            : GameData.ObtenerTiempoLimite();

        GameData.tiempoRestante = tiempoRestante; // Se guarda el tiempo en gameData

        tiempoAgotado = false; // <-- agregado: reiniciamos flag de agotado

        // Ocultamos mensaje de tiempo acabado
        if (mensajeTiempoTerminado != null)
            mensajeTiempoTerminado.SetActive(false);

        //Muestra el tiempo en pantalla
        ActualizarTextoTemporizador();
    }

    void Update()
    {
        if (tiempoRestante > 0 && !tiempoAgotado) // Si el tiempo no se ha agotado...
        {
            tiempoRestante -= Time.deltaTime; // Restamos el tiempo que pasó desde el último frame

            GameData.tiempoRestante = tiempoRestante; // Actualiza GameData con el nuevo tiempo

            int segundoActual = Mathf.FloorToInt(tiempoRestante); // Redondea el tiempo

            // Con este if evitamos mostrar el tiempo cada frame, si no cada segundo.
            if (segundoActual != ultimoSegundoMostrado)
            {
                Debug.Log("Tiempo restante: " + segundoActual);
                ultimoSegundoMostrado = segundoActual; // Guardamos este segundo para no repetir
                ActualizarTextoTemporizador(); // Actualiza el texto temporizador.
            }

            if (tiempoRestante <= 0) // Si el tiempo llega a 0,,
            {
                tiempoRestante = 0;
                TiempoTerminado(); // Mostramos el mensaje que avisa que el tiempo se ha acabado
            }
        }
    }

    void TiempoTerminado() // Muestra el mensaje de que se acabo el tiempo 
    {
        tiempoAgotado = true;
        Debug.Log("¡Tiempo terminado!");

        if (mensajeTiempoTerminado != null)
            mensajeTiempoTerminado.SetActive(true); // Se activa el mensaje
    }

    void ActualizarTextoTemporizador() // Convierte el tiempo en segundos y lo muestra en pantalla
    {
        if (textoTemporizador == null) return; // Si no hay objeto asignado en escena, no hace nada.

        int minutos = Mathf.FloorToInt(tiempoRestante / 60f); // Divide el tiempo restante entre 60 para obtener los minutos completos.
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f); // Dividimos los minutos para sacar los segundos

        textoTemporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos); // Actualizamos el formato del texto
    }

    public void DetenerTemporizador() // Detenemos el conteo del tiempo desde Update.
    {
        tiempoAgotado = true;
    }
}