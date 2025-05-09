using UnityEngine;
using UnityEngine.InputSystem;

public class RocketBehaviour : MonoBehaviour
{
    private float bulletSpeed = .5f;
    private float explosionForce = 5f;
    private float explosionRadius = 10f;
    private GameObject player;
    public GameObject explosionEffectPrefab;

	private AudioSource source;
	private float audio_volume_fire = .25f;
	private float audio_volume_explosion = .25f;
	public AudioClip rocket_fire_audioclip;
	public AudioClip rocket_explode_audioclip;

    public void setPlayer(GameObject p)
    {
        player = p;
    }

	void Start()
	{
		source = GetComponent<AudioSource>();
		// Play an audio clip
		source.PlayOneShot(rocket_fire_audioclip, audio_volume_fire);
	}

    void Update()
    {
        // debug feature
        if (Keyboard.current.rKey.isPressed && Keyboard.current.yKey.isPressed)
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

		// Play an audio clip
		source.PlayOneShot(rocket_explode_audioclip, audio_volume_explosion);
        
        // Add an explosion particle effect
		// We put it a bit backwards so that it lights a bit the surface we collided with 
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position - transform.forward, Quaternion.identity);
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
