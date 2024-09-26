using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCloud : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.left * (GameManager.Instance.GetScrollSpeed()/8) * Time.deltaTime);
    }
}
