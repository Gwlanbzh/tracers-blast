using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject options = GameObject.Find("OptionsValues");
        gameObject.GetComponent<Camera>().fieldOfView = options.GetComponent<OptionsValues>().getFOV();
    }
}
