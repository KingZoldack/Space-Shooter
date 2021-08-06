using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _speed = 22.5f;
    [SerializeField] float _enemySpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Enemy_Laser")
        {
            FireEnemyLaser();
        }

        else
        {
            FirePlayerLaser();
        }
    }

    public void FirePlayerLaser()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
        }
    }

    public void FireEnemyLaser()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -6.18f)
        {
            Destroy(this.gameObject);
        }
    }
}
