using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // CharacterController cc;
    Rigidbody rb;
    Collider playerCollider;
    Animator animator;

    public GameObject model;

    // Translational move speed
    public float MoveSpeed;
    // public float Gravity;
    public float acceleration = 1.0f;
    public float jumpStrength = 10.0f;
    public float fallMultiplier = 2.5f;
    public float dashStrength = 10.0f;
    public float stoppingDrag = 10.0f;

    public float dashCooldown = 2.5f;

    private Vector3 moveVector = Vector3.zero;
    private bool isGrounded;
    private float originalDrag;
    private float currentDashCooldown = 0.0f;

    //SOUNDS
    [Header("Sounds")]
    public AudioClip[] FootstepSounds;
    public AudioSource AudioSrc;
    public float AudioStepTime = 1.0f;
    private float stepTimer = 0.0f;

    void Start()
    {
        // cc  = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        AudioSrc = GetComponent<AudioSource>();

        // Don't let physics apply any weird torques that mess with aiming our character
        rb.freezeRotation = true;

        originalDrag = rb.drag;

    }

    void FixedUpdate()
    {

        // Check against all layers except Player
        int layerMask = ~LayerMask.GetMask("Player");
        // check a short capsule below the player to see if we're grounded
        isGrounded = Physics.CheckCapsule(playerCollider.bounds.center,
            new Vector3(playerCollider.bounds.center.x,
                playerCollider.bounds.min.y - 0.01f,
                playerCollider.bounds.center.z),
            0.18f,
            layerMask);
        animator.SetBool("isGrounded", isGrounded);
        // Debug.Log("isGrounded: " + isGrounded.ToString());

        moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        moveVector = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * moveVector;

        if (isGrounded)
        {
            rb.velocity += moveVector * acceleration * Time.deltaTime;
        }
        else
        {
            // Don't accelerate at full speed in the air
            rb.velocity += moveVector * acceleration * Time.deltaTime / 4.0f;
        }

        Vector3 translationalVelocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        if (translationalVelocity.sqrMagnitude > MoveSpeed * MoveSpeed)
        {
            // evil hardcoded drag constant
            translationalVelocity *= 0.75f;
        }
        // Debug.Log(translationalVelocity.magnitude);
        rb.velocity = new Vector3(translationalVelocity.x, rb.velocity.y, translationalVelocity.z);

        if (
            moveVector.sqrMagnitude < 0.5f && isGrounded
            || translationalVelocity.sqrMagnitude > MoveSpeed * MoveSpeed
        )
        {
            rb.drag = stoppingDrag;
        }
        else
        {
            rb.drag = originalDrag;
        }





        // If we're falling, apply stronger gravity to mitigate the floaty feel
        if (rb.velocity.y < 0.0f)
        {
            // * Note: Physics.gravity is taken to be negative, so we add it
            rb.velocity += Physics.gravity * fallMultiplier * Time.fixedDeltaTime;
        }

        if (isGrounded && rb.velocity.magnitude > 2f)
        {
            StepSounds();
        }
        else
        {
            StartCoroutine(AudioFader.FadeOut(AudioSrc, 2.0f));
        }

        stepTimer += Time.deltaTime;
    }

    void Update()
    {
        if (currentDashCooldown > 0.0f) {
            currentDashCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump!");
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && (currentDashCooldown <= 0.0f))
        {
            Debug.Log("Dash!");
            rb.AddForce(model.transform.forward * dashStrength, ForceMode.Impulse);
            currentDashCooldown = dashCooldown;
        }

        Turning();

        // // If we're moving, set our model's rotation
        // if (cc.velocity.magnitude > 0.1f)
        // {
        //     var rotation = Quaternion.LookRotation(moveVector);
        //     rotation.x = 0.0f;
        //     rotation.z = 0.0f;
        //     model.transform.rotation = rotation;
        // }
    }

    void Turning()
    {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, 99999.9f, LayerMask.GetMask("Floor")))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;

                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                model.transform.rotation = newRotation;
            }
    }


    private void StepSounds()
    {
        if (stepTimer >= AudioStepTime)
        {
            int index = UnityEngine.Random.Range(0, FootstepSounds.Length);
            AudioSrc.volume = UnityEngine.Random.Range(0.7f, 0.85f);
            AudioSrc.pitch = UnityEngine.Random.Range(0.88f, 1.12f);
            AudioSrc.PlayOneShot(FootstepSounds[index]);

            stepTimer = 0.0f;
        }
    }
}
