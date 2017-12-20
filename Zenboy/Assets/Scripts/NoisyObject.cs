using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NoisyObject : MonoBehaviour {

    public bool turnedOn = false;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

	void Awake () {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update () {

        //Apagar el objeto si se tocó (Android)
        if(Application.platform == RuntimePlatform.Android) {
            if(Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began) {

                    //Transformar coordenadas de camara a posición global
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

                    //Revisar si hizo click en el objeto
                    if (Physics2D.OverlapPoint(worldPoint) == GetComponent<Collider2D>()) {
                        TurnOff();
                    }
                }
            }
        }

        //Apagar el objeto si se tocó (Windows)
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
            if (Input.GetMouseButtonDown(0)) {

                //Transformar coordenadas de camara a posición global
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Revisar si hizo click en el objeto
                if (Physics2D.OverlapPoint(worldPoint) == GetComponent<Collider2D>()) {
                    TurnOff();
                }
            }
        }
    }

    public void TurnOff() {
        turnedOn = false;
        audioSource.Pause();
        spriteRenderer.color = Color.white;
    }

    public void TurnOn() {
        turnedOn = true;
        audioSource.UnPause();
        audioSource.Play();
        spriteRenderer.color = Color.red;
    }

    public static void TurnOffAll() {
        foreach (NoisyObject no in FindObjectsOfType<NoisyObject>()) {
            no.TurnOff();
        }
    }

    public static int TurnedOnObjectsCount() {
        int counter = 0;
        foreach (NoisyObject no in FindObjectsOfType<NoisyObject>()) {
            if (no.turnedOn) { counter++; }
        }
        return counter;
    }

    public static NoisyObject[] TurnedOffObjects() {
        List<NoisyObject> list = new List<NoisyObject>();
        foreach (NoisyObject no in FindObjectsOfType<NoisyObject>()) {
            if (!no.turnedOn) { list.Add(no); }
        }
        return list.ToArray();
    }
}
