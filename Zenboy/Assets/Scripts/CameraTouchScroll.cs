using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchScroll : MonoBehaviour {

    [Range(0f, 1f)]
    public float t = 0.5f;
    public Vector3 a;
    public Vector3 b;
    public Vector3 offset = new Vector3(0f, 0f, 1f);

    [Header("Other")]
    public float touchPanSpeed = 0.05f;
    public float clickPanSpeed = 1f;

	void Awake () {
		
	}


	void Update () {

        //Si la plataforma es Android
        if(Application.platform == RuntimePlatform.Android) {

            //Aumentar o disminuir la posicion "t" dependiendo del touch
            if (Input.touchCount > 0f) {

                Touch touch = Input.GetTouch(0);

                //Si el touch está en progreso
                if (touch.phase == TouchPhase.Moved) {
                    t = Mathf.Clamp01(t - touch.deltaPosition.x * Time.deltaTime * touchPanSpeed);
                }

            }
        } 
        
        //Si la plataforma es Windows
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {

            //Aumentar o disminuir la posicion "t" dependiendo de el click
            if (Input.GetMouseButton(0) && Input.GetAxis("Mouse X") != 0f) {
                t = Mathf.Clamp01(t - Input.GetAxis("Mouse X") * Time.deltaTime * clickPanSpeed);
            }
        }

        //Acomodar la camara en la posicion "t"
        transform.position = Vector3.Lerp(a, b, t) + offset;

    }

    private void OnDrawGizmosSelected() {
        Camera camera = GetComponent<Camera>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(a, new Vector2(camera.aspect * camera.orthographicSize * 2f, camera.orthographicSize * 2f));
        Gizmos.DrawWireCube(b, new Vector2(camera.aspect * camera.orthographicSize * 2f, camera.orthographicSize * 2f));
        Gizmos.DrawLine(a,b);
    }
}
