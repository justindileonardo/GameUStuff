using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    public GameObject currentGeography, temporaryGeography;
    public Animator animFlagsBox;
    public bool mouseIsHighEnough;
    Vector3 mousePosCameraRelative;
    Vector2 mousePos2DCameraRelative;
    public AudioSource selectSound, deselectSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosCameraRelative = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 mousePos2DCameraRelative = new Vector2(mousePosCameraRelative.x, mousePosCameraRelative.y);
        //print(mousePosCameraRelative.y);
        if(mousePosCameraRelative.y > 0.16f)
        {
            mouseIsHighEnough = true;
        } 
        else
        {
            mouseIsHighEnough = false;
        }
        SelectGeography();
    }

    public void SelectGeography()
    {
       
        //click left click
        if(Input.GetMouseButtonDown(0))
        {
            //raycast to click on an object
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(mouseIsHighEnough == true)
            {
                //if you hit something (not nothing)
                if (hit.collider != null)
                {
                    //if it's not the first click (if you have something selected already)
                    if (currentGeography != null)
                    {
                        //set a temporary selection as the geography you clicked last time
                        temporaryGeography = currentGeography;
                        //then set the new clicked geography as the currentGeography and change color to yellow
                        currentGeography = hit.collider.gameObject;
                        currentGeography.GetComponent<SpriteRenderer>().color = Color.yellow;
                        //if the geographies you clicked are different (different positions), make the temporary geography back to white
                        if (currentGeography.transform.position != temporaryGeography.transform.position)
                        {
                            temporaryGeography.GetComponent<SpriteRenderer>().color = Color.white;
                            selectSound.Play();
                        }
                        //deselect selection if you click the same geography you already have selected
                        if (currentGeography.transform.position == temporaryGeography.transform.position)
                        {
                            deselectSound.Play();
                            Deselect();
                        }
                    }
                    //if first click (nothing selected)
                    else
                    {
                        //just make the selection yellow
                        currentGeography = hit.collider.gameObject;
                        currentGeography.GetComponent<SpriteRenderer>().color = Color.yellow;
                        animFlagsBox.SetBool("HasASelection", true);
                        selectSound.Play();
                    }
                }
            }
            else if (currentGeography == null)
            {
                //if you hit something (not nothing)
                if (hit.collider != null)
                {
                    //if it's not the first click (if you have something selected already)
                    if (currentGeography != null)
                    {
                        //set a temporary selection as the geography you clicked last time
                        temporaryGeography = currentGeography;
                        //then set the new clicked geography as the currentGeography and change color to yellow
                        currentGeography = hit.collider.gameObject;
                        currentGeography.GetComponent<SpriteRenderer>().color = Color.yellow;
                        //if the geographies you clicked are different (different positions), make the temporary geography back to white
                        if (currentGeography.transform.position != temporaryGeography.transform.position)
                        {
                            temporaryGeography.GetComponent<SpriteRenderer>().color = Color.white;
                            selectSound.Play();
                        }
                        //deselect selection if you click the same geography you already have selected
                        if (currentGeography.transform.position == temporaryGeography.transform.position)
                        {
                            deselectSound.Play();
                            Deselect();
                        }
                    }
                    //if first click (nothing selected)
                    else
                    {
                        //just make the selection yellow
                        currentGeography = hit.collider.gameObject;
                        currentGeography.GetComponent<SpriteRenderer>().color = Color.yellow;
                        animFlagsBox.SetBool("HasASelection", true);
                        selectSound.Play();
                    }
                }
            }







        }

    }

    public void Deselect()
    {
        if(currentGeography != null)
        {
            currentGeography.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(temporaryGeography != null)
        {
            temporaryGeography.GetComponent<SpriteRenderer>().color = Color.white;
        }
        currentGeography = null;
        temporaryGeography = null;
        animFlagsBox.SetBool("HasASelection", false);
    }

}
