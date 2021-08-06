using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayer : MonoBehaviour
{
    float _speed = 4.6f;

    [SerializeField]
    int spreiteID;

    [SerializeField]
    GameObject _playerShieldVisual;

    private void Start()
    {
        StartCoroutine(ActivateShieldVisuals());

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 9.5f)
        {
            transform.position = new Vector3(transform.position.x, -6, 0);
        }
    }

    IEnumerator ActivateShieldVisuals()
    {
        int randomActivationTime0 = Random.Range(2, 11);
        int randomActivationTime1 = Random.Range(3, 16);
        int randomActivationTime2 = Random.Range(5, 21);
        int randomActivationTime3 = Random.Range(11, 26);


        for (; ;)
        {
            if (spreiteID == 0)
            {
                yield return new WaitForSeconds(randomActivationTime0);
                _playerShieldVisual.SetActive(true);
                yield return new WaitForSeconds(randomActivationTime0);
                _playerShieldVisual.SetActive(false);
            }

            else if (spreiteID == 1)
            {
                yield return new WaitForSeconds(randomActivationTime1);
                _playerShieldVisual.SetActive(true);
                yield return new WaitForSeconds(randomActivationTime1);
                _playerShieldVisual.SetActive(false);
            }

            else if (spreiteID == 2)
            {
                yield return new WaitForSeconds(randomActivationTime2);
                _playerShieldVisual.SetActive(true);
                yield return new WaitForSeconds(randomActivationTime2);
                _playerShieldVisual.SetActive(false);
            }

            else if (spreiteID == 3)
            {
                yield return new WaitForSeconds(randomActivationTime3);
                _playerShieldVisual.SetActive(true);
                yield return new WaitForSeconds(randomActivationTime3);
                _playerShieldVisual.SetActive(false);
            }
        }
       
    }
}

    

 
