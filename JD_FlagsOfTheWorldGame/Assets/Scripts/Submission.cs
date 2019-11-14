using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Submission : MonoBehaviour
{
    private Selection selectionScript;
    bool correct;
    public Text text_countryNamePopup;
    // Start is called before the first frame update
    void Start()
    {
        selectionScript = GameObject.Find("SelectionScript").GetComponent<Selection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(text_countryNamePopup.color.a > 0)
        {
            text_countryNamePopup.color = new Color(0, 0, 0, text_countryNamePopup.color.a - 0.005f);
        }
        
        
    }
    //when you click the button of a flag
    public void MakeSubmission(GameObject correctCountry)
    {
        //if you select right country's flag...
        if(selectionScript.currentGeography == correctCountry)
        {
            print("correct!");
            correct = true;
            selectionScript.Deselect();
        }
        //if you select wrong country's flag...
        else
        {
            print("WRONG!!!");
            correct = false;
            selectionScript.Deselect();
        }
    }
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

    public void TextCountryName(string countryName)
    {
        if(correct == true)
        {
            text_countryNamePopup.enabled = true;
            text_countryNamePopup.text = countryName;
            text_countryNamePopup.color = new Color(0, 0, 0, 1);
        }
        
    }

    
}
