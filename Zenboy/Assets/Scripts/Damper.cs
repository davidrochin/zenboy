using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damper : MonoBehaviour {

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Camera.main.transform.forward, Mathf.Abs(Camera.main.transform.position.z) + 1f);
            if(raycast.transform.tag == "Object" && raycast.transform.GetComponent<SpriteRenderer>().color == Color.red)
            {
                raycast.transform.GetComponent<SpriteRenderer>().color = Color.white;
                if (FindObjectOfType<Caller>().ObjectsAreOff())
                {
                    FindObjectOfType<Player>().density = 0;
                    GameObject.Find("AudioSource").GetComponent<AudioSource>().Pause();
                    if (FindObjectOfType<Player>().concentration >= 1)
                    {
                        FindObjectOfType<Player>().score += 5;
                    }
                    FindObjectOfType<Player>().score += 1;
                }
            }
        } else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, Mathf.Abs(Camera.main.transform.position.z) + 1f);
            if (raycast.collider != null && raycast.transform.tag == "Object" && raycast.transform.GetComponent<SpriteRenderer>().color == Color.red)
            {
                raycast.transform.GetComponent<SpriteRenderer>().color = Color.white;
                if (FindObjectOfType<Caller>().ObjectsAreOff())
                {
                    FindObjectOfType<Player>().density = 0;
                    GameObject.Find("AudioSource").GetComponent<AudioSource>().Pause();
                    if(FindObjectOfType<Player>().concentration >= 1)
                    {
                        FindObjectOfType<Player>().score += 5;
                    }
                    FindObjectOfType<Player>().score += 1;
                }
            }
        }
    }
}