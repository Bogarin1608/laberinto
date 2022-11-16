using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float horizontalMove;
    public float verticalMove;
    public float playerSpeed;
    private Vector3 movePlayer;

    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    
    private Vector3 playerInput;
    
    public CharacterController player;
    
    public Vector2 sensitivity;
    private void Start()
    {
        player = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove= Input.GetAxis("Vertical");
        UpdateMouseLook();

        CamDirection();
        
        player.Move(movePlayer*Time.deltaTime);
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        movePlayer = playerInput.x * camRight+playerInput.z*camForward;
        // player.transform.LookAt(player.transform.position+movePlayer);

        movePlayer = movePlayer * playerSpeed;

        SetGravity();

        PlayerSkills();

    }

    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    
    
    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0,hor*sensitivity.x,0);
        }
    }

    void SetGravity()
    {
        
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }

    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }
}