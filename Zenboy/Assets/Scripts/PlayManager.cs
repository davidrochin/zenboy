using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    public bool paused;
    public int collectedCoins;
    public int score;

    Caller caller;
    Player player;

    SpriteRenderer backgroundSprite;

	void Awake () {
        caller = FindObjectOfType<Caller>();
        player = FindObjectOfType<Player>();
        backgroundSprite = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
	}
	
	
	void Update () {

        //Alterar los tiempos minimos y maximos del Caller deacuerdo al puntaje
        caller.minTime = Mathf.Lerp(caller.initialMinTime, 0f, Mathf.InverseLerp(0f, 100f, score));
        caller.maxTime = Mathf.Lerp(caller.initialMaxTime, 0f, Mathf.InverseLerp(0f, 100f, score));

        //Pintar el fondo de color conforme a la concentración
        backgroundSprite.color = Color.Lerp(Color.red, Color.white, player.concentration);
    }

    public void RestartPlay() {

    }
}
