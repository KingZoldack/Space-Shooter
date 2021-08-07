using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject[] powerups;
     Player _player;
    Powerup _powerupID;
    bool _stopSpawning = false;

    
   


   public void StartSpawning()
    {
        _player = FindObjectOfType<Player>();
        _powerupID = FindObjectOfType<Powerup>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.6f, 9.6f), 6.35f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        while (_stopSpawning == false)
        {
            //Debug.Log(_player.randomPowerup);
            _player.HandlePowerupSpawn();
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void StopPowerupSpawn()
    {
        _stopSpawning = true;
    }
}
