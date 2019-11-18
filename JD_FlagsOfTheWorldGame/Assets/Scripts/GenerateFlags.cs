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
    // Start is called before the first frame update
    void Start()
    {
        amountOfFlags = flagButtonsStart.Count;
        Invoke("SetRandomFlagPositions", 1);
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
            //flag 12 goes at bottom right
            if(SceneManager.GetActiveScene().name == "NorthAmerica")
            {
                flagButtonsRandomized[12].GetComponent<RectTransform>().anchoredPosition = new Vector3(360, -17.5f, 0);
            }
            else if(SceneManager.GetActiveScene().name == "SouthAmerica")
            {
                flagButtonsRandomized[12].GetComponent<RectTransform>().anchoredPosition = new Vector3(60, 17.5f, 0);
            }
            

            //2ND PAGE
            //if flags 0-12
            if (i >= 26 && i <= 39)
            {
                //if even, positive y
                if (i % 2 == 0)
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i/2 * 30), 17.5f, 0);
                }
                //if odd, negative y
                else
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i/2 * 30 - 30), -17.5f, 0);
                }

            }
            //if flags 14-25
            else if (i >= 40 && i <= 51)
            {
                //if even, positive y
                if (i % 2 == 0)
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i/2 * 30), 17.5f, 0);
                }
                //if odd, negative y
                else
                {
                    flagButtonsRandomized[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(minX + (i/2 * 30 - 30), -17.5f, 0);
                }
            }

            if(i >25)
            {
                //flag 12 goes at bottom right
                if(SceneManager.GetActiveScene().name == "NorthAmerica")
                {
                    flagButtonsRandomized[38].GetComponent<RectTransform>().anchoredPosition = new Vector3(360, -17.5f, 0);
                }
                
            }
            
        }
        
    }
}
