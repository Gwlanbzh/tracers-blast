using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float currentPitch;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentPitch = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        handleKeys();
    }

    void handleKeys()
    {
        if (Input.GetKey("w"))
        {
            Debug.Log("Forward");
            // transform.Translate(Vector3.forward * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.forward * .25f, ForceMode.Impulse);
        }
        
        if (Input.GetKey("s"))
        {
            Debug.Log("Backward");
            // transform.Translate(Vector3.back * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.forward * -1f * .25f, ForceMode.Impulse);
        }
        
        if (Input.GetKey("a"))
        {
            Debug.Log("Left");
            // transform.Translate(Vector3.left * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.right * -1f * .25f, ForceMode.Impulse);
        }
        
        if (Input.GetKey("d"))
        {
            Debug.Log("Right");
            // transform.Translate(Vector3.right * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.right * .25f, ForceMode.Impulse);
        }
        
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Jump");
            // transform.Translate(Vector3.right * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.up * (float)5, ForceMode.Impulse);
        }
        
        // mouse movement
        float mouseSensitivity = 1000f;
        
        float h = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float v = - Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Debug.LogFormat("Horizontal: {0}", h);
        Debug.LogFormat("Vertical: {0}", v);

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
}
