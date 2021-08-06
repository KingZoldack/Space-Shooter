using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadScene("Single_Player_Mode");
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Single_Player_Mode");
    }
}
