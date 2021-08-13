using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayObjectAnimation : MonoBehaviour
{
    [SerializeField]
    float _astroidRotationSpeed = 2.5f;
    float _playerMoveSpeed = 1.5f;

    [SerializeField]
    float _backgroundMoveSpeed = 3.5f;

    [SerializeField]
    float meteorMoveSpeed = 1.0f;

    bool startMovement = false;
    bool _moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateAstroid();

        if (this.gameObject.tag == "Warp_Text_Player")
        {
           
           transform.Translate(Vector3.up * _playerMoveSpeed * Time.deltaTime);
        }

       // MeteorMovement();
    }

    public void RotateAstroid()
    {
        if (this.gameObject.tag == "Astroid")
        {
            transform.Rotate(Vector3.forward * _astroidRotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Teleport_To_Right")
        {
            Vector3 warpToRight = new Vector3(-10.03f, transform.position.y, transform.position.z);
            transform.position = warpToRight;
        }

        else if (other.tag == "Turn_Back_On_Left_Side")
        {
            Vector3 faceLeft = new Vector3(0f, 0f, 90f);
            //this.gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(faceLeft);
        }

        else if (other.tag == "Teleport_To_Left")
        {
            Vector3 warpToLeft = new Vector3(10.23f, transform.position.y, transform.position.z);
            transform.position = warpToLeft;
        }

        else if (other.tag == "Turn_Back_On_Right_Side")
        {
            Vector3 faceRight = new Vector3(0f, 0f, -90f);
            //this.gameObject.SetActive(false);
            transform.rotation = Quaternion.Euler(faceRight);
        }

    }

    //private void MeteorMovement()
    //{
    //    if (this.gameObject.tag == "Meteor")
    //    {
    //        transform.Translate(Vector3.up * meteorMoveSpeed * Time.deltaTime);
    //        transform.Translate(Vector3.right * meteorMoveSpeed * Time.deltaTime);
    //    }
    //}
}
