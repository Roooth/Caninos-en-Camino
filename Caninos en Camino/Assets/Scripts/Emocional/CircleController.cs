using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public float maxSize = 5f;
    public float minSize = 1f;
    public float speed = 2f;
    private bool expanding = true;


    void Update()
    {
        if (expanding)
        {
            transform.localScale += Vector3.one * speed * Time.deltaTime;
            if (transform.localScale.x >= maxSize)
                expanding = false;
        }
        else
        {
            transform.localScale -= Vector3.one * speed * Time.deltaTime;
            if (transform.localScale.x <= minSize)
                expanding = true;
        }
    }
}

