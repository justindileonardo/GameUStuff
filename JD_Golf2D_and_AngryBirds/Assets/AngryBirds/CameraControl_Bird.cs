using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_Bird : MonoBehaviour
{

    public GameObject target;
    public float lerpSpeed, moveSpeed, camMinX, camMaxX;
    // Start is called before the first frame update
    void Start()
    {

    }
    void FixedUpdate()
    {
        //lerps camera to ball when ball is moving
        if (target.GetComponent<BirdControl>().launched == true && target.GetComponent<BirdControl>().death == false)
        {
            float interpolation = lerpSpeed * Time.deltaTime;
            Vector3 position = this.transform.position;
            position.x = Mathf.Lerp(transform.position.x, target.transform.position.x, interpolation);
            transform.position = position;
        }
    }
    // Update is called once per frame
    void Update()
    {

        //before launching
        if (target.GetComponent<BirdControl>().launched == false && target.GetComponent<BirdControl>().aboutToLaunch == false)
        {
            //right arrow moves camera right to certain range
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < camMaxX)
                    transform.position = new Vector2(transform.position.x + moveSpeed, transform.position.y);
            }
            //left arrow moves camera left to certain range
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > camMinX)
                {
                    transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
                }
            }
        }

        

    }
}
