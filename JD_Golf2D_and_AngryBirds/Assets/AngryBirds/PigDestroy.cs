using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDestroy : MonoBehaviour
{
    public Rigidbody2D rb;
    public int pigScore;
    public BirdGameValues birdGameValuesScript;

    void Awake()
    {
        birdGameValuesScript = GameObject.Find("BirdGameValues").GetComponent<BirdGameValues>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(rb.velocity.x > 1 || rb.velocity.x < -1 || rb.velocity.y > 1 || rb.velocity.y < -1)
        {
            birdGameValuesScript.AddScoreFunction(transform.position, pigScore);
            Destroy(gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bird")
        {
            birdGameValuesScript.AddScoreFunction(transform.position, pigScore);
            Destroy(gameObject);
        }
    }
}
