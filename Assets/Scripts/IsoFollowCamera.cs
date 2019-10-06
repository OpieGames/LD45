using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoFollowCamera : MonoBehaviour
{
    public float smoothDurationSeconds = 0.5f;

    Transform playerTransform;
    // Vector3 displacementToPlayer;
    Vector3 targetPosition;
    Vector3 startPosition;
    float timeSinceMoveStart;
    bool currentlyMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // displacementToPlayer = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);

        if (!currentlyMoving)
        {
            if (Input.GetAxisRaw("Camera") > 0.0f)
            {
                Debug.Log("Camera +!");
                currentlyMoving = true;
                startPosition = transform.localPosition;
                targetPosition = Quaternion.Euler(0.0f, -90.0f, 0.0f) * startPosition;
                timeSinceMoveStart = 0.0f;

            }
            else if (Input.GetAxisRaw("Camera") < 0.0f)
            {
                Debug.Log("Camera -!");
                currentlyMoving = true;
                startPosition = transform.localPosition;
                targetPosition = Quaternion.Euler(0.0f, 90.0f, 0.0f) * startPosition;
                timeSinceMoveStart = 0.0f;
            }
        }
        else
        {
            timeSinceMoveStart += Time.deltaTime;
            float current_t = Mathf.SmoothStep(0.0f, 1.0f, timeSinceMoveStart / smoothDurationSeconds);
            transform.localPosition = Vector3.Slerp(startPosition, targetPosition, current_t);
            if (timeSinceMoveStart > smoothDurationSeconds)
            {
                currentlyMoving = false;
            }
        }
    }
}
