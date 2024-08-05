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

    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        current_pos = 0;    // 0 = center , 1  = Left , 2 = Right
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + running_speed * Time.deltaTime);
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

        if (current_pos == 0)
        {
            if (Vector3.Distance(transform.position, new Vector3(center_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
            {
                Vector3 dir = new Vector3(center_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
            }
        }
        else if (current_pos == 1)
        {
            if (Vector3.Distance(transform.position, new Vector3(left_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
            {
                Vector3 dir = new Vector3(left_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
            }
        }
        else if (current_pos == 2)
        {
            if (Vector3.Distance(transform.position, new Vector3(right_pos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
            {
                Vector3 dir = new Vector3(right_pos.position.x, transform.position.y, transform.position.z) - transform.position;
                transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(Vector3.up * jump_Force);
            rb.velocity = Vector3.up * jump_Force;
        }

    }

}
