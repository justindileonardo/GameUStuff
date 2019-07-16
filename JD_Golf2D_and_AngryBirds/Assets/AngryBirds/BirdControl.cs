using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdControl : MonoBehaviour
{

    public float force, forceMultiplier, rotationSpeed;
    public Rigidbody2D rb;
    public bool Subtracting, launched, death, aboutToLaunch, success;
    public Slider forceSlider;
    private Vector3 startPosition;
    public Camera theCamera;
    public SpriteRenderer ballAimer;
    public Text successText, advanceText;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        ballAimer.enabled = true;
        success = false;
        successText.enabled = false;
        advanceText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //reset ball
        if (launched == true && success == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ResetBall());
            }
        }

        //force slider
        forceSlider.value = force;

        if (launched == true)
        {
            ballAimer.enabled = false;
        }
        else
        {
            ballAimer.enabled = true;
        }

        //if ready to launch...
        if (launched == false)
        {
            //before pressing space
            if (aboutToLaunch == false)
            {
                if (transform.rotation.z < 0.7071068f/*90*/)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        transform.Rotate(0, 0, rotationSpeed);
                    }
                }
                if (transform.rotation.z > -0.7071068f/*-90*/)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        transform.Rotate(0, 0, -rotationSpeed);
                    }
                }
            }
            //if first press space...
            if (Input.GetKeyDown(KeyCode.Space))
            {
                aboutToLaunch = true;
                transform.position = startPosition;
                rb.velocity = new Vector2(0, 0);
                theCamera.transform.position = new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z);
            }
            //if hold space down...
            if (Input.GetKey(KeyCode.Space))
            {
                //if going up...
                if (Subtracting == false)
                {
                    //add force multiplier
                    force += forceMultiplier;
                    //if reaches top, reverse
                    if (force >= 1000)
                    {
                        Subtracting = true;
                    }
                }
                else
                {
                    //subtract force multiplier
                    force -= forceMultiplier;
                    //if reaches bottom, reverse
                    if (force <= 0)
                    {
                        Subtracting = false;
                    }
                }
            }

            //if let go of space
            if (Input.GetKeyUp(KeyCode.Space))
            {
                launched = true;
                //rb.AddForce(Vector3.right * force);               //adds the force in world space right
                //rb.AddForce(Vector3.up * (force/1.25f));          //adds the force in world space up
                rb.AddForce(transform.right * force);               //adds the force in local space right  
            }
        }

        if (launched == true && success == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //go to next level
                print("Next level loading...");
            }
        }
    }

    IEnumerator ResetBall()
    {
        death = true;
        yield return new WaitForSeconds(0.75f);
        rb.velocity = new Vector2(0, 0);
        force = 0;
        Subtracting = false;
        yield return new WaitForSeconds(.4f);
        death = false;
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.Sleep();
        yield return new WaitForSeconds(.35f);
        launched = false;
        aboutToLaunch = false;
        rb.WakeUp();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator Success()
    {
        successText.enabled = true;
        success = true;
        yield return new WaitForSeconds(1.0f);
        advanceText.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            StartCoroutine(ResetBall());
        }
        if (other.gameObject.tag == "Hole")
        {
            StartCoroutine(Success());
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Floor")
        {
            rb.velocity *= .75f;
        }
    }
}
