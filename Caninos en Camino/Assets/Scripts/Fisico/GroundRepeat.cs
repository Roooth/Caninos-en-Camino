using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRepeat : MonoBehaviour
{
    private float spriteWidth = 20f;

    void Start()
    {
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        //spriteWidth = groundCollider.size.x;
    }

    void Update()
    {
        if (transform.position.x < -spriteWidth)
        {
            transform.Translate(Vector2.right * 2f * spriteWidth);
        }
    }
}
