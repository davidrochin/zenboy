using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NoisyObject : MonoBehaviour {

    public enum ShutdownMode {Touched, SwipeLeft, SwipeRight, SwipeUp, SwipeDown}
    public ShutdownMode shutdownMode;
    public bool turnedOn = false;
    public Vector3 pointerLocation;
    [HideInInspector]
    public Vector2 firstPressPos;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    new Collider2D collider;

	void Awake () {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        CreatePointer();
    }

    private void CreatePointer()
    {
        GameObject pointer = new GameObject("Pointer");
        pointer.transform.parent = transform;
        pointer.AddComponent<SpriteRenderer>().enabled = false;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/UI/" + shutdownMode.ToString());
        pointer.transform.localPosition = pointerLocation;
        pointer.transform.localScale = new Vector3(1, 1, 1);
    }
	
	void Update () {

        //Apagar el objeto si se tocó (Android)
        if (TouchUtil.CheckIfTouched(collider, shutdownMode)) {
            TurnOff();
        }
    }

    public void TurnOff() {
        turnedOn = false;
        audioSource.Pause();
        spriteRenderer.color = Color.white;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TurnOn() {
        turnedOn = true;
        audioSource.UnPause();
        audioSource.Play();
        spriteRenderer.color = Color.red;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/UI/" + shutdownMode.ToString());
        transform.Find("Pointer").GetComponent<SpriteRenderer>().enabled = true;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.localPosition + pointerLocation, "Sprites/UI/" + shutdownMode.ToString() + ".png", true);
    }
}
