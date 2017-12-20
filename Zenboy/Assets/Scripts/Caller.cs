using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caller : MonoBehaviour {
    public float initialMinTime, initialMaxTime;

    /*[HideInInspector] public float minTime;
    [HideInInspector] public float maxTime;*/

    public float minTime;
    public float maxTime;

    PlayManager playManager;

    void Awake() {
        playManager = FindObjectOfType<PlayManager>();
        StartCoroutine("Call");
    }

    void Start() {
        minTime = initialMinTime;
        maxTime = initialMaxTime;
    }

    IEnumerator Call() {
        while (true) {

            //Revisar si está en pausa el Player
            if (!playManager.paused) {

                //Esperar una cantidad aleatoria de segundos
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));

                //Elegir un objeto aleatorio y encenderlo
                NoisyObject[] noisyObjects = NoisyObject.TurnedOffObjects();
                if(noisyObjects.Length > 0) {
                    int objectToTurnOn = Mathf.RoundToInt(Random.Range(0, noisyObjects.Length));
                    noisyObjects[objectToTurnOn].TurnOn();
                }
            } else {
                yield return new WaitForEndOfFrame();
            } 
        }
    }
}