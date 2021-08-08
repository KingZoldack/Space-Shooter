using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayObjectAnimation : MonoBehaviour
{
    [SerializeField]
    float _astroidRotationSpeed = 3.5f;
    float _playerMoveSpeed = 1.5f;

    bool startMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag == "How_To_Play_Player")
        {
            startMovement = true;
            StartCoroutine(MovingPlayer());
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotateAstroid();

        
    }

    public void RotateAstroid()
    {
        if (this.gameObject.tag == "Astroid")
        {
            transform.Rotate(Vector3.forward * _astroidRotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator MovingPlayer()
    {
        while (startMovement == true)
        {
            Vector3 leftPos = new Vector3(-349.91f, transform.position.y, transform.position.z);
            Vector3 rightPos = new Vector3(-348.4f, transform.position.y, transform.position.z);

            transform.Translate(Vector3.right * _playerMoveSpeed * Time.deltaTime);

            if (transform.position.x <= -348.4)
            {
                yield return new WaitForSeconds(3);
                transform.Translate(Vector3.left * _playerMoveSpeed * Time.deltaTime);
            }
            //transform.Translate(leftPos * _playerMoveSpeed * Time.deltaTime);
            //yield return new WaitForSeconds(3);

        }
    }
}
