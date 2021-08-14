using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    [SerializeField] float _speedBoostValue = 8.5f;
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] int _lives = 3;
    SpawnManger sapwnManager;
    [SerializeField] float _fireRate = 0.5f;
    float _canFire = -1f;

    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;


    bool isTripleShotActive = false;
    bool isSpeedBoostActive = false;
    bool isShieldActive = false;
    bool startTimer = false;
    [SerializeField] float timer = 30;

    [SerializeField] GameObject _playerShieldVisual;
    [SerializeField] GameObject _rightEngineDamged;
    [SerializeField] GameObject _leftEngineDamaged;
    [SerializeField] GameObject[] powerups;

    public int score = 0;
    public int bestScore;
    [SerializeField] int pointsPerKill = 10;

    AudioManager _audioManager;
    AudioSource audioManagerAudioSource;

    [SerializeField]
    Animator explosionAnimation;
  
    [SerializeField]
    Animator _playerTurn;
    [SerializeField]
    GameObject _explosionPrefab;

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        _audioManager = FindObjectOfType<AudioManager>();
         audioManagerAudioSource = _audioManager.GetComponent<AudioSource>();
        
        sapwnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManger>();
        if (sapwnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }

        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        uiManager.bestScoretext.text = "Best Score: " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne == true)
        {
            CalculatePlayerOneMovement();

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }

            if (startTimer == true)
            {
                timer -= 1 * Time.deltaTime;
            }
        }

        else if (isPlayerTwo == true)
        {
            CalculatePlayerTwoMovement();

            if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > _canFire)
            {
                FireLaser();
            }

            if (startTimer == true)
            {
                timer -= 1 * Time.deltaTime;
            }
        }
        
    }

    private void FireLaser()
    {
        Vector3 laserOffset = new Vector3(0, 1.05f, 0);
        Vector3 tripleShotOffset = new Vector3(0, -1.45f, 0);
        _canFire = Time.time + _fireRate;
        if (isTripleShotActive == true)
        {
            
            Instantiate(_tripleShotPrefab, transform.position + tripleShotOffset, Quaternion.identity);

        }

        else
        {
            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }

        audioManagerAudioSource.PlayOneShot(_audioManager.laserSound, _audioManager.laserSoundVolume);
    }

    void CalculatePlayerOneMovement()
    {
        if (isSpeedBoostActive == false)
        {
            MovePlayerOneLeftAndRight();
        }

        else if (isSpeedBoostActive == true)
        {
            _speed = _speedBoostValue;
            MovePlayerOneLeftAndRight();
        }

        //Clamps the movement on the y axis.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.6f, 0), 0);

        if (transform.position.x >= 10.3f)
        {
            transform.position = new Vector3(-10.3f, transform.position.y, 0);
        }

        else if (transform.position.x <= -10.3f)
        {
            transform.position = new Vector3(10.3f, transform.position.y, 0);
        }
    }

    void CalculatePlayerTwoMovement()
    {
        if (isSpeedBoostActive == false)
        {
            MovePlayerTwoLeftAndRight();
        }

        else if (isSpeedBoostActive == true)
        {
            _speed = _speedBoostValue;
            MovePlayerTwoLeftAndRight();
        }

        //Clamps the movement on the y axis.
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.6f, 0), 0);

        if (transform.position.x >= 10.3f)
        {
            transform.position = new Vector3(-10.3f, transform.position.y, 0);
        }

        else if (transform.position.x <= -10.3f)
        {
            transform.position = new Vector3(10.3f, transform.position.y, 0);
        }
    }

    public void MovePlayerOneLeftAndRight()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        _playerTurn.SetFloat("moveSpeed", direction.x);
    }

    public void MovePlayerTwoLeftAndRight()
    {
        _playerTurn.SetBool("isLeft", false);
        _playerTurn.SetBool("isRight", false);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _playerTurn.SetBool("isLeft", true);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _playerTurn.SetBool("isRight", true);
        }
    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            audioManagerAudioSource.PlayOneShot(_audioManager.shieldPowerdownSound, _audioManager.shieldPowerdownSoundVolume);
            isShieldActive = false;
            _playerShieldVisual.SetActive(false);
            return;
        }

        _lives--;
        uiManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _rightEngineDamged.SetActive(true);
            audioManagerAudioSource.PlayOneShot(_audioManager.engineDamagedSound, _audioManager.engineDamagedSoundVolume);
        }
        
        else if (_lives == 1)
        {
            _leftEngineDamaged.SetActive(true);
            audioManagerAudioSource.PlayOneShot(_audioManager.engineDamagedSound, _audioManager.engineDamagedSoundVolume);
        }

        if (_lives == 0)
        {
            audioManagerAudioSource.PlayOneShot(_audioManager.explosionSound, _audioManager.explosionSoundVoulme);
            sapwnManager.OnPlayerDeath();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            _rightEngineDamged.SetActive(false);
            _leftEngineDamaged.SetActive(false);
            Destroy(this.gameObject, 1f);
            GameObject explode = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explode.SetActive(true);
            _speed = 0f;
            Destroy(explode, 2f);
            uiManager.GameOver();
            BestScoreCheck();
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        audioManagerAudioSource.PlayOneShot(_audioManager.collectedPowerupSound, _audioManager.collectedPowerupSoundVolume);
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        while (isTripleShotActive == true)
        {
            yield return new WaitForSeconds(10);
            isTripleShotActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        audioManagerAudioSource.PlayOneShot(_audioManager.collectedPowerupSound, _audioManager.collectedPowerupSoundVolume);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        while (isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(10);
            isSpeedBoostActive = false;
            _speed = 5f;
        }
    }

    public void ShieldIsActive()
    {
        isShieldActive = true;
        audioManagerAudioSource.PlayOneShot(_audioManager.collectedPowerupSound, _audioManager.collectedPowerupSoundVolume);
        _playerShieldVisual.SetActive(true);
    }

    public void lifeCollected()
    {
        audioManagerAudioSource.PlayOneShot(_audioManager.collectedPowerupSound, _audioManager.collectedPowerupSoundVolume);
        _lives++;
        uiManager.UpdateLives(_lives);
        
        if (_lives == 3)
        {
            _rightEngineDamged.SetActive(false);
        }

        if (_lives == 2)
        {
            _leftEngineDamaged.SetActive(false);
        }
    }

    public void AddToScore()
    {
        score += pointsPerKill;
    }

    public void BestScoreCheck()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            //uiManager.bestScoretext.text = "Best Score: " + bestScore;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Laser")
        {
            Destroy(other.gameObject);
            Damage();
        }
    }

    public void HandlePowerupSpawn()
    {
        int randomPowerup = Random.Range(0, 4);

        Vector3 posToSpawn = new Vector3(Random.Range(-9.16f, 9.16f), 6.64f, 0);
        if (isShieldActive == false && randomPowerup != 3)
        {
            GameObject newPowerup = Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
        }

        //This line prevents shields from spawning if they are already active.
        else if (isShieldActive == true && randomPowerup != 3)
        {
            int dontSpawnShields = Random.Range(0, 2);
            GameObject newPowerup = Instantiate(powerups[dontSpawnShields], posToSpawn, Quaternion.identity);
        }

        if (_lives != 3 && randomPowerup == 3)
        {
            startTimer = true;

            if (timer <= 0)
            {
                GameObject newPowerup = Instantiate(powerups[3], posToSpawn, Quaternion.identity);
                timer = 30;
            }
        }
    }
}
