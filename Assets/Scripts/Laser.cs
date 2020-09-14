using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    [SerializeField] float m_laserSpeed = 9f;
    void Start()
    {

    }
    void Update()
    {
        LaserDirection();
    }

    private void LaserDirection()
    {
        transform.Translate(Vector3.up * m_laserSpeed * Time.deltaTime);

        if (transform.position.y > 6f)
        {
            Destroy(this.gameObject);
        }
    }

    
}
