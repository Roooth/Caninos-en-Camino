using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Controller : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float radius;

    private Rigidbody2D perroRB;

    void Start()
    {
        perroRB = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        bool ifGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, Ground);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ifGrounded)
            {
                perroRB.AddForce(Vector2.up * upForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.ShowGameOverScreen();
            Time.timeScale = 0f;
        }
    }
}
