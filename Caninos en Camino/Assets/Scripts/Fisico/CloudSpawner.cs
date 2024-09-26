using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;

    void Start()
    {
        StartCoroutine(SpawnCloud());
    }

    private IEnumerator SpawnCloud()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, clouds.Length);
            float minTime = 4f;
            float maxTime = 6.6f;
            float randomTime = Random.Range(minTime, maxTime);

            Instantiate(clouds[randomIndex], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
