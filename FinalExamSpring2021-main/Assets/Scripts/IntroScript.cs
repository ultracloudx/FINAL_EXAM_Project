using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;

public class IntroScript : MonoBehaviour
{
    public string userNameInput;
    public GameObject warningText;
    public GameObject inputField;
    //public Button playButton;
    public Dropdown dropdown;
    public Slider slider;

    public float timeValue;

    // Start is called before the first frame update
    void Awake()
    {
        setLivesDropdown(dropdown.value);
        Debug.Log("dropdown value is " + dropdown.value);
        timeValue = 30f;
        Debug.Log("slider value is " + timeValue);
        //Debug.Log("slider value is " + slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreName()
    {
        userNameInput = inputField.GetComponent<Text>().text;
        Debug.Log(userNameInput);
    }

    public void PlayGame()
    {
        if (userNameInput == "")
        {
            Debug.Log("No Name!");
        }
        else
        {
            if (File.Exists(Application.persistentDataPath + "/gamesave.typegame"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.typegame", FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                Debug.Log("No Game Saved!");
            }



            PlayerPrefs.SetInt("loadedScore", 0);
            PlayerPrefs.SetString("playerNAME", userNameInput);
            PlayerPrefs.SetFloat("selectedTIME", timeValue);
            PlayerPrefs.SetFloat("currTIME", timeValue);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void setLivesDropdown(int livesIndex)
    {
        int i = 0;

        switch (livesIndex)
        {
            case 0:
                i = 1;
                PlayerPrefs.SetInt("difficultySelected", i);
                PlayerPrefs.SetInt("livesSelected", 1);
                PlayerPrefs.SetInt("currLivesPref", 1);
                Debug.Log("LIVES " + i);
                break;
            case 1:
                i = 2;
                PlayerPrefs.SetInt("difficultySelected", i);
                PlayerPrefs.SetInt("livesSelected", 2);
                PlayerPrefs.SetInt("currLivesPref", 2);
                Debug.Log("LIVES " + i);
                break;
            case 2:
                i = 3;
                PlayerPrefs.SetInt("difficultySelected", i);
                PlayerPrefs.SetInt("livesSelected", 3);
                PlayerPrefs.SetInt("currLivesPref", 3);
                Debug.Log("LIVES " + i);
                break;
            default:
                i = 0;
                PlayerPrefs.SetInt("difficultySelected", i);
                PlayerPrefs.SetInt("livesSelected", 1);
                PlayerPrefs.SetInt("currLivesPref", 1);
                Debug.Log("LIVES " + i);
                break;
        }
    }

    public void setTime()
    {
        //default is currently 30f. Slider sets from 30 - 90
        //pinSliderText.text = Slider.value.ToString("");
        timeValue = slider.value;
        Debug.Log(timeValue);
        
    }
}
