using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Avoider : MonoBehaviour
{
    public float avoidDistance = 3.0f;

    Transform playerTransform;
    NavMeshAgent agent;

    float squaredAvoidDistance;
    IEnumerator activeIdleCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (!playerTransform)
        {
            Debug.LogError("Couldn't find the player Transform, dummy");
        }
        agent = GetComponent<NavMeshAgent>();

        squaredAvoidDistance = avoidDistance * avoidDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     Ray mousePosRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit floorHit;

        //     if (Physics.Raycast(mousePosRay, out floorHit, 9999.0f, LayerMask.GetMask("Floor")))
        //     {
        //         Debug.Log("Retargeting NavMeshAgent");
        //         agent.SetDestination(floorHit.point);
        //     }
        // }
        float squaredPlayerDistance = (playerTransform.position - transform.position).sqrMagnitude;
        if (squaredPlayerDistance < squaredAvoidDistance)
        {
            // If we were idling, stop idling so we don't randomly retarget the NavMeshAgent
            if (activeIdleCoroutine != null)
            {
                StopCoroutine(activeIdleCoroutine);
                activeIdleCoroutine = null;
            }
            AvoidPlayer();
        }
        else
        {
            if (activeIdleCoroutine == null)
            {
                activeIdleCoroutine = IdleMovement();
                StartCoroutine(activeIdleCoroutine);
            }
        }
    }

    void AvoidPlayer()
    {
        Debug.Log("Run away!!!");
        Vector3 awayFromPlayer = transform.position - playerTransform.position;
        agent.SetDestination(transform.position + awayFromPlayer * agent.speed);
    }

    IEnumerator IdleMovement()
    {
        while (true)
        {
            // Target a random point up to half our speed away --
            // that is, half a second's travel away
            float distance = agent.speed / 2.0f;
            float wanderDelaySeconds = 1.5f + Random.Range(-0.5f, 0.5f);
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += transform.position;
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas);
            agent.SetDestination(navMeshHit.position);
            Debug.Log("Idling. New destination: " + agent.destination.ToString());
            yield return new WaitForSeconds(wanderDelaySeconds);
        }
    }
}
