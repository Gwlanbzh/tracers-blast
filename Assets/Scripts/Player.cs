using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Z pressed");
            GetComponent<Rigidbody>().AddForce(Vector3.forward);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("Z released");
            GetComponent<Rigidbody>().AddForce(Vector3.back);
        }
    }
}
