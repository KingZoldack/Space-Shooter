using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    float _playerObjectMoveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Rotating_Player")
        {
            transform.Translate(Vector3.up * _playerObjectMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Move_Down")
        {
            Vector3 faceDown = new Vector3(0f, 0f, -180f);
            transform.rotation = Quaternion.Euler(faceDown);
        }

        if (other.gameObject.tag == "Move_Left")
        {
            Vector3 faceLeft = new Vector3(0f, 0f, 90f);
            transform.rotation = Quaternion.Euler(faceLeft);
        }

        if (other.gameObject.tag == "Move_Up")
        {
            Vector3 faceUp = new Vector3(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(faceUp);
        }

        if (other.gameObject.tag == "Move_Right")
        {
            Vector3 faceRight = new Vector3(0f, 0f, -90f);
            transform.rotation = Quaternion.Euler(faceRight);
        }
    }
    
}
