using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayObjectAnimation : MonoBehaviour
{
    [SerializeField]
    float _astroidRotationSpeed = 2.5f;
    float _playerMoveSpeed = 1.5f;

    bool startMovement = false;

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

        //    if (transform.position.x >= 10.50f)
        //    {
        //        Vector3 warpToRight = new Vector3(-10.03f, transform.position.y, transform.position.z);
        //        transform.position = warpToRight;
        //    }

        //    //if (transform.position.x == -7.93)
        //    //{
        //    //    gameObject.SetActive(false);
        //    //}



        //    //else if (transform.position.x <= -5.13f)
        //    //{
        //    //    _playerMoveSpeed = 0.5f;
        //    //}
        //}


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


    //IEnumerator MovingPlayer()
    //{
    //    while (startMovement == true)
    //    {
    //        Vector3 leftPos = new Vector3(-5.13f, transform.position.y, transform.position.z);
    //        Vector3 rightPos = new Vector3(-2.86f, transform.position.y, transform.position.z);

    //        yield return new WaitForSeconds(0);
    //        transform.Translate(Vector3.right * _playerMoveSpeed * Time.deltaTime);

    //        if (transform.position.x >= -2.86f)
    //        {
    //            yield return new WaitForSeconds(0);
    //            transform.Translate(Vector3.left * _playerMoveSpeed * Time.deltaTime);
    //            yield return new WaitForSeconds(5);

    //        }


    //        //transform.Translate(leftPos * _playerMoveSpeed * Time.deltaTime);
    //        //yield return new WaitForSeconds(3);

    //    }
    //}
}
