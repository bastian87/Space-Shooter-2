using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float m_rotation = 15f;
    [SerializeField] GameObject m_explosionPrefab;
    private SpawnManager m_spawnManager;

    void Start()
    {
        m_spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * m_rotation * Time.deltaTime);        
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(m_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            m_spawnManager.StartSpawning();
            Destroy(this.gameObject);          
        }
    }
}
