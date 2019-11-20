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
    public Color flashRed, flagUsed;
    public AudioSource successSound, wrongSound, completeLevelSound, failLevelSound;
    public GameObject CompleteLevelButton, RestartLevelButton, MainMenuFailButton;
    public PolygonCollider2D[] countriesColliders;

    // Start is called before the first frame update
    void Start()
    {
        selectionScript = GameObject.Find("SelectionScript").GetComponent<Selection>();

        LivesPerLevel();
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
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = flagUsed;
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

    public void LivesPerLevel()
    {
        if (SceneManager.GetActiveScene().name == "NorthAmerica")
        {
            if (LevelManager.difficulty == 0)
            {
                lives = 15;
            }
            else if (LevelManager.difficulty == 1)
            {
                lives = 8;
            }
            else if (LevelManager.difficulty == 2)
            {
                lives = 8;
            }
            else if (LevelManager.difficulty == 3)
            {
                lives = 1;
            }
        }
        else if (SceneManager.GetActiveScene().name == "SouthAmerica")
        {
            if (LevelManager.difficulty == 0)
            {
                lives = 7;
            }
            else if (LevelManager.difficulty == 1)
            {
                lives = 4;
            }
            else if (LevelManager.difficulty == 2)
            {
                lives = 4;
            }
            else if (LevelManager.difficulty == 3)
            {
                lives = 1;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Europe")
        {
            if (LevelManager.difficulty == 0)
            {
                lives = 20;
            }
            else if (LevelManager.difficulty == 1)
            {
                lives = 10;
            }
            else if (LevelManager.difficulty == 2)
            {
                lives = 10;
            }
            else if (LevelManager.difficulty == 3)
            {
                lives = 1;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Australia")
        {
            if (LevelManager.difficulty == 0)
            {
                lives = 2;
            }
            else if (LevelManager.difficulty == 1)
            {
                lives = 2;
            }
            else if (LevelManager.difficulty == 2)
            {
                lives = 1;
            }
            else if (LevelManager.difficulty == 3)
            {
                lives = 1;
            }
        }

    }
    
    //adjusting the lives images
    public void SetLives()
    {
        if (lives == 0)
         {
             lives_hearts[0].SetActive(false);
         }
         for (int i = 0; i <= lives_hearts.Length; i++)
         {
             if(lives == i)
             {
                 if(lives != 0)
                 {
                     lives_hearts[i - 1].SetActive(true);
                 }
             }
             else if(lives < i)
             {
                 lives_hearts[i-1].SetActive(false);
             }
         }

    



    /*if(lives == 0)
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
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
        lives_hearts[10].SetActive(false);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 11)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(false);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 12)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(false);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 13)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(false);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 14)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(false);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 15)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(false);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 16)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(false);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 17)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(false);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 18)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(false);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 19)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(false);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 20)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(false);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 21)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(false);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 22)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(false);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 23)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(false);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 24)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(false);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 25)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(true);
        lives_hearts[25].SetActive(false);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 26)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(true);
        lives_hearts[25].SetActive(true);
        lives_hearts[26].SetActive(false);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 27)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(true);
        lives_hearts[25].SetActive(true);
        lives_hearts[26].SetActive(true);
        lives_hearts[27].SetActive(false);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 28)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(true);
        lives_hearts[25].SetActive(true);
        lives_hearts[26].SetActive(true);
        lives_hearts[27].SetActive(true);
        lives_hearts[28].SetActive(false);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 29)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(true);
        lives_hearts[25].SetActive(true);
        lives_hearts[26].SetActive(true);
        lives_hearts[27].SetActive(true);
        lives_hearts[28].SetActive(true);
        lives_hearts[29].SetActive(false);
    }
    if (lives == 30)
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
        lives_hearts[10].SetActive(true);
        lives_hearts[11].SetActive(true);
        lives_hearts[12].SetActive(true);
        lives_hearts[13].SetActive(true);
        lives_hearts[14].SetActive(true);
        lives_hearts[15].SetActive(true);
        lives_hearts[16].SetActive(true);
        lives_hearts[17].SetActive(true);
        lives_hearts[18].SetActive(true);
        lives_hearts[19].SetActive(true);
        lives_hearts[20].SetActive(true);
        lives_hearts[21].SetActive(true);
        lives_hearts[22].SetActive(true);
        lives_hearts[23].SetActive(true);
        lives_hearts[24].SetActive(true);
        lives_hearts[25].SetActive(true);
        lives_hearts[26].SetActive(true);
        lives_hearts[27].SetActive(true);
        lives_hearts[28].SetActive(true);
        lives_hearts[29].SetActive(true);
    }
    */
}
    
}
