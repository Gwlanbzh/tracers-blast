using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //Classe présent à chaque jeu, définissant le comportement que TOUS les mode de jeux auront

    public GameObject current_gamemode;
    public GameObject normal_ui;
    public GameObject pause_ui;

    public GameObject win_ui;

    public GameObject text_template;

    void Start()
    {
        current_gamemode.GetComponent<GameMode>().game_reset();
        win_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {
            game_pause();
        }
    }

    public void game_pause(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pause_ui.SetActive(true);
        normal_ui.SetActive(false);
        
    }

    public void game_resume(){
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        normal_ui.SetActive(true);
        pause_ui.SetActive(false);
        
    }

    public void end_game(){
        //Ce qu'on fait quand le joueur à gagner (en gros afficher l'UI de Damien/Alex)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        win_ui.SetActive(true);
        normal_ui.SetActive(false);
    }

    public void return_mainmenu(){
        // Destruction
        Destroy(GameObject.Find("OptionsValues"));
        SceneManager.LoadScene("Menus");
    }
    public void Pop_Up(string text){
        if (text_template){
            GameObject instance = Instantiate(text_template,transform.position,Quaternion.identity);
            instance.transform.SetParent(normal_ui.transform,false);
            instance.GetComponentInChildren<TMP_Text>().text = text;
            
        }
    }

}
