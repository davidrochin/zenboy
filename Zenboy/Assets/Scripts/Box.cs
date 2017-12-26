using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    Collider2D collider;
    float speed = 12f;
    public Vector3 desiredPosition;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void Update () {

        //Mover la caja hasta su posicion deseada
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, speed * Time.deltaTime);

        //Si se toca la caja, subir de nivel y destruirla
        if (TouchUtil.CheckTouched(collider)) {
            FindObjectOfType<PlayManager>().NextLevel();
            Destroy(gameObject);
        }
	}
}
