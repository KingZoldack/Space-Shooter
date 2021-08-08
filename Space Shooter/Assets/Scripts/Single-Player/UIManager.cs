using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Scoretext;
    [SerializeField] TextMeshProUGUI _gameOverText;
    [SerializeField] TextMeshProUGUI _restartText;

    [SerializeField] Image _livesImage;
    [SerializeField] Sprite[] _livesSprites;

    bool _isGameOver = false;

    Player player;
    SceneManagement sceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        sceneManagement = FindObjectOfType<SceneManagement>();
        _gameOverText.enabled = false;
        _restartText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Scoretext.text = "Score: " + player.score;
        
        if (_isGameOver == true)
        {
            sceneManagement.NewGame();
        }
    }

    public void UpdateLives(int currentLives)
    {
        //Live sometime fall below 0 if plaer gets damaged twice with one life
        //Life throws the array out of index and throws an error
        //line below fixes that
        if (currentLives < 0)
        {
            currentLives = 0;
        }
        _livesImage.sprite = _livesSprites[currentLives];
    }

    public void GameOver()
    {
        StartCoroutine(GameOverTextFlicker());
        _restartText.enabled = true;
        _isGameOver = true;
    }

    IEnumerator GameOverTextFlicker()
    {
        while (true)
        {
            _gameOverText.enabled = true;
            yield return new WaitForSeconds(1);
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
        
