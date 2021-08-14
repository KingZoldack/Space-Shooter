using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    SceneManagement _sceneManagement;

    [SerializeField]
    public GameObject _singlePlayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _sceneManagement = FindObjectOfType<SceneManagement>();
    }

    //public void TurnOnSP()
    //{
    //    if (this.gameObject.activeInHierarchy)
    //    {
    //        this.gameObject.SetActive(false);
    //        _sceneManagement._singlePlayer.SetActive(true);
    //    }
    //}
}
