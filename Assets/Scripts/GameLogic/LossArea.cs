using UnityEngine;

public class LossArea : MonoBehaviour
{
    public GameObject gamemode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        PlayerBehaviour player = other.GetComponentInParent<PlayerBehaviour>();
        if (player != null){
            Debug.Log("toucher");
            //On appelle la fonction gamemode
            gamemode.GetComponentInParent<GameMode>().game_reset();
        }
    }
}
