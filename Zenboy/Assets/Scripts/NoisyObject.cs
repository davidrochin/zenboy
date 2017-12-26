using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NoisyObject : MonoBehaviour {

    public enum ShutdownMode {Touched, SwipeLeft, SwipeRight, SwipeUp, SwipeDown}
    public ShutdownMode shutdownMode;

    public bool turnedOn = false;
    public int availableAtLevel = 1;
    public Vector3 iconLocation;

    [HideInInspector]
    public Vector2 firstPressPos;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Animator animator;

    new Collider2D collider;

	void Awake () {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        CreatePointer();
    }
	
	void Update () {

        if (TouchUtil.CheckSwipe(collider, shutdownMode)) {
            TurnOff();
        }
    }

    public void TurnOff() {
        turnedOn = false;

        //Dejar de reproducir el sonido
        audioSource.Pause();

        //Desactivar el ícono que le dice al jugador como apagar el objeto
        spriteRenderer.color = Color.white;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().enabled = false;

        //Si tiene Animator, desactivar el booleano para encender el objeto
        if (animator != null) { animator.SetBool("turnedOn", false); }
    }

    public void TurnOn() {
        turnedOn = true;

        //Empezar a reproducir el sonido
        audioSource.UnPause();
        audioSource.Play();

        //Dibujar el icono que le dice al jugador como apagar el objeto
        spriteRenderer.color = Color.red;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/UI/" + shutdownMode.ToString());
        transform.Find("Pointer").GetComponent<SpriteRenderer>().enabled = true;

        //Si tiene Animator, activar el booleano para encender el objeto
        if (animator != null) { animator.SetBool("turnedOn", true); }
    }

    private void CreatePointer() {
        GameObject pointer = new GameObject("Pointer");
        pointer.transform.parent = transform;
        pointer.AddComponent<SpriteRenderer>().enabled = false;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/UI/" + shutdownMode.ToString());
        transform.Find("Pointer").GetComponent<SpriteRenderer>().sortingLayerName = "Middle";
        pointer.transform.localPosition = iconLocation;
        pointer.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawIcon(transform.localPosition + iconLocation, "Sprites/UI/" + shutdownMode.ToString() + ".png", true);
    }

    #region Estáticos

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

    #endregion
}
