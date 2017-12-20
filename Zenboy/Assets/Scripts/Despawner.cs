using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour {

    float despawnRadius = 1f;

	void Update () {
        Collider2D detected = Physics2D.OverlapCircle(transform.position, despawnRadius);
        if (detected != null) { Destroy(detected.gameObject); }
	}

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, despawnRadius);
    }
}
