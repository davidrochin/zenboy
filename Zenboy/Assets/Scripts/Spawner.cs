using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public float minSpawnTime, maxSpawnTime;

    PlayManager playManager;

    void Awake() {
        playManager = GetComponent<PlayManager>();
    }

    void Start () {
        StartCoroutine("SpawnRoutine");
	}

    IEnumerator SpawnRoutine() {
        while (true) {
            //Revisar si está en pausa el Player
            if (!FindObjectOfType<PlayManager>().paused) {

                //Esperar una cantidad aleatoria de segundos
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

                //Spawnear el objeto
                Instantiate(objectToSpawn, transform.position, transform.rotation);

            } else {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.position, "Sprites/UI/Circle.png", true);
    }
}
