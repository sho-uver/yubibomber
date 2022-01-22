using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {        
        time += Time.deltaTime;
        /*
        switch (Mathf.RoundToInt(time))
        {
            case 0:
                transform.position = new Vector2(1,0);
                break;
            case 1:
                transform.position = new Vector2(-2,0);
                break;
            case 2:
                transform.position = new Vector2(1,0);
                break;
            case 3:
                break;
            case 4:
                time = 0;
                break;
        }
        */
    }
}
