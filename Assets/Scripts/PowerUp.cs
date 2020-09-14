using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float m_speed = 3f;
    // ID for powers
    // 0 = Triple
    // 1 = Speed
    // 2 = Shield
    [SerializeField] int powerupID;


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }

    private void Move()
    {
        transform.Translate(Vector2.down * m_speed * Time.deltaTime);
        Destroy(this.gameObject, 5f);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.ActivateTripleLaser();
                        break;
                    case 1:
                        player.ActivateSpeed();                        
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
                
            }
            Destroy(this.gameObject);

        }
    }
}
