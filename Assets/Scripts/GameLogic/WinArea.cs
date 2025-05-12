using UnityEngine;

public class WinArea : MonoBehaviour
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
            //On appelle la fonction gamemode
            gamemode.GetComponentInParent<GameMode>().game_won();
        }
    }
}
