using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed = 3.5f;
    [SerializeField]
    Animator explosionAnimation;
    [SerializeField]
    GameObject _explosionPrefab;

    AudioManager _audioManager;
    AudioSource audioManagerAudioSource;

    SpawnManger spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        explosionAnimation = FindObjectOfType<Animator>();
        spawnManager = FindObjectOfType<SpawnManger>();
        _audioManager = FindObjectOfType<AudioManager>();
        audioManagerAudioSource = _audioManager.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 1f);
            GameObject explode = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            audioManagerAudioSource.PlayOneShot(_audioManager.explosionSound, _audioManager.explosionSoundVoulme);
            explode.SetActive(true);
            _rotationSpeed = 0;
            Destroy(explode, 2f);
            spawnManager.StartSpawning();
        }
    }
}
