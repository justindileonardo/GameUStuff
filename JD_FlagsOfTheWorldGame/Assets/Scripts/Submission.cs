using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submission : MonoBehaviour
{
    private Selection selectionScript;
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
            selectionScript.Deselect();
        }
        else
        {
            print("WRONG!!!");
            selectionScript.Deselect();
        }
    }
}
