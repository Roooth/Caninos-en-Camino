using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        //LOS OBSTACULOS SALEN EN RELACION CON LA VELOCIDAD DEL JUGADOR Y UNA VEZ SALEN DE PANTALLA SE DESTRUYEN
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
