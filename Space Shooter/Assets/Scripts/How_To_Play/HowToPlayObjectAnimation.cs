using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayObjectAnimation : MonoBehaviour
{
    [SerializeField]
    float _astroidRotationSpeed = 2.5f;
    float _playerMoveSpeed = 0.5f;

    bool startMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateAstroid();

        //if (this.gameObject.tag == "How_To_Play_Player")
        //{
 
        //        transform.Translate(Vector3.right * _playerMoveSpeed * Time.deltaTime);


        //    if (transform.position.x >= -2.86f)
        //    {
        //        _playerMoveSpeed = -0.5f;
        //    }

        //    else if (transform.position.x <= -5.13f)
        //    {
        //        _playerMoveSpeed = 0.5f;
        //    }
        //}


    }

    public void RotateAstroid()
    {
        if (this.gameObject.tag == "Astroid")
        {
            transform.Rotate(Vector3.forward * _astroidRotationSpeed * Time.deltaTime);
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
