using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NoisyObject : MonoBehaviour {

    public bool turnedOn = false;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    Collider2D collider;

	void Awake () {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
	
	void Update () {

        //Apagar el objeto si se tocó (Android)
        if (TouchUtil.CheckIfTouched(collider)) {
            TurnOff();
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
