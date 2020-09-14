using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    // If a variable has "m_" is private, if not, then is public.
    [SerializeField] float m_speed = 10f;
    [SerializeField] float m_speedMultiplier = 2;
    [Header ("PowerUps")]
    [SerializeField] GameObject m_laserPrefab;
    [SerializeField] GameObject m_tripleLaserPrefab;    
    [SerializeField] GameObject m_shieldVisual;
    [SerializeField] GameObject m_damageVisualRight, m_damageVisualLeft;
    

    bool m_tripleLaserActive = false;
    bool m_speedBoostActive = false;
    bool m_shieldActive = false;
    

    [SerializeField] float m_tripleLaserDuration = 5f;
    [SerializeField] float m_speedBoostDuration = 5f;
    [SerializeField] float m_laserPosition = 1.17f;    
    [Header("Health")]
    [SerializeField] int m_lives = 3;

    [SerializeField] private float m_fireRate = 0.3f;
    private float m_canFire = -1f;

    [SerializeField]
    private int m_score;

    UIManager m_uiManager;

    private SpawnManager m_spawnManager;    

    void Start()
    {
        m_spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        m_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (m_spawnManager == null)
        {
            Debug.LogError("ERROR");
        }

        if (m_uiManager == null)
        {
            Debug.LogError("The UI is null");
        }
    }
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > m_canFire)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        m_canFire = Time.time + m_fireRate;        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_tripleLaserActive == true)
            {
                Instantiate(m_tripleLaserPrefab, transform.position, Quaternion.identity);
            }

            else
            {
                Instantiate(m_laserPrefab, transform.position + new Vector3(0, m_laserPosition, 0), Quaternion.identity);
            }
        }
    }
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
                
        transform.Translate(direction * m_speed * Time.deltaTime);
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 4.5f), 0);

        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }
    public void Damage()
    {
        if(m_shieldActive == true)
        {
            
            m_shieldActive = false;
            m_shieldVisual.SetActive(false);
            return;
        }
        
        m_lives--;
        m_uiManager.UpdateLives(m_lives);

        if (m_lives == 2)
        {
            m_damageVisualLeft.SetActive(true);
            return;
        }
        else if (m_lives == 1)
        {
            m_damageVisualLeft.SetActive(true);
            m_damageVisualRight.SetActive(true);
            return;
        }

        if (m_lives < 1)
        {
            m_spawnManager.OnPlayerDeath();
            
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleLaser()
    {
        m_tripleLaserActive = true;
        StartCoroutine(TripleLaserRoutine());
    }    
    public void ActivateSpeed()
    {
        m_speedBoostActive = true;
        m_speed *= m_speedMultiplier;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public void ActivateShield()
    {
        m_shieldActive = true;
        m_shieldVisual.gameObject.SetActive(true);
    }

    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(m_speedBoostDuration);
        m_speedBoostActive = false;
        m_speed /= m_speedMultiplier;
    }    
    IEnumerator TripleLaserRoutine()
    {
        yield return new WaitForSeconds(m_tripleLaserDuration);
        m_tripleLaserActive = false;
    }

    public void AddScore()
    {
        m_score += 10;
        m_uiManager.UpdateScore(m_score);        
    }    

}
