using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdControl : MonoBehaviour
{
    public bool usingMouseSlingShot;
    public float force, forceMultiplier, rotationSpeed;
    public Rigidbody2D rb;
    public bool Subtracting, launched, death, aboutToLaunch, success;
    public Slider forceSlider;
    private Vector3 startPosition;
    public Camera theCamera;
    public SpriteRenderer ballAimer;
    public Text successText, advanceText;

    Vector3 dir;
    float distance;
    public float maxPullDistance;
    float pullPercent;
    Vector3 mousePos;
    float baseLaunchForce = 100;
    public float launchForceBonus;
    public Image powerBar;


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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ResetBall());
            }
        }

        //force slider
        forceSlider.value = force;

        if(usingMouseSlingShot == false)
        {
            if (launched == true)
            {
                ballAimer.enabled = false;
            }
            else
            {
                ballAimer.enabled = true;
            }
        }
        else
        {
            ballAimer.enabled = false;
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

            //If using mouse click
            //if first press space...
            if (Input.GetMouseButtonDown(0))
            {
                aboutToLaunch = true;
                transform.position = startPosition;
                rb.velocity = new Vector2(0, 0);
                theCamera.transform.position = new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z);
            }

            //Mouse Held
            if (Input.GetMouseButton(0))
            {
                //Get mouse
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                //distance of mouse to ball
                distance = Vector3.Distance(mousePos, transform.position);

                //Direction the player is aiming
                dir = transform.position - mousePos;
                dir.Normalize();

                //get amount pulled back
                distance = Mathf.Clamp(distance, 0, maxPullDistance);
                //Makes a percent of how far the player is pulling back relative to the max
                pullPercent = distance / maxPullDistance;

                //Power Bar Display
                powerBar.gameObject.SetActive(true);
                powerBar.fillAmount = pullPercent;
                //angles the bar
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                powerBar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }

            //Launch
            if (Input.GetMouseButtonUp(0))
            {
                launched = true;
                rb.isKinematic = false;
                // makes a force with a base force and an additional bonus force depending how far back the player pulls
                float force = baseLaunchForce + launchForceBonus * pullPercent;
                rb.AddForce(dir * force);
                powerBar.gameObject.SetActive(false);
            }
        }

        //if in the hole
        if (launched == true && success == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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
