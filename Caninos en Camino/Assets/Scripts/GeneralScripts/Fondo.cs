using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fondo : MonoBehaviour
{
    [SerializeField] private RawImage fondo;
    [SerializeField] private float eje_x, eje_y;
    
    void Update()
    {
        fondo.uvRect = new Rect(fondo.uvRect.position + new Vector2(eje_x, eje_y) * Time.deltaTime, fondo.uvRect.size);
    }
}
