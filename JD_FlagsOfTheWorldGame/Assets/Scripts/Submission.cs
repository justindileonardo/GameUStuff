using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Submission : MonoBehaviour
{
    private Selection selectionScript;
    public int numberOfCountries;
    public int lives;
    public GameObject[] lives_hearts;
    bool correct;
    int wrongSubmissions, correctSubmissions;
    public Text text_countryNamePopup, text_wrongSubmissions, text_levelCompleted, text_levelFailed;
    public bool fadingText;
    public Color flashRed;
    public AudioSource successSound, wrongSound, completeLevelSound, failLevelSound;
    public GameObject CompleteLevelButton, RestartLevelButton, MainMenuFailButton;
    public PolygonCollider2D[] countriesColliders;
    // Start is called before the first frame update
    void Start()
    {
        selectionScript = GameObject.Find("SelectionScript").GetComponent<Selection>();
        if(LevelManager.difficulty == 0)
        {
            lives = 10;
        }
        else if(LevelManager.difficulty == 1)
        {
            lives = 5;
        }
        else if(LevelManager.difficulty == 2)
        {
            lives = 5;
        }
        else if (LevelManager.difficulty == 3)
        {
            lives = 1;
        }
        SetLives();
        //testing
        //correctSubmissions = 23;
    }

    // Update is called once per frame
    void Update()
    {
        //fade the country name text
        if(text_countryNamePopup.color.a > 0 && fadingText == true)
        {
            text_countryNamePopup.color = new Color(0, 0, 0, text_countryNamePopup.color.a - 0.025f);
        }
        
        //update wrong text
        text_wrongSubmissions.text = "Wrong: " + wrongSubmissions;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
    //when you click the button of a flag
    public void MakeSubmission(GameObject correctCountry)
    {
        //if you select right country's flag...
        if(selectionScript.currentGeography == correctCountry)
        {
            //print("correct!");
            correct = true;
            correctSubmissions ++;
            if(correctSubmissions >= numberOfCountries)
            {
                StartCoroutine(FinishLevel());
            }
            successSound.Play();
            selectionScript.currentGeography.SetActive(false);
            selectionScript.Deselect();
        }
        //if you select wrong country's flag...
        else
        {
            //print("WRONG!!!");
            wrongSubmissions ++;
            lives--;
            SetLives();
            correct = false;
            wrongSound.Play();
            StartCoroutine(FlashRed());
            if(lives <= 0)
            {
                StartCoroutine(FailLevel());
            }
            //selectionScript.Deselect();
        }
    }

    //flashing the country red a few times when choosing the wrong flag
    IEnumerator FlashRed()
    {
        selectionScript.currentGeography.GetComponent<SpriteRenderer>().color = flashRed;
        yield return new WaitForSeconds(0.1f);
        selectionScript.currentGeography.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        selectionScript.currentGeography.GetComponent<SpriteRenderer>().color = flashRed;
        yield return new WaitForSeconds(0.1f);
        selectionScript.currentGeography.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.025f);
        selectionScript.Deselect();
    }


    //selecting the correct flag, turning the flag on
    public void TurnColorOn(SpriteRenderer countrySpriteColor)
    {
        //if you select right country's flag
        if(correct == true)
        {
            //turn flag color on geography
            countrySpriteColor.enabled = true;
            //disable the flag button
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        }
        
    }

    //Changing the text to be the correct country's name
    public void TextCountryName(string countryName)
    {
        if(correct == true)
        {
            fadingText = false;
            text_countryNamePopup.enabled = true;
            text_countryNamePopup.text = countryName;
            text_countryNamePopup.color = new Color(0, 0, 0, 1);
            StartCoroutine(FadeText());
            
        }
        
    }

    //fading the country's name text 
    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(1.0f);
        fadingText = true;
    }
    
    //Finishing the level, enabling things after a delay
    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(2.0f);
        CompleteLevelButton.SetActive(true);
        completeLevelSound.Play();
        text_levelCompleted.enabled = true;
    }

    IEnumerator FailLevel()
    {
        for(int i = 0; i<countriesColliders.Length; i++)
        {
            countriesColliders[i].enabled = false;
        }
        yield return new WaitForSeconds(1.0f);
        failLevelSound.Play();
        text_levelFailed.enabled = true;
        RestartLevelButton.SetActive(true);
        MainMenuFailButton.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void FailMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //adjusting the lives images
    public void SetLives()
    {
        if(lives == 0)
        {
            lives_hearts[0].SetActive(false);
            lives_hearts[1].SetActive(false);
            lives_hearts[2].SetActive(false);
            lives_hearts[3].SetActive(false);
            lives_hearts[4].SetActive(false);
            lives_hearts[5].SetActive(false);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if(lives == 1)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(false);
            lives_hearts[2].SetActive(false);
            lives_hearts[3].SetActive(false);
            lives_hearts[4].SetActive(false);
            lives_hearts[5].SetActive(false);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if(lives == 2)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(false);
            lives_hearts[3].SetActive(false);
            lives_hearts[4].SetActive(false);
            lives_hearts[5].SetActive(false);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if(lives == 3)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(false);
            lives_hearts[4].SetActive(false);
            lives_hearts[5].SetActive(false);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 4)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(false);
            lives_hearts[5].SetActive(false);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 5)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(true);
            lives_hearts[5].SetActive(false);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 6)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(true);
            lives_hearts[5].SetActive(true);
            lives_hearts[6].SetActive(false);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 7)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(true);
            lives_hearts[5].SetActive(true);
            lives_hearts[6].SetActive(true);
            lives_hearts[7].SetActive(false);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 8)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(true);
            lives_hearts[5].SetActive(true);
            lives_hearts[6].SetActive(true);
            lives_hearts[7].SetActive(true);
            lives_hearts[8].SetActive(false);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 9)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(true);
            lives_hearts[5].SetActive(true);
            lives_hearts[6].SetActive(true);
            lives_hearts[7].SetActive(true);
            lives_hearts[8].SetActive(true);
            lives_hearts[9].SetActive(false);
        }
        if (lives == 10)
        {
            lives_hearts[0].SetActive(true);
            lives_hearts[1].SetActive(true);
            lives_hearts[2].SetActive(true);
            lives_hearts[3].SetActive(true);
            lives_hearts[4].SetActive(true);
            lives_hearts[5].SetActive(true);
            lives_hearts[6].SetActive(true);
            lives_hearts[7].SetActive(true);
            lives_hearts[8].SetActive(true);
            lives_hearts[9].SetActive(true);
        }
    }
    
}
