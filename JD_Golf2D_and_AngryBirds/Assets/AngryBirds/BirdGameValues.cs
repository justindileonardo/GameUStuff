using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BirdGameValues : MonoBehaviour
{

    public int score;
    public GameObject textScoreGained;
    public Text textScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "" + score;
    }

    public void AddScoreFunction(Vector3 thePigPosition, int theNumberOfScore)
    {
        StartCoroutine(AddScore(thePigPosition, theNumberOfScore));
    }

    public IEnumerator AddScore(Vector3 pigPosition, int numberOfScore)
    {
        score += numberOfScore;
        GameObject text = Instantiate(textScoreGained, pigPosition, Quaternion.identity);
        text.GetComponent<TextMesh>().text = "" + numberOfScore + "";
        text.GetComponent<Renderer>().sortingLayerName = "PopupText";
        yield return new WaitForSeconds(2.0f);
        Destroy(text);
    }
}
