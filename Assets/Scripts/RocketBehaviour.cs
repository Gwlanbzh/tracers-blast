using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private float bulletSpeed = .5f;
    private float explosionForce = 3f;
    private float explosionRadius = 10f;
    private GameObject player;
    public GameObject explosionEffectPrefab;

    public void setPlayer(GameObject p)
    {
        player = p;
    }

    void FixedUpdate()
    {
        // debug feature
        if (Input.GetKey("r") && Input.GetKey("y"))
        {
            // this removes all rockets from the scene
            Destroy(gameObject);
        }
        
        
        transform.Translate(Vector3.forward * bulletSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Apply an explosion force on the player
        player.GetComponent<PlayerBehaviour>().applyExplosionForce(explosionForce, transform.position, explosionRadius);

        Debug.Log((player.transform.position - transform.position).magnitude);
        
        // Add an explosion particle effect
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 5f);
        
        // Delete the capsule immediatly so that the rocket is no longer visible.
        // Disable the smoke effect from spawning new particles and make the whole prefab
        // disappear after all particles have faded.
        ParticleSystem smokeParticleSystem = transform.Find("SmokeEffect").gameObject.GetComponent<ParticleSystem>();
        GameObject capsule = transform.Find("Capsule").gameObject;
        smokeParticleSystem.Stop();
		Destroy(capsule);
        Destroy(gameObject, 5f);
    }
}
