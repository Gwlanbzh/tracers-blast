using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Vector3 velocity;
    
    // Player physics settings
    private float movementForceOnGround;
    private float movementForceFloating;  // aka air control
    private float jumpForce;
    private float frictionForce;
    
    // Mouse control
    private float currentPitch;
    private float mouseSensitivity;

    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.freezeRotation = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // Player physics
        velocity = new Vector3(0f, 0f, 0f);
        movementForceOnGround = 1f;
        movementForceFloating = .05f;
        jumpForce = 1.25f;
        frictionForce = 8f;
        
        // mouse controls
        currentPitch = 0f;
        mouseSensitivity = 1000f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleKeys();
        Debug.Log(isOnGround() ? "Grounded" : "Floating");
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

        Vector3 acceleration;

        if (isOnGround())
        {
            acceleration = wishdir.normalized * movementForceOnGround
                         - velocity * frictionForce;

        }
        else
        {
            acceleration = wishdir.normalized * movementForceFloating;
        }
        
        velocity += acceleration * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);  // FIXME : should have a * Time.fixedDeltaTime
        
        if (Input.GetKey("space"))
        {
            // Debug.Log("Jump");
            // transform.Translate(Vector3.right * Time.deltaTime);
            if (isOnGround())
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

        /*

        camera.transform.rotate(Quaternion.Euler(v, 0, h));

        */
    }

    /*
     * Check if the player is standing on ground.
     * 4 rays are cast in a circle shape under the player. 
     */
    bool isOnGround()
    {
        float maxDistanceFromPlayer = .2f;  // The distance at whichthe raycast ends BELOW the player.
        float sourceYOffset = .05f;         // You have to start ABOVE the ground, because apparently
                                            // the ray won't intersect a surface at its origin point. 
        float sourceRadius = .25f;          // Radius of the "circle" on which the rays are cast.
        
        return Physics.Raycast(transform.position + (new Vector3( sourceRadius, sourceYOffset,            0f)), -transform.up, sourceYOffset + maxDistanceFromPlayer)
            || Physics.Raycast(transform.position + (new Vector3(-sourceRadius, sourceYOffset,            0f)), -transform.up, sourceYOffset + maxDistanceFromPlayer)
            || Physics.Raycast(transform.position + (new Vector3(           0f, sourceYOffset,  sourceRadius)), -transform.up, sourceYOffset + maxDistanceFromPlayer)
            || Physics.Raycast(transform.position + (new Vector3(           0f, sourceYOffset, -sourceRadius)), -transform.up, sourceYOffset + maxDistanceFromPlayer)
            ;
    }
}
