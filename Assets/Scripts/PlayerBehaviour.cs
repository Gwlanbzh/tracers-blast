using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float currentPitch;
    private float movementForce;
    private float frictionForce;
    private float jumpForce;
    private float mouseSensitivity;

    private Vector3 velocity;

    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.freezeRotation = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        velocity = new Vector3(0f, 0f, 0f);
        frictionForce = 8f;
        movementForce = 1f;
        jumpForce = 5f;
        
        currentPitch = 0f;
        mouseSensitivity = 1000f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleKeys();
    }

    void handleKeys()
    {
        Vector3 wishdir = new Vector3(0f, 0f, 0f);
        
        if (Input.GetKey("w"))
        {
            // Debug.Log("Forward");
            // transform.Translate(Vector3.forward * Time.deltaTime);
            // GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed, ForceMode.Impulse);
            wishdir += transform.forward;
        }
        
        if (Input.GetKey("s"))
        {
            // Debug.Log("Backward");
            // transform.Translate(Vector3.back * Time.deltaTime);
            // GetComponent<Rigidbody>().AddForce(transform.forward * -1f * movementSpeed, ForceMode.Impulse);
            wishdir += -transform.forward;
        }
        
        if (Input.GetKey("a"))
        {
            // Debug.Log("Left");
            // transform.Translate(Vector3.left * Time.deltaTime);
            // GetComponent<Rigidbody>().AddForce(transform.right * -1f * movementSpeed, ForceMode.Impulse);
            wishdir += -transform.right;
        }
        
        if (Input.GetKey("d"))
        {
            // Debug.Log("Right");
            // transform.Translate(Vector3.right * Time.deltaTime);
            // GetComponent<Rigidbody>().AddForce(transform.right * movementSpeed, ForceMode.Impulse);
            wishdir += transform.right;
        }
        
        Vector3 acceleration = wishdir.normalized * movementForce
                             - velocity * frictionForce;
        
        velocity += acceleration * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);
        
        if (Input.GetKeyDown("space"))
        {
            // Debug.Log("Jump");
            // transform.Translate(Vector3.right * Time.deltaTime);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
        // mouse movement
        float h = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float v = - Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Debug.LogFormat("Horizontal: {0}", h);
        // Debug.LogFormat("Vertical: {0}", v);

        Transform camera = transform.Find("camera");
        
        /* === Garbage, dont use ===
        
        if (Input.GetKey("j"))
        {
            camera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -1.0f);
        }

        if (Input.GetKey("k"))
        {
            camera.transform.rotation = Quaternion.Euler(-1.0f, 0.0f, 0.0f);
        }

        if (Input.GetKey("l"))
        {
            camera.transform.rotation = Quaternion.Euler(1.0f, 0.0f, 0.0f);
        }

        if (Input.GetKey(";"))
        {
            camera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 1.0f);
        }
        
        */
        
        // Pitch rotation on camera
        if (currentPitch + v >= -90 && currentPitch + v <= 90)
        {
            camera.transform.Rotate(v, 0f, 0f, Space.Self);
            currentPitch += v;
        }

        // Yaw rotation on Player
        transform.Rotate(0f, h, 0f, Space.World);

        // Debug.Log(isOnGround() ? "Is grounded : " : "Floating");

        /*

        camera.transform.rotate(Quaternion.Euler(v, 0, h));

        */
    }

    /*
     * Check if the player is standing on ground.
     * TODO : To make this work on uneven ground (or at least kind of),
     *        cast 4 rays in a square shape under the player
     *        or something like this ? 
     */
    bool isOnGround()
    {
        return Physics.Raycast(transform.position + (new Vector3(0f, .1f, 0f)), -transform.up, .2f);
    }
}
