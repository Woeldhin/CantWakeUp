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
    // Animation component
    public Animation anim;

    // Variables for MagicMirror to use //
    // state toggles
    public bool flipped = false, mirrorCamera = false;
    // reference, obtained at Start()
    private CameraMirror camMirror;
    // number bools to flip controls around
    private float flipCameraX = 1, flipMovementX = 1;
    [SerializeField] // reference for room hight, nessesary for the MagicMirror to work properly
    private float roomHight = 3;

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
    //Audio clips
    public AudioClip jumpingSound;
    public AudioClip step;
    //Audio sources
    public AudioSource playersFeet4Jump;
    public AudioSource playersFeet4Walk;

    private void Start()
    {
        // Set the starting speed as standing speed just in case.
        speed = standSpeed;
        // Set paused to false
        paused = true;
        // Set grounded to true
        isGrounded = true;
        StartCoroutine(WakeUp());

        //Putting sounds to sources
        playersFeet4Jump.clip = jumpingSound;
        playersFeet4Walk.clip = step;

        camMirror = GetComponentInChildren<CameraMirror>();
    }

    void Update()
    {
        // Character movement

        if(!paused) {

            // Jump
            if (Input.GetKeyDown(keybindings.jump))
            {
                if (isGrounded)
                {
                    //Jump clip played
                    playersFeet4Jump.Play();
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
            rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity * flipCameraX;

            // Calculate vertical rotation
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            // Rotate character for the horizontal rotation (camera is following the character)
            transform.localEulerAngles = new Vector3(0, rotationX, transform.rotation.eulerAngles.z);
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
            playerRB.MovePosition(transform.position + (Vector3.Normalize(transform.forward * Input.GetAxisRaw("Vertical") + transform.right * flipMovementX * Input.GetAxisRaw("Horizontal")) * speed * Time.deltaTime));
            if(isGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && playersFeet4Walk.isPlaying == false)
            {
                playersFeet4Walk.pitch = Random.Range(0.8f, 1.15f);
                playersFeet4Walk.volume = Random.Range(0.75f, 1);
                playersFeet4Walk.PlayDelayed(0.1f);
            }
        }
        // for manual gravity, that may be reversed
        playerRB.AddForce(Physics.gravity * (flipped ? -1 : 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    // 'teleport' player to the other side of the mirror, aka flip him very spesific way
    public void Flip(GameObject portal, bool flip)
    {

        if (flip)
        {
            // get players reflected offset from the 'portal'
            Vector3 portalOffset = Vector3.Reflect(portal.transform.position - transform.position, portal.transform.up);
            // set y component to zero, so we may manually add room hight correction
            portalOffset.y = 0;
            // move player to new position with room hight correction added
            transform.position = portalOffset + new Vector3(portal.transform.position.x, roomHight - transform.position.y, portal.transform.position.z);
            // get new faceing direction for the camera
            Vector3 newCameraRotation = Vector3.Reflect(transform.forward, portal.transform.up);
            // rotate players camera and turn it upsidedown
            transform.rotation = Quaternion.LookRotation(newCameraRotation, -transform.up);

        }
        else
        {
            // get player newreflected faceing from the portal
            Vector3 newCameraRotation = Vector3.Reflect(transform.forward, portal.transform.up);
            // rotate player to face the new direction
            transform.rotation = Quaternion.LookRotation(newCameraRotation, transform.up);
        }

    }

    // flip the player upsidedown, or upright
    public void Flipped(bool flipped)
    {
        this.flipped = flipped;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (flipped ? 180 : 0));
        UpdateControls();
    }

    // Mirror player camera if necessery
    public void Mirroring(bool mirrored)
    {
        mirrorCamera = mirrored;
        if (camMirror != null)
        {
            camMirror.mirror = mirrorCamera;
        }
        UpdateControls();
    }

    // Update horizontal control flipping
    public void UpdateControls()
    {

        flipMovementX = mirrorCamera ? -1 : 1;

        if (flipped)
        {
            flipCameraX = mirrorCamera ? 1 : -1;
        }
        else
        {
            flipCameraX = mirrorCamera ? -1 : 1;
        }
    }

    IEnumerator WakeUp()
    {

        anim.Play("LieInBed");
        yield return new WaitForSeconds(1);
        anim.Play("WakeUp");
        yield return new WaitForSeconds(3);
        paused = false;
        yield return null;
    }
}
