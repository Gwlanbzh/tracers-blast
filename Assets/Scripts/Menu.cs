using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas PrincipalCanvas;
    public Canvas OptionCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameLogic");
    }

    public void goToLobby()
    {
        PrincipalCanvas.gameObject.SetActive(true);
        OptionCanvas.gameObject.SetActive(false);
    }
    public void goToOption()
    {
        PrincipalCanvas.gameObject.SetActive(false);
        OptionCanvas.gameObject.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
