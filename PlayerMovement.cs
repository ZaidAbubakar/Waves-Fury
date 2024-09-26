using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundedDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    bool isMoving;

    public bool isSprinting;
    public Slider sprintSlider;
    public float Stamina;
    public float staminaDecay;
    public float staminaHeal;
    public float maxStamina;
    public bool emptyStamina;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);


    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    void Update()
    {

        //Sprinting
        sprintSlider.value = Stamina;
        sprintSlider.maxValue = maxStamina;

        if(Stamina > 10)
        {
            emptyStamina = false;
        }
        if(Stamina <= 0)
        {
            emptyStamina = true;
        }
        if (emptyStamina) {
            isSprinting = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && !emptyStamina)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting && Stamina > 0)
        {
            Stamina -= staminaDecay * Time.deltaTime;
        }
        else
        {
            if (Stamina < maxStamina)
            {
                Stamina += staminaHeal * Time.deltaTime;
            }
        }


        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundedDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        // Movements

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (!isSprinting)
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Movement detection
       /* if (lastPosition != gameObject.transform.position && isGrounded == true)
        { isMoving = true; } 
        else { isMoving = false; }*/

        lastPosition = gameObject.transform.position;
    }
}
