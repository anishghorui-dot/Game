using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("UI References")]
    public Text scoreText;
    public Text coinsText;
    public Text lettersText;
    public GameObject gameOverPanel;
    public Text finalScoreText;
    public GameObject splashScreen;
    public Text nameDisplayText;
    
    [Header("Game Stats")]
    private float score = 0;
    private int coins = 0;
    private List<bool> collectedLetters = new List<bool> { false, false, false, false, false };
    private string[] anishLetters = { "A", "N", "I", "S", "H" };
    
    [Header("Settings")]
    public float scoreMultiplier = 10f;
    public int letterBonus = 500;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Time.timeScale = 0; // Pause until game starts
        UpdateUI();
        
        if (splashScreen != null)
        {
            splashScreen.SetActive(true);
        }
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    
    public void StartGame()
    {
        Time.timeScale = 1;
        
        if (splashScreen != null)
        {
            splashScreen.SetActive(false);
        }
        
        ResetGame();
    }
    
    void ResetGame()
    {
        score = 0;
        coins = 0;
        collectedLetters = new List<bool> { false, false, false, false, false };
        UpdateUI();
    }
    
    public void UpdateScore(float amount)
    {
        score += amount * scoreMultiplier;
        UpdateUI();
    }
    
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }
    
    public void CollectLetter(int index)
    {
        if (index >= 0 && index < collectedLetters.Count)
        {
            if (!collectedLetters[index])
            {
                collectedLetters[index] = true;
                AddCoins(50);
                
                // Check if all letters collected
                bool allCollected = true;
                foreach (bool letter in collectedLetters)
                {
                    if (!letter)
                    {
                        allCollected = false;
                        break;
                    }
                }
                
                if (allCollected)
                {
                    // Bonus for collecting all letters
                    AddCoins(letterBonus);
                    ShowNameBonus();
                    
                    // Reset letters for next collection
                    collectedLetters = new List<bool> { false, false, false, false, false };
                }
                
                UpdateUI();
            }
        }
    }
    
    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
        
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + coins;
        }
        
        if (lettersText != null)
        {
            string letterDisplay = "";
            for (int i = 0; i < anishLetters.Length; i++)
            {
                if (collectedLetters[i])
                {
                    letterDisplay += anishLetters[i] + " ";
                }
                else
                {
                    letterDisplay += "_ ";
                }
            }
            lettersText.text = letterDisplay;
        }
    }
    
    void ShowNameBonus()
    {
        if (nameDisplayText != null)
        {
            nameDisplayText.text = "🎉 ANISH BONUS +500! 🎉";
            nameDisplayText.gameObject.SetActive(true);
            Invoke("HideNameBonus", 2f);
        }
    }
    
    void HideNameBonus()
    {
        if (nameDisplayText != null)
        {
            nameDisplayText.gameObject.SetActive(false);
        }
    }
    
    public void GameOver()
    {
        Time.timeScale = 0;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + Mathf.FloorToInt(score) + "\nCoins: " + coins;
        }
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
