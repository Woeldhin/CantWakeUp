using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    // Various variables and gameobjects //
    // Camera, duh
    public Camera head;
    // Player reticule
    public Image reticule;
    // Player collider
    public CapsuleCollider playerCollider;
    // Variable for interactable object
    public GameObject interactable;
    // GabPoint object
    public Transform grabPoint;
    // Pause boolean
    public bool paused;
    // Keybinds
    public Keybindings keybindings;
    // Rigidbody
    public Rigidbody playerRB;

    // Movement variables //
    // Interact distance
    public float maxDistance;
    // Standing movement speed
    public float standSpeed;
    // Crouching movement speed
    public float crouchSpeed;
    // Standing height
    public float standHeight;
    // Crouching height
    public float crouchHeight;
    // Standing up speed (0 - 1)
    public float standUpSpeed;
    // Crouching down speed (0 - 1)
    public float crouchDownSpeed;
    // Jumpforce
    public float jumpForce;
    // Grounded boolean
    private bool isGrounded;
    // Current movement speed
    private float speed;
    // Current height
    private float height;
    // Target height
    private float targetHeight;
    // Stand/Crouch transition speed
    private float standCrouchSpeed;

    // Camera stuff //
    // Mouse sensitivity
    public float sensitivity;
    // Lowest angle the camera will rotate to in vertical axis
    public float minY;
    // Highest angle the camera will rotate to in vertical axis
    public float maxY;
    // Variables to house rotation calculations
    private float rotationX;
    private float rotationY;

    //Audio stuff//
    //Clips
    public AudioClip walkingSound;
    public AudioClip jumpingSound;
    //Sources
    public AudioSource playersFeet1;
    public AudioSource playersFeet2;

    private void Start()
    {
        // Set the starting speed as standing speed just in case.
        speed = standSpeed;
        // Set paused to false
        paused = false;
        // Set grounded to true
        isGrounded = true;
        //Set up the clips to source (Audio)
        playersFeet1.clip = walkingSound;
        playersFeet2.clip = jumpingSound;
    }

    void Update()
    {
        // Character movement
        if (!paused) {
            if (Input.GetKeyDown(keybindings.jump))
            {
                if (isGrounded)
                {
                    // Jump sound
                    playersFeet2.Play();
                    // Adds upward force to player rigidbody
                    gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    // Set isGrounded to false
                    isGrounded = false;
                }
            }

            // Crouch down
            if (Input.GetKeyDown(keybindings.crouch))
            {
                // Change movement speed to crouch speed
                speed = crouchSpeed;
                // Set the target camera height to crouch height
                targetHeight = crouchHeight;
                // Make the collider smaller
                playerCollider.height = crouchHeight + 0.05f;
                // Move the collider to floor level
                playerCollider.center = new Vector3(0, (crouchHeight + 0.05f) / 2, 0);
                // Set stand/crouch transition speed to crouchDown
                standCrouchSpeed = crouchDownSpeed;
            }
            // Stand up
            if (Input.GetKeyUp(keybindings.crouch))
            {
                // Set movement speed to stand speed
                speed = standSpeed;
                // Set the target camera height to stand height
                targetHeight = standHeight;
                // Make the collider larger
                playerCollider.height = standHeight + 0.05f;
                // Move the collider to floor level
                playerCollider.center = new Vector3(0, (standHeight + 0.05f) / 2, 0);
                // Set stand/crouch transition speed to standUp
                standCrouchSpeed = standUpSpeed;
            }
            // Stand/crouch camera height transition
            // Check if the current camera height is different from the target height
            if (height != targetHeight)
            {
                // Calculate new height for each frame
                height = Mathf.Lerp(head.transform.localPosition.y, targetHeight, standCrouchSpeed);
                // Set the camera height each frame
                head.transform.localPosition = new Vector3(0, height, 0);
            }

            // Viewpoint rotation

            // Calculate horizontal rotation
            rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

            // Calculate vertical rotation
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            // Rotate character for the horizontal rotation (camera is following the character)
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            // Rotate only the camera in vertical rotation (so that the character model doesn't tilt)
            head.transform.localEulerAngles = (new Vector3(-rotationY, head.transform.localEulerAngles.y, 0));

            // Camera raycast


            // Gives objects a chance to reset stuff if needed
            // Chacks if left mouse button is released
            if (interactable && Input.GetKeyUp(keybindings.interact))
            {
                // Tells interactable object to run funtion 'Reset'
                interactable.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
            }

            // Get interactable objects
            // Variable for the object hit by raycast
            RaycastHit hit;
            // Check if raycast hits anything
            if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, maxDistance) && !Input.GetKey(keybindings.interact))
            {
                // Check if the hit object has the tag "Interactable"
                if (hit.collider.CompareTag("Interactable"))
                {
                    // Store hit object
                    interactable = hit.transform.gameObject;
                    // Set reticule color to red
                    reticule.color = Color.red;
                }
                else
                {
                    // Clear any previous hit objects
                    interactable = null;
                    // Set reticule color to black
                    reticule.color = Color.white;
                }
            }
            else if (!Input.GetKey(keybindings.interact))
            {
                // Clear any previous hit objects
                interactable = null;
                // Set reticule color to black
                reticule.color = Color.white;
            }


            // Interaction keypresses //

            // Interact with interactable objects

            // Check if there is an object to interact with and if left mouse button is clicked
            if (interactable && Input.GetKeyDown(keybindings.interact))
            {
                // Tell the interactable object to do something
                interactable.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                grabPoint.position = hit.point;
            }
            // Check if there is an object to interact with and if left mouse button is held
            if (interactable && Input.GetKey(keybindings.interact))
            {
                // Tell the object to execute function called "Hold" and sends the point where the payer is looking as a parameter
                //Debug.Log(grabPoint.position);
                interactable.SendMessage("Hold", grabPoint.position, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            // Moves character forward or backward
            //if (!playersFeet1.isPlaying)
            //{
            //    playersFeet1.PlayOneShot(walkingSound);
            //}
            playerRB.MovePosition(transform.position + (Vector3.Normalize(transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")) * speed * Time.deltaTime));
        }
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }
}
