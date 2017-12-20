using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUtil {

	public static bool CheckIfTouched(Collider2D col) {

        //Revisar si se tocó el collider (Android)
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {

                    //Transformar coordenadas de camara a posición global
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

                    //Revisar si hizo click en el objeto
                    if (Physics2D.OverlapPoint(worldPoint) == col) {
                        return true;
                    }
                }
            }
        }

        //Revisar si se hizo click en el collider (Windows)
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
            if (Input.GetMouseButtonDown(0)) {

                //Transformar coordenadas de camara a posición global
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Revisar si hizo click en el objeto
                if (Physics2D.OverlapPoint(worldPoint) == col) {
                    return true;
                }
            }
        }

        return false;
    }

}
