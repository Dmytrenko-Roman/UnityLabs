using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private Camera followCamera;
    [SerializeField]
    private float jumpForce = 1.0f;
    [SerializeField]
    private float gravity = -9.81f;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();    
    }

    private void Update() 
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) 
        {
            playerVelocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        controller.Move(movementDirection * speed * Time.deltaTime);

        if (movementDirection != Vector3.zero) 
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
            TextManager.scoreValue += 1;
        }

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SampleScene" && TextManager.scoreValue == 10) SceneManager.LoadScene(1);

        playerVelocity.y += gravity* Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);  
    }
}