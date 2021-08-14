using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    UIManager _uiManager;

    [SerializeField]
    Animator _pauseMenuAnimator;

    public int currentSceneIndex;



    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _uiManager = FindObjectOfType<UIManager>();
        _pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.P))
        {
            PauseGame();
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

    public void LoadMainMenuFromPause()
    {
        SceneManager.LoadScene(0);
        ResumeGame();
    }

    public void LoadSinglePlayerHelp()
    {
        SceneManager.LoadScene(2);
    }

    public void PauseGame()
    {
        _uiManager.pauseMenuPanel.SetActive(true);
        _pauseMenuAnimator.SetBool("isPaused", true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        
            Time.timeScale = 1;
            _uiManager.pauseMenuPanel.SetActive(false);
        
    }
}
