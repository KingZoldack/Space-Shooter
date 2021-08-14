using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    UIManager _uiManager;

    public int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void NewGame()
    {
        if (Input.GetKeyDown("r"))
        {
            if (_uiManager.isCoOpMode == false)
            {
                SceneManager.LoadScene("Single_Player_Mode");
            }

            else if (_uiManager.isCoOpMode == true)
            {
                SceneManager.LoadScene("Co-Op_Mode");
            }
        }

        else if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadSinglePlayerScene()
    {
        SceneManager.LoadScene("Single_Player_Mode");
    }

    public void LoadMultiplayerPlayerScene()
    {
        SceneManager.LoadScene("Co-Op_Mode");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSPMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSinglePlayerHelp()
    {
        SceneManager.LoadScene(2);
    }
}
