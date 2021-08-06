using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(StartScrollRoutine());

        //if (transform.position.y >= 14f)
        //{
        //    transform.position = new Vector3(transform.position.x, -14f, 0);
        //}
    }

    IEnumerator StartScrollRoutine()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        for (; ;)
        {
            if (transform.position.y >= 14f)
            {
                transform.position = new Vector3(transform.position.x, -14f, 0);
            }
        }
    }
}


