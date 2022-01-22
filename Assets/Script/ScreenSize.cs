using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    Vector2 baseSize;
    Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        baseSize = new Vector2(750, 1334);
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect safeArea = Screen.safeArea;
        float num = Screen.height / safeArea.size.y;
        camera.orthographicSize =  5 * num;
    }
}
