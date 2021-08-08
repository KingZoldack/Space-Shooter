using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] float _speed = 3f;

    //Assigning ID numbers to the different powerups.
    //0 = Triple Shot
    //1 = Speed Powerup
    //2 = Shield Powerup
    [SerializeField] public int powerupID;

    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -6.85f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldIsActive();
                        break;
                    case 3:
                        player.lifeCollected();
                        break;
                    default:
                        break;
                }
            }
            
            Destroy(this.gameObject);
        }
    }
}
