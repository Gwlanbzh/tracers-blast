using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private float bulletSpeed = 10f;
    private float explosionForce = 10f;
    private float explosionRadius = 20f;
    private GameObject player;

    public void setPlayer(GameObject p)
    {
        player = p;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey("r"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerBehaviour>().applyExplosionForce(explosionForce, transform.position, explosionRadius);
        // TODO : add an explosion animation with a particle system ?
        Destroy(gameObject, 1f);
    }
}
