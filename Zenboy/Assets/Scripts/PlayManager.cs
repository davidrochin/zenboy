using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    public bool paused;
    public int collectedCoins;
    public int score;

    Caller caller;

	void Awake () {
        caller = FindObjectOfType<Caller>();
	}
	
	
	void Update () {

        //Alterar los tiempos minimos y maximos del Caller deacuerdo al puntaje
        caller.minTime = Mathf.Lerp(caller.initialMinTime, 0f, Mathf.InverseLerp(0f, 100f, score));
        caller.maxTime = Mathf.Lerp(caller.initialMaxTime, 0f, Mathf.InverseLerp(0f, 100f, score));
    }

    public void RestartPlay() {

    }
}
