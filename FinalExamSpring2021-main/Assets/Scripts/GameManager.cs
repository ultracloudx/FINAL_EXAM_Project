using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text nameDisplay;
    public GameObject PauseMenuUI;

    [SerializeField]
    public int playerScore;
    public int playerLives;
    //public float currTime;
    private string playerName;

    //Buttons
    Button decreaseScoreBut;
    Button increaseScoreBut;
    Button decreaseLivesBut;
    Button increaseLivesBut;

    //Clock
    private float timeRemaining = 60f;
    public bool timerIsRunning = false;
    public Text timeText;



    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1f;
        playerScore = PlayerPrefs.GetInt("loadedScore");
        playerLives = PlayerPrefs.GetInt("currLivesPref");
        timeRemaining = PlayerPrefs.GetFloat("currTIME");
        playerName = PlayerPrefs.GetString("playerNAME");

        //Clock
        timerIsRunning = true;
        /*if (PlayerPrefs.GetInt("isSave") == 1)
        {
            timeRemaining = PlayerPrefs.GetFloat("currTime");
        }
        else
        {
            timeRemaining = PlayerPrefs.GetFloat("MaxTIME");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //int currLives = PlayerPrefs.GetInt("currLivesPref");
        scoreText.text = playerScore.ToString();
        livesText.text = playerLives.ToString();
        nameDisplay.text = "PLAYER: " + playerName;

        if (playerLives == 0)
        {
            //Time.timeScale = 0f;

        }

        DisplayTime(timeRemaining);
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                //Debug.Log(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                //PlayerPrefs.SetInt("isTimeRemaining", 1);
                timerIsRunning = false;
                EndGame();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void saveCurrTime()
    {
        PlayerPrefs.SetFloat("currTIME", timeRemaining);
    }

    public void decreaseScore()
    {
        playerScore--;
    }

    public void increaseScore()
    {
        playerScore++;
    }

    public void decreaseLives()
    {
        playerLives--;
    }

    public void increaseLives()
    {
        playerLives++;
    }


    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.score = playerScore;
        save.lives = playerLives;
        save.name = playerName;
        save.time = timeRemaining;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.typegame");
        bf.Serialize(file, save);
        file.Close();

        playerName = PlayerPrefs.GetString("playerNAME");
        save.name = playerName;
        save.time = timeRemaining;

        playerScore = save.score;
        scoreText.text = playerScore.ToString();

        //PlayerPrefs.SetFloat("currTIME", timeRemaining);
        PlayerPrefs.SetInt("currLivesPref", playerLives);
        Debug.Log("Game Saved! Score saved is = " + save.score + " " + save.name);
        Debug.Log("Time is: " + save.time);
    }

    public void LoadGame()
    {
        //1
        if (File.Exists(Application.persistentDataPath + "/gamesave.typegame"))
        {

            //2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.typegame", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            playerName = save.name;
            playerLives = save.lives;
            timeRemaining = save.time;
            playerScore = save.score;
            //PlayerPrefs.SetString("playerNAME", playerName);


            PlayerPrefs.SetInt("loadedScore", playerScore);
            PlayerPrefs.SetInt("currLivesPref", playerLives);
            PlayerPrefs.SetFloat("currTIME", timeRemaining);
            
            Debug.Log("Game Loaded Score is = " + save.score + " " + save.name);
            Debug.Log("Game Loaded Lives is = " + save.lives);
            Debug.Log("Game Loaded Time is = " + save.time);
            Restart();

        }
        else
        {
            Debug.Log("No Game Saved!");
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        timeRemaining = PlayerPrefs.GetFloat("selectedTIME");
        PlayerPrefs.SetFloat("currTIME", timeRemaining);
        int currLives = PlayerPrefs.GetInt("livesSelected");
        PlayerPrefs.SetInt("currLivesPref", currLives);
        PlayerPrefs.SetInt("loadedScore", 0);
        scoreText.text = playerScore.ToString();
        Restart();

    }

    public void Restart()
    {
        PauseMenuUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("loadedScore", playerScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
