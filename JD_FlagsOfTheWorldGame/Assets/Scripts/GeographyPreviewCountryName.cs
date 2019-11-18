using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeographyPreviewCountryName : MonoBehaviour
{
    public string countryName;
    public Text previewText;
    // Start is called before the first frame update
    void Start()
    {
        previewText = GameObject.Find("Text_Country").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
