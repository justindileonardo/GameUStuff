using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    Rigidbody rb;
    public BirdGameValues birdGameValuesScript;
    public int otherPigScore;
    public Vector3 otherPigVector3;
    void Awake()
    {
        birdGameValuesScript = GameObject.Find("BirdGameValues").GetComponent<BirdGameValues>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Pig")
        {
            if (rb.velocity.x > 1 || rb.velocity.x < -1 || rb.velocity.y > 1 || rb.velocity.y < -1)
            {
                otherPigScore = other.gameObject.GetComponent<PigDestroy>().pigScore;
                otherPigVector3 = other.gameObject.GetComponent<PigDestroy>().transform.position;
                birdGameValuesScript.AddScoreFunction(otherPigVector3, otherPigScore);
                Destroy(other.gameObject);
            }
        }
    }
}
