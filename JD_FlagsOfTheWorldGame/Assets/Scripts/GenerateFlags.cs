using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GenerateFlags : MonoBehaviour
{
    public List<Button> flagButtonsStart;
    public List<Button> flagButtonsRandomized;
    public int amountOfFlags;
    int minX = -360;
    int flagNumber;
    // Start is called before the first frame update
    void Start()
    {
        amountOfFlags = flagButtonsStart.Count;
        Invoke("SetRandomFlagPositions", 1);
        Invoke("GoBackPage", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(flagButtonsStart.Count > 0)
        {
            int randomFlag = Random.Range(0, flagButtonsStart.Count);
            flagButtonsRandomized.Add(flagButtonsStart[randomFlag]);
            flagButtonsStart.RemoveAt(randomFlag);
        }
        
    }
    void SetRandomFlagPositions()
    {
        for(int i = 0; i < amountOfFlags; i++)
        {
            
            //if flags 0-12
            if (i >= 0 && i <= 13)
            {
                //if even, positive y
                if(i % 2 == 0)
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i * 30), 17.5f, 0);
                }
                //if odd, negative y
                else
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i * 30-30), -17.5f, 0);
                }
                
            }
            //if flags 14-25
            else if(i >= 14 && i <= 25)
            {
                //if even, positive y
                if (i % 2 == 0)
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i * 30), 17.5f, 0);
                }
                //if odd, negative y
                else
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i * 30-30), -17.5f, 0);
                }
            }
            //flag 12 goes at specific position
            if(SceneManager.GetActiveScene().name == "NorthAmerica")
            {
                flagButtonsRandomized[12].GetComponent<RectTransform>().anchoredPosition = new Vector3(360, -17.5f, 0);
            }
            else if(SceneManager.GetActiveScene().name == "SouthAmerica")
            {
                flagButtonsRandomized[12].GetComponent<RectTransform>().anchoredPosition = new Vector3(60, 17.5f, 0);
            }
            else if (SceneManager.GetActiveScene().name == "Europe")
            {
                flagButtonsRandomized[12].GetComponent<RectTransform>().anchoredPosition = new Vector3(240, 17.5f, 0);
                flagButtonsRandomized[13].GetComponent<RectTransform>().anchoredPosition = new Vector3(180, 17.5f, 0);
                //flagButtonsRandomized[25].GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 17.5f, 0);
            }


            //2ND PAGE
            //if flags 0-12
            if (i >= 26 && i <= 39)
            {
                //if even, positive y
                if (i % 2 == 0)
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + ((i-26) * 30), 17.5f, 0);
                }
                //if odd, negative y
                else
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + ((i-26) * 30 - 30), -17.5f, 0);
                }

            }
            //if flags 14-25
            else if (i >= 40 && i <= 51)
            {
                //if even, positive y
                if (i % 2 == 0)
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + ((i-26) * 30), 17.5f, 0);
                }
                //if odd, negative y
                else
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + ((i-26) * 30 - 30), -17.5f, 0);
                }
            }

            if(i >25)
            {
                //flag 12 goes at specific position
                if(SceneManager.GetActiveScene().name == "NorthAmerica")
                {
                    flagButtonsRandomized[38].GetComponent<RectTransform>().anchoredPosition = new Vector3(360, -17.5f, 0);
                }
                else if (SceneManager.GetActiveScene().name == "Europe")
                {
                    flagButtonsRandomized[38].GetComponent<RectTransform>().anchoredPosition = new Vector3(240, -17.5f, 0);
                    flagButtonsRandomized[39].GetComponent<RectTransform>().anchoredPosition = new Vector3(180, -17.5f, 0);
                }

            }
            
        }
        
    }

    public void GoUpPage()
    {
        if(SceneManager.GetActiveScene().name == "Europe")
        {
            for (int i = 0; i < flagButtonsRandomized.Count; i++)
            {
                
                if (i <= 24)
                {
                    flagButtonsRandomized[i].enabled = false;
                    flagButtonsRandomized[i].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[18].enabled = false;
                    flagButtonsRandomized[18].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[20].enabled = false;
                    flagButtonsRandomized[20].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[25].enabled = false;
                    flagButtonsRandomized[25].GetComponent<Image>().enabled = false;
                }
                else if (i >= 25 && i <= 49)
                {
                    flagButtonsRandomized[i].enabled = true;
                    flagButtonsRandomized[i].GetComponent<Image>().enabled = true;
                    flagButtonsRandomized[18].enabled = true;
                    flagButtonsRandomized[18].GetComponent<Image>().enabled = true;
                    flagButtonsRandomized[20].enabled = true;
                    flagButtonsRandomized[20].GetComponent<Image>().enabled = true;
                    flagButtonsRandomized[25].enabled = false;
                    flagButtonsRandomized[25].GetComponent<Image>().enabled = false;
                }
            }
        }
        
    }
    public void GoBackPage()
    {
        if (SceneManager.GetActiveScene().name == "Europe")
        {
            for (int i = 0; i < flagButtonsRandomized.Count; i++)
            {
                if (i <= 24)
                {
                    flagButtonsRandomized[i].enabled = true;
                    flagButtonsRandomized[i].GetComponent<Image>().enabled = true;
                    flagButtonsRandomized[18].enabled = true;
                    flagButtonsRandomized[18].GetComponent<Image>().enabled = true;
                    flagButtonsRandomized[20].enabled = false;
                    flagButtonsRandomized[20].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[25].enabled = true;
                    flagButtonsRandomized[25].GetComponent<Image>().enabled = true;
                }
                else if (i >= 25 && i <= 49)
                {
                    flagButtonsRandomized[i].enabled = false;
                    flagButtonsRandomized[i].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[18].enabled = false;
                    flagButtonsRandomized[18].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[20].enabled = false;
                    flagButtonsRandomized[20].GetComponent<Image>().enabled = false;
                    flagButtonsRandomized[25].enabled = true;
                    flagButtonsRandomized[25].GetComponent<Image>().enabled = true;
                }
            }
        }
            
    }

    
}
