using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -13.67f)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, 13.67f, 2);
            transform.position = spawnPos;
        }
    }

}


