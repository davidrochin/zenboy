using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

    [Header("Setup")]
    public GameObject box;

    [Header("Debug")]
    public bool paused;
    public int collectedCoins;
    public int score;
    public int level = 1;
    public float maxScore = 100f;
    public float boxOnScore = 0f;
    public bool boxSpawned = false;

    Vector3 boxSpawnPosition;

    Caller caller;
    Player player;

    SpriteRenderer backgroundSprite;
    Image bar;

    NoisyObject[] allNoisyObjects;

	void Awake () {
        caller = FindObjectOfType<Caller>();
        player = FindObjectOfType<Player>();
        backgroundSprite = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        bar = GameObject.FindGameObjectWithTag("Concentration Bar").GetComponent<Image>();

        //Guardar la posicion del Box Spawn y borrarlo para que no estorbe
        boxSpawnPosition = GameObject.FindGameObjectWithTag("Box Spawn").transform.position;
        Destroy(GameObject.FindGameObjectWithTag("Box Spawn"));
    }

    void Start() {
        //Guardar todos los objetos y esconder los que no son del Nivel 1
        allNoisyObjects = FindObjectsOfType<NoisyObject>();
        foreach (NoisyObject no in allNoisyObjects) {
            if (no.availableAtLevel > 1) {
                no.gameObject.SetActive(false);
            }
        }

        //Determinar cuando se va a aparecer la caja para subir de nivel
        boxOnScore = Random.Range(maxScore / 3f, maxScore);
    }

    

    void Update () {

        //Alterar los tiempos minimos y maximos del Caller deacuerdo al puntaje
        caller.minTime = Mathf.Lerp(caller.initialMinTime, 0f, Mathf.InverseLerp(0f, maxScore, score));
        caller.maxTime = Mathf.Lerp(caller.initialMaxTime, 0f, Mathf.InverseLerp(0f, maxScore, score));

        //Pintar la barra de concentración conforme a su valor
        bar.color = Color.Lerp(Color.red, new Color(0.25f, 0.5f, 0.25f), player.concentration);

        //Si es hora de aparecer la caja, hacerlo
        if(score >= boxOnScore && !boxSpawned) {
            SpawnBox();
        }
    }

    public void SpawnBox() {
        GameObject b = Instantiate(box, boxSpawnPosition + Vector3.up * 12f, Quaternion.identity);
        b.GetComponent<Box>().desiredPosition = boxSpawnPosition;
        boxSpawned = true;
    }

    public void NextLevel() {

        level++;
        maxScore += 100f;
        boxSpawned = false;

        //Resetear la concentración
        FindObjectOfType<Player>().concentration = 1f;

        //Determinar cuando se va a aparecer la caja para subir de nivel
        boxOnScore = Random.Range(maxScore / 3f, maxScore);

        //Aparecer los objetos de ese nivel
        foreach (NoisyObject no in allNoisyObjects) {
            if(no.availableAtLevel <= level) {
                no.gameObject.SetActive(true);
            }
        }

        //Apagar todos los objetos
        NoisyObject.TurnOffAll();
    }

    public void RestartPlay() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
