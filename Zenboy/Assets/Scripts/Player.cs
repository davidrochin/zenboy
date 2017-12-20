using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [Range(0, 1f)]
    [HideInInspector]
    public float concentration = 1f;
    [HideInInspector]
    public float noise = 0f;
    public float recoverConcentration;
    public Sprite[] cheers;

    PlayManager playManager;

    private void Awake() {
        playManager = FindObjectOfType<PlayManager>();
    }

    private void Start() {

        //Empezar la corrutina que le da un punto por segundo al jugador
        StartCoroutine("Score");
    }

    private void Update() {

        if (playManager.paused == false) {
            //Cambiar el sprite del personaje dependiendo de la concentración
            GetComponent<SpriteRenderer>().sprite = GetConcentrationSprite();

            //Sumarle densidad por cada objeto ruidoso encendido
            noise = NoisyObject.TurnedOnObjectsCount() * 0.10f;

            ManageConcentration();
            UpdateBar();
            UpdateScore();
        }

    }

    public void RestartGame() {
        concentration = 1;
        playManager.paused = false;
        noise = 0f;
        playManager.score = 0;
        NoisyObject.TurnOffAll();
    }

    private void ManageConcentration() {

        //Restar concentración si hay ruido
        if (noise > 0) {
           concentration = Mathf.Clamp01(concentration - noise * Time.deltaTime);
        } 
        
        //Sumar concentración si NO hay ruido
        else {
            concentration = Mathf.Clamp01(concentration + recoverConcentration * Time.deltaTime);
        }

        //Si la concentracion es 0 o menor, se acaba el juego
        if (concentration <= 0) {
            GameObject.Find("GameOver").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("GameOver").GetComponent<CanvasGroup>().blocksRaycasts = true;
            playManager.paused = true;
            concentration = 0f;
            NoisyObject.TurnOffAll();
        }
    }

    private void UpdateBar() {
        Image bar = GameObject.Find("Bar").GetComponent<Image>();
        bar.fillAmount = concentration;
    }

    private void UpdateScore() {
        Text text = GameObject.Find("Score").GetComponent<Text>();
        text.text = playManager.score.ToString();
    }

    private Sprite GetConcentrationSprite() {
        for (int i = 0; i < cheers.Length; i++) {
            float portion = 1f / cheers.Length;
            float fraction = portion * i;
            if (concentration <= (fraction + portion) && concentration >= fraction) {
                return cheers[i];
            }
        }
        return GetComponent<SpriteRenderer>().sprite;
    }

    IEnumerator Score() {
        while (true) {
            yield return new WaitForSeconds(1f);
            if (!playManager.paused) {
                playManager.score += 1;
            }
        }
    }
}