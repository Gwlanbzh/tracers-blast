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
    
    public GameObject rocketPrefab;
    
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
        mouseSensitivity = 2.4f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleKeys();
    }

    void handleKeys()
    {
        /* === Movements === */
        Vector3 wishdir = new Vector3(0f, 0f, 0f);
        
        if (Input.GetKey("w"))
        {
            wishdir += transform.forward;
        }
        
        if (Input.GetKey("s"))
        {
            wishdir += -transform.forward;
        }
        
        if (Input.GetKey("a"))
        {
            wishdir += -transform.right;
        }
        
        if (Input.GetKey("d"))
        {
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
            if (isOnGround())
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
        // mouse movement
        float h = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float v = - Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        Transform camera = transform.Find("camera");
        
        // Pitch rotation on camera
        if (currentPitch + v >= -90 && currentPitch + v <= 90)
        {
            camera.transform.Rotate(v, 0f, 0f, Space.Self);
            currentPitch += v;
        }

        // Yaw rotation on Player
        transform.Rotate(0f, h, 0f, Space.World);
        /* === End of Movements === */
        
        /* === Weapon === */
        if (Input.GetMouseButtonDown(0))
        {
            Transform bulletSpawnLocation = transform.Find("camera").Find("weapon").Find("BulletSpawn");
            GameObject bullet = Instantiate(rocketPrefab, bulletSpawnLocation.position + bulletSpawnLocation.forward * .25f, camera.transform.rotation);
            bullet.GetComponent<RocketBehaviour>().setPlayer(gameObject);
        }
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

    public void applyExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
		// TODO: this doesn't work well with the custom movement physics. Should be re-done with the jump
		// and a custom gravity.
        rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0f, ForceMode.Impulse);
    }
}
