using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistance
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isOnGround;
    [SerializeField] private float speed;
    private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    //Crouch and Sprint
    private float crouchTimer = 0f;
    private bool lerpCrouch;
    private bool crouching;
    private bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = controller.isGrounded;
        if(lerpCrouch)
            {
                crouchTimer += Time.deltaTime;
                float p = crouchTimer / 1;
                p *= p;
                if (crouching)
                    controller.height = Mathf.Lerp(controller.height, 1, p);
                else
                    controller.height = Mathf.Lerp(controller.height, 2, p);

                if (p > 1)
                {
                    lerpCrouch = false;
                    crouchTimer = 0f;
                }
            }
    }
    //Gets the inputs for the InputManager and applys them to the player character controller
    public void ManageMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isOnGround && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isOnGround)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }
    //Save and Load
    public void SaveData(ref SaveData data)
    {
        data.playerPosition = this.transform.position;
    }
    public void LoadData(SaveData data)
    {
        this.transform.position = data.playerPosition;
    }
}

