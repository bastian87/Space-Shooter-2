using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool m_isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void GameOver()
    {
        m_isGameOver = true;
    }
}
