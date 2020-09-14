using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject m_enemyPrefab;
    [SerializeField] GameObject m_enemyContainer;
    [SerializeField] GameObject[] m_powerUps;

    

    [SerializeField] float m_timeToWait = 5f;

    private bool m_stopSpawn = false;    
    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerRoutine());
    }
    

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (m_stopSpawn == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(m_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = m_enemyContainer.transform;
            yield return new WaitForSeconds(m_timeToWait);
        }
    }

    IEnumerator SpawnPowerRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (m_stopSpawn == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 6, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(m_powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }        
    }
    public void OnPlayerDeath()
    {
        m_stopSpawn = true;
    }

    
}
