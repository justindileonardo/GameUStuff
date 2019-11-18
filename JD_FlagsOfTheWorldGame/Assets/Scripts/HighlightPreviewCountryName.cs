using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HighlightPreviewCountryName : MonoBehaviour, IPointerEnterHandler
{
    public string countryName;
    public Text previewText;
    void Start()
    {
        previewText = GameObject.Find("Text_Country").GetComponent<Text>();
        previewText.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(LevelManager.difficulty == 0 || LevelManager.difficulty == 1)
        {
            previewText.enabled = true;
            previewText.text = countryName;
        } else
        {
           
        }
        
    }

}
