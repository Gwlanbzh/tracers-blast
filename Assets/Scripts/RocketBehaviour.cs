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
        
        // Add an explosion particle effect
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1f);
		// Destroy the light sooner than the rest for a better effect
		Destroy(explosionEffect.transform.Find("Light").gameObject, .2f);
        
        // Disable the children/components that produce a visible output.
        // Disable the smoke effect from spawning new particles and destroy the whole prefab
        // only *after* all particles have faded.
        ParticleSystem smokeParticleSystem = transform.Find("SmokeEffect").gameObject.GetComponent<ParticleSystem>();
        GameObject capsule = transform.Find("Capsule").gameObject;
		CapsuleCollider cc = GetComponent<CapsuleCollider>();

        smokeParticleSystem.Stop();
		cc.enabled = false;
		capsule.SetActive(false);
        Destroy(gameObject, 5f);
    }
}
