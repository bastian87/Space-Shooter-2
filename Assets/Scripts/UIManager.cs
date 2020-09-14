using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [Header("Texts")]
    [SerializeField] Text m_scoreText;
    [SerializeField] Text m_gameOverText;
    [SerializeField] Text m_restartText;

    [Header("Lives Display Image")]
    [SerializeField] Image m_livesImage;

    [Header("Number of Lives")]
    [SerializeField] Sprite[] m_liveSprites;

    GameManager m_gameManager;
    void Start()
    {
        m_scoreText.text = "Score: " + 0;
        m_gameOverText.gameObject.SetActive(false);
        m_restartText.gameObject.SetActive(false);
        m_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (m_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }

    }
    public void UpdateScore(int playerScore)
    {
        m_scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        m_livesImage.sprite = m_liveSprites[currentLives];
        if (currentLives == 0)
        {

            GameOverSequence();
        }
    }
    private void GameOverSequence()
    {
        m_gameOverText.gameObject.SetActive(true);
        m_restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFLickerRoutine());
        m_gameManager.GameOver();
        
        

    }

    IEnumerator GameOverFLickerRoutine()
    {
        while (true)
        {
            // This way we can flick the Game Over Text or any text like 80's arcade games.
            m_gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            m_gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    
}
