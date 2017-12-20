using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour {

    public Vector3 rollDirection = Vector3.left;
    public float rollSpeed = 1f;

    Collider2D collider;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void Update () {

        //Revisar si la moneda fue tocada
        if (TouchUtil.CheckIfTouched(collider)) {
            FindObjectOfType<PlayManager>().collectedCoins++;
            Destroy(gameObject);
        }

        rollDirection = rollDirection.normalized;
        transform.position = transform.position + rollDirection * rollSpeed * Time.deltaTime;
	}
}
