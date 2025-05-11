using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float seconde_to_destroy = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,seconde_to_destroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
