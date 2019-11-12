using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submission : MonoBehaviour
{
    private Selection selectionScript;
    bool correct;
    // Start is called before the first frame update
    void Start()
    {
        selectionScript = GameObject.Find("SelectionScript").GetComponent<Selection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeSubmission(GameObject correctCountry)
    {
        if(selectionScript.currentGeography == correctCountry)
        {
            print("correct!");
            correct = true;
            selectionScript.Deselect();
        }
        else
        {
            print("WRONG!!!");
            correct = false;
            selectionScript.Deselect();
        }
    }
    public void TurnColorOn(SpriteRenderer countrySpriteColor)
    {
        if(correct == true)
        {
            countrySpriteColor.enabled = true;
        }        
    }
}
