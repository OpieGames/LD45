using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHop : MonoBehaviour
{
    public float minHopStrength = 0.5f;
    public float maxHopStrength = 3.5f;
    public float timeBetweenHops = 2.0f;

    Rigidbody rb;

    float timeSinceLastHop = 0.0f;
    float randomHopDelay = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastHop > timeBetweenHops)
        {
            timeSinceLastHop = 0.0f;
            randomHopDelay = Random.Range(timeBetweenHops - 0.15f * timeBetweenHops,
                timeBetweenHops + 0.15f * timeBetweenHops);
            Vector3 randomHopDirection = Random.insideUnitSphere;
            if (randomHopDirection.y < 0.0f)
            {
                randomHopDirection.y = -randomHopDirection.y;
            }

            randomHopDirection *= Random.Range(minHopStrength, maxHopStrength);

            rb.AddForce(randomHopDirection, ForceMode.Impulse);
        }

        timeSinceLastHop += Time.deltaTime;
    }
}
