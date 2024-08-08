using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controler : MonoBehaviour
{
    [SerializeField] Transform center_pos;
    [SerializeField] Transform left_pos;
    [SerializeField] Transform right_pos;

    [Header ("Android Controls")]
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    int current_pos = 0;

    public float side_speed;
    public float running_speed;
    public float jump_Force;
    public float slide_Force;

    [SerializeField] Rigidbody rb;

    bool isGameStarted = false;
    bool isGameOver = false;

    [SerializeField] Animator player_Animator;
    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false ;
        isGameOver = false ;
        current_pos = 0;    // 0 = center , 1  = Left , 2 = Right
        dragDistance = Screen.height * 1 / 100; //dragDistance is 5% height of the screen
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted || !isGameOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Game is Started");
                isGameStarted = true ;
                player_Animator.SetInteger("isRunning", 1);
                player_Animator.speed = 1.3f;
            }
        }


        if (isGameStarted)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + running_speed * Time.deltaTime);
#if UNITY_EDITOR

            if (current_pos == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    current_pos = 1;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    current_pos = 2;
                }
            }
            else if (current_pos == 1)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    current_pos = 0;
                }
            }
            else if (current_pos == 2)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    current_pos = 0;
                }

            }

#elif UNITY_ANDROID

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            if (current_pos == 0)
                            {
                                current_pos = 2;
                            }
                            else if (current_pos == 1)
                            {
                                current_pos = 0;
                            }
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            if (current_pos == 0)
                            {
                                current_pos = 1;
                            }
                            else if (current_pos == 2)
                            {
                                current_pos = 0;
                            }
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            rb.velocity = Vector3.up * jump_Force;
                            StartCoroutine(Jump());
                            
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            rb.velocity = Vector3.down * slide_Force;
                            StartCoroutine(Slide());
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }

#endif

            if (current_pos == 0)
            {
                if (Vector3.Distance(transform.position, new Vector3(center_pos.position.x, transform.position.y, transform.position.z)) >= 0.5f)
                {
                    Vector3 dir = new Vector3(center_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }
            else if (current_pos == 1)
            {
                if (Vector3.Distance(transform.position, new Vector3(left_pos.position.x, transform.position.y, transform.position.z)) >= 0.5f)
                {
                    Vector3 dir = new Vector3(left_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }
            else if (current_pos == 2)
            {
                if (Vector3.Distance(transform.position, new Vector3(right_pos.position.x, transform.position.y, transform.position.z)) >= 0.5f)
                {
                    Vector3 dir = new Vector3(right_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                    transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                rb.velocity = Vector3.up * jump_Force;
                StartCoroutine(Jump());

            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                rb.velocity = Vector3.down * slide_Force;
                StartCoroutine(Slide());

            }

        }
    }

    IEnumerator Jump()
    {
        player_Animator.SetInteger("isJump", 1);
        yield return new WaitForSeconds(0.1f);
        player_Animator.SetInteger("isJump", 0);
    }

    IEnumerator Slide()
    {
        player_Animator.SetInteger("isSlide", 1);
        yield return new WaitForSeconds(0.1f);
        player_Animator.SetInteger("isSlide", 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            isGameStarted = false;
            isGameOver = true;
            player_Animator.applyRootMotion = true;
            player_Animator.SetInteger("isDied", 1);
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.isKinematic = true;
            
            rb.interpolation = RigidbodyInterpolation.Interpolate;

        }
    }

}
