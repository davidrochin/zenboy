using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caller : MonoBehaviour {
    public float minTime, maxTime, delayInSeconds;
    public float density;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.Pause();
        StartCoroutine("Call");
    }

    IEnumerator Call()
    {
        while (true)
        {
            if (!FindObjectOfType<Player>().pause)
            {
                yield return new WaitForSeconds(Random.Range(minTime, maxTime));
                List<GameObject> objects = new List<GameObject>();
                foreach (PolygonCollider2D polygonCol in FindObjectsOfType<PolygonCollider2D>())
                {
                    if (polygonCol.tag == "Object")
                    {
                        objects.Add(polygonCol.gameObject);
                    }
                }
                int randomObject = Mathf.RoundToInt(Random.Range(0, objects.Count));
                objects[randomObject].GetComponent<SpriteRenderer>().color = Color.red;
                audioSource.transform.position = objects[randomObject].transform.position;
                audioSource.Play();
            }
            yield return new WaitForSeconds(delayInSeconds);
            if (!ObjectsAreOff())
            {
                FindObjectOfType<Player>().density = this.density;
            }
            yield return new WaitUntil(() => ObjectsAreOff());
        }
    }

    public bool ObjectsAreOff()
    {
        foreach (PolygonCollider2D polygonCol in FindObjectsOfType<PolygonCollider2D>())
        {
            if (polygonCol.tag == "Object" && polygonCol.GetComponent<SpriteRenderer>().color == Color.red)
            {
                return false;
            }
        }
        return true;
    }

    public void DisableObjects()
    {
        foreach (PolygonCollider2D polygonCol in FindObjectsOfType<PolygonCollider2D>())
        {
            if (polygonCol.tag == "Object" && polygonCol.GetComponent<SpriteRenderer>().color == Color.red)
            {
                polygonCol.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (FindObjectOfType<AudioSource>().isPlaying)
        {
            FindObjectOfType<AudioSource>().Pause();
        }
    }
}