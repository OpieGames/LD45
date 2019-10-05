using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    public GameObject model;

    public float MoveSpeed;
    public float Gravity;
    
    private Vector3 moveVector = Vector3.zero;
    
    void Start()
    {
        cc  = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        if (cc.isGrounded)
        {
            moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        }

        moveVector.y -= Gravity * Time.deltaTime;

        moveVector = Quaternion.Euler(0, 45, 0) * moveVector;

        cc.Move(moveVector.normalized * MoveSpeed * Time.deltaTime);

        // If we're moving, set our model's rotation
        if (cc.velocity.magnitude > 0.1f)
        {
            var rotation = Quaternion.LookRotation(moveVector);
            rotation.x = 0.0f;
            rotation.z = 0.0f;
            model.transform.rotation = rotation;
        }
    }
}
