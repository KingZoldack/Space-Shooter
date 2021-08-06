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
    [SerializeField] int pointsPerKill = 10;

    [SerializeField] AudioClip _laserSound;
    [SerializeField] AudioClip _powerupSound;
    [SerializeField] AudioClip _engineDamaged;
    [SerializeField] AudioClip _explosionSound;
    AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laserSound;

        sapwnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManger>();
        if (sapwnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (startTimer == true)
        {
            timer -= 1 * Time.deltaTime;
            Debug.Log(timer);
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

        _audioSource.Play();
    }

    void CalculateMovement()
    {
        if (isSpeedBoostActive == false)
        {
            MoveLeftAndRight();
        }

        else if (isSpeedBoostActive == true)
        {
            _speed = _speedBoostValue;
            MoveLeftAndRight();
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

    public void MoveLeftAndRight()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        _playerTurn.SetFloat("moveSpeed", direction.x);
    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            _playerShieldVisual.SetActive(false);
            return;
        }

       _lives--;
        uiManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _rightEngineDamged.SetActive(true);
            _audioSource.PlayOneShot(_engineDamaged);
        }

        else if (_lives == 1)
        {
            _leftEngineDamaged.SetActive(true);
            _audioSource.PlayOneShot(_engineDamaged);
        }

        if (_lives == 0)
        {
            _audioSource.PlayOneShot(_explosionSound);
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
        }


    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        _audioSource.PlayOneShot(_powerupSound);
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        while (isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5);
            isTripleShotActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        _audioSource.PlayOneShot(_powerupSound);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {

        while (isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(5);
            isSpeedBoostActive = false;
            _speed = 5f;
        }
    }

    public void ShieldIsActive()
    {
        isShieldActive = true;
        _audioSource.PlayOneShot(_powerupSound);
        _playerShieldVisual.SetActive(true);

    }

    public void lifeCollected()
    {
        _audioSource.PlayOneShot(_powerupSound);
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
