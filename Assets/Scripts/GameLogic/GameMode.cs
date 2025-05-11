using System.Linq;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject game_manager;
    private static GameObject static_manager;
    public GameObject player;
    public GameObject time_text;
    TMP_Text text_compoenent; //Pour éviter d'appeler getCompoenent à chaque update

    public float base_time = 5.0f;

    public Vector3 spawn_location;
    static float remaingTime;

    public GameObject powerup_parent;

    void Start()
    {
        static_manager = game_manager;
        text_compoenent = time_text.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Gestion du text
        remaingTime -= Time.deltaTime;
        time_text.GetComponent<TMP_Text>().text = "Time reaminging: " + string.Format("{0:00}",remaingTime);
        if (remaingTime<0){respawn();}
    }

    public void game_won(){
        Debug.Log("The game is won");
        //Remplir ici les chose à faire avant d'afficher l'écran de victoire
        game_manager.GetComponent<GameManager>().end_game();
        
    }

    public void respawn(){
        player.transform.position = spawn_location;
        remaingTime = base_time;
        Debug.Log("Respawn of the player");
    }

    public void game_reset()
    {
        //MEMO : Peut être passé cette fonction en private
        Debug.Log("test");
        game_manager.GetComponent<GameManager>().game_resume();
        respawn();
        for (int i = 0; i < powerup_parent.transform.childCount; i++){
            //Division en 2 étape pour la clareté
            GameObject child = powerup_parent.transform.GetChild(i).gameObject;
            child.transform.Find("PowerUp").gameObject.SetActive(true);
        }
    }

    public static void add_time(float time){
        static_manager.GetComponent<GameManager>().Pop_Up("+"+time+"s");
        remaingTime += time;
    }
    public static float get_time(){
        return remaingTime;
    }
}
