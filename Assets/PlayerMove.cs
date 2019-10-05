using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

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
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        }

        moveVector.y -= Gravity * Time.deltaTime;

        cc.Move(moveVector.normalized * MoveSpeed * Time.deltaTime);
    }
}
