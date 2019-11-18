using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static bool NA_complete, SA_complete, EU_complete, AS_complete, AF_complete, AU_complete;
    public static int difficulty;
    public SpriteRenderer[] NA_Flags, SA_Flags;
    public Animator UI_Holder_Animator, UI_TitleHolder_Animator, UI_Holder2_Animator, UI_Controls_Animator;
    public bool UI_enabled, UI_Controls_enabled;
    public Button easy, normal, hard, expert, NA, SA, EU, AS, AF, AU;
    public static bool flagPreviews, geographyPreviews;
    public AudioSource menuSound;
    // Start is called before the first frame update
    void Start()
    {

        flagPreviews = false;
        geographyPreviews = true;

        UI_enabled = true;
        UI_Controls_enabled = false;
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SetDifficultyFirstTime(difficulty);
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (NA_complete == true)
            {
                EnableNorthAmericaFlags();
            }
            if (SA_complete == true)
            {
                EnableSouthAmericaFlags();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void CompleteLevel()
    {
        if(SceneManager.GetActiveScene().name == "NorthAmerica")
        {
            NA_complete = true;
        }
        else if(SceneManager.GetActiveScene().name == "SouthAmerica")
        {
            SA_complete = true;
        }
        else if(SceneManager.GetActiveScene().name == "Europe")
        {
            EU_complete = true;
        }
        else if(SceneManager.GetActiveScene().name == "Asia")
        {
            AS_complete = true;
        }
        else if(SceneManager.GetActiveScene().name == "Africa")
        {
            AF_complete = true;
        }
        else if(SceneManager.GetActiveScene().name == "Australia")
        {
            AU_complete = true;
        }
        SceneManager.LoadScene("MainMenu");
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SetDifficulty(int levelDifficulty)
    {
        menuSound.Play();
        if(levelDifficulty == 0)
        {
            difficulty = 0;     //easy
            easy.GetComponent<Image>().color = Color.green;
            normal.GetComponent<Image>().color = Color.white;
            hard.GetComponent<Image>().color = Color.white;
            expert.GetComponent<Image>().color = Color.white;
        }
        else if(levelDifficulty == 1)
        {
            difficulty = 1;     //normal
            easy.GetComponent<Image>().color = Color.white;
            normal.GetComponent<Image>().color = Color.green;
            hard.GetComponent<Image>().color = Color.white;
            expert.GetComponent<Image>().color = Color.white;
        }
        else if(levelDifficulty == 2)
        {
            difficulty = 2;     //hard
            easy.GetComponent<Image>().color = Color.white;
            normal.GetComponent<Image>().color = Color.white;
            hard.GetComponent<Image>().color = Color.green;
            expert.GetComponent<Image>().color = Color.white;
        }
        else if (levelDifficulty == 3)
        {
            difficulty = 3;     //expert
            easy.GetComponent<Image>().color = Color.white;
            normal.GetComponent<Image>().color = Color.white;
            hard.GetComponent<Image>().color = Color.white;
            expert.GetComponent<Image>().color = Color.green;
        }
    }
    public void SetDifficultyFirstTime(int levelDifficulty)
    {
        //menuSound.Play();
        if (levelDifficulty == 0)
        {
            difficulty = 0;     //easy
            easy.GetComponent<Image>().color = Color.green;
            normal.GetComponent<Image>().color = Color.white;
            hard.GetComponent<Image>().color = Color.white;
            expert.GetComponent<Image>().color = Color.white;
        }
        else if (levelDifficulty == 1)
        {
            difficulty = 1;     //normal
            easy.GetComponent<Image>().color = Color.white;
            normal.GetComponent<Image>().color = Color.green;
            hard.GetComponent<Image>().color = Color.white;
            expert.GetComponent<Image>().color = Color.white;
        }
        else if (levelDifficulty == 2)
        {
            difficulty = 2;     //hard
            easy.GetComponent<Image>().color = Color.white;
            normal.GetComponent<Image>().color = Color.white;
            hard.GetComponent<Image>().color = Color.green;
            expert.GetComponent<Image>().color = Color.white;
        }
        else if (levelDifficulty == 3)
        {
            difficulty = 3;     //expert
            easy.GetComponent<Image>().color = Color.white;
            normal.GetComponent<Image>().color = Color.white;
            hard.GetComponent<Image>().color = Color.white;
            expert.GetComponent<Image>().color = Color.green;
        }
    }

    public void UI_Holder_Toggle()
    {
        if(UI_enabled == true)
        {
            UI_enabled = false;
            UI_Holder_Animator.SetBool("UI_Holder_Enabled", false);
            UI_TitleHolder_Animator.SetBool("UI_TitleHolder_Enabled", false);
            UI_Holder2_Animator.SetBool("UI_Holder2_Enabled", false);
        }
        else if(UI_enabled == false)
        {
            UI_enabled = true;
            UI_Holder_Animator.SetBool("UI_Holder_Enabled", true);
            UI_TitleHolder_Animator.SetBool("UI_TitleHolder_Enabled", true);
            UI_Holder2_Animator.SetBool("UI_Holder2_Enabled", true);
        }
    }

    public void UI_ToggleControls()
    {
        if(UI_Controls_enabled == true)
        {
            UI_Controls_enabled = false;
            UI_Controls_Animator.SetBool("UI_Controls_Enabled", false);
        }
        else if(UI_Controls_enabled == false)
        {
            UI_Controls_enabled = true;
            UI_Controls_Animator.SetBool("UI_Controls_Enabled", true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EnableNorthAmericaFlags()
    {
        NA.GetComponent<Image>().color = Color.green;
        for(int i = 0; i < NA_Flags.Length; i++)
        {
            NA_Flags[i].enabled = true;
        }
    }
    public void EnableSouthAmericaFlags()
    {
        SA.GetComponent<Image>().color = Color.green;
        for (int i = 0; i < SA_Flags.Length; i++)
        {
            SA_Flags[i].enabled = true;
        }
    }


}
