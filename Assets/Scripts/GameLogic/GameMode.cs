using System.Linq;
using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject game_manager;
    private static GameObject static_manager;
    public GameObject player;
    public GameObject time_text;
	public bool isTutorial; 
    TMP_Text text_compoenent; //Pour éviter d'appeler getCompoenent à chaque update

    public float base_time = 5.0f;

    private Vector3 spawn_location;
    static float remaingTime;

    public GameObject powerup_parent;

    void Awake()
    {
        static_manager = game_manager;
        text_compoenent = time_text.GetComponent<TMP_Text>();
        spawn_location = player.transform.position;

		if (isTutorial)
	        time_text.GetComponent<TMP_Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
		// si on est dans un tutoriel on laisse un temps infini au joueur
		if (isTutorial)
			return;

        //Gestion du text
        remaingTime -= Time.deltaTime;
        time_text.GetComponent<TMP_Text>().text = "Time remaining: " + string.Format("{0:00}",remaingTime);
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
    }

    public void game_reset()
    {
        game_manager.GetComponent<GameManager>().game_resume();
        respawn();
        for (int i = 0; i < powerup_parent.transform.childCount; i++){
            //Division en 2 étape pour la clareté
            GameObject child = powerup_parent.transform.GetChild(i).gameObject;
            child.transform.Find("PowerUp").gameObject.SetActive(true);
            // child.transform.Find("PowerUp").Find("Capsule").GetComponent<Animator>().Play("Idle");
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
