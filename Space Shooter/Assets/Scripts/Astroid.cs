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
    [SerializeField] AudioClip _explosionSound;

    AudioSource _audioSource;

    SpawnManger spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        explosionAnimation = FindObjectOfType<Animator>();
        spawnManager = FindObjectOfType<SpawnManger>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosionSound;
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
            _audioSource.Play();
            explode.SetActive(true);
            _rotationSpeed = 0;
            Destroy(explode, 2f);
            spawnManager.StartSpawning();
            
        }
    }
}
