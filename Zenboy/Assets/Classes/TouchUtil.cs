using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUtil {

	public static bool CheckSwipe(Collider2D col, NoisyObject.ShutdownMode shutdownMode) {

        NoisyObject noisyObject = null;
        if (col.GetComponent<NoisyObject>() != null)
        {
            noisyObject = col.GetComponent<NoisyObject>();
        }
        //Revisar si se tocó el collider (Android)
        if (Application.platform == RuntimePlatform.Android) {
            /*
            if (shutdownMode == NoisyObject.ShutdownMode.Touched)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {

                        //Transformar coordenadas de camara a posición global
                        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);

                        //Revisar si hizo click en el objeto
                        if (Physics2D.OverlapPoint(worldPoint) == col)
                        {
                            return true;
                        }
                    }
                }
            } else
            {
            */
                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);
                    NoisyObject.ShutdownMode direction = NoisyObject.ShutdownMode.Touched;

                    if (t.phase == TouchPhase.Began)
                    {
                        // Si hizo click en el objeto guardar en un Vector2 la posicion del dedo al tocar la pantalla
                        noisyObject.firstPressPos = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(t.position)) == col ? t.position : Vector2.zero;
                    }

                    if (t.phase == TouchPhase.Ended && noisyObject.firstPressPos != Vector2.zero)
                    {
                        Vector2 secondPressPos = new Vector2(t.position.x, t.position.y);
                        Vector3 currentSwipe = new Vector3(secondPressPos.x - noisyObject.firstPressPos.x, secondPressPos.y - noisyObject.firstPressPos.y);

                        if (currentSwipe.magnitude < 200f)
                        {
                            return NoisyObject.ShutdownMode.Touched == shutdownMode ? true : false;
                        }

                        currentSwipe.Normalize();
                        if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                            direction = NoisyObject.ShutdownMode.SwipeUp;
                        } else if (currentSwipe.y < 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                            direction = NoisyObject.ShutdownMode.SwipeDown;
                        } else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                            direction = NoisyObject.ShutdownMode.SwipeLeft;
                        } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                            direction = NoisyObject.ShutdownMode.SwipeRight;
                        }

                        return direction == shutdownMode ? true : false;
                    }
                }
            //}
        }

        //Revisar si se hizo click en el collider (Windows)
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
            /*
            if (shutdownMode == NoisyObject.ShutdownMode.Touched)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    //Transformar coordenadas de camara a posición global
                    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    //Revisar si hizo click en el objeto
                    if (Physics2D.OverlapPoint(worldPoint) == col)
                    {
                        return true;
                    }
                }
            }
            else
            {
            */
                NoisyObject.ShutdownMode direction = NoisyObject.ShutdownMode.Touched;

                if (Input.GetMouseButtonDown(0))
                {
                    noisyObject.firstPressPos = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) == col ? (Vector2)Input.mousePosition : Vector2.zero;
                }

                if (Input.GetMouseButtonUp(0) && noisyObject.firstPressPos != Vector2.zero)
                {
                    Vector2 secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 currentSwipe = new Vector3(secondPressPos.x - noisyObject.firstPressPos.x, secondPressPos.y - noisyObject.firstPressPos.y);

                    if (currentSwipe.magnitude < 200f)
                    {
                        return NoisyObject.ShutdownMode.Touched == shutdownMode ? true : false;
                    }

                    currentSwipe.Normalize();
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        direction = NoisyObject.ShutdownMode.SwipeUp;
                    }
                    else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        direction = NoisyObject.ShutdownMode.SwipeDown;
                    }
                    else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        direction = NoisyObject.ShutdownMode.SwipeLeft;
                    }
                    else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        direction = NoisyObject.ShutdownMode.SwipeRight;
                    }

                    return direction == shutdownMode ? true : false;
                }
            //}
        }

        return false;
    }

    public static bool CheckTouched(Collider2D col) {

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

    public static bool CheckTouched(GameObject go) {
        return CheckTouched(go.GetComponent<Collider2D>());
    }

}
