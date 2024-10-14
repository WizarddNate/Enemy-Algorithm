using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float normalspeed = 1.5f;
    public float sprintspeed = 100.0f;
    public float speed = 0.0f;
    //In C#, all variables are private by default 
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (!rb2d) // (rb2d == null)
            Debug.LogWarning("Rigidbody not found on GameObject " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

        speed = normalspeed;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 inputDirection = new Vector2(h, v);
        rb2d.AddForce(inputDirection * speed);
   

       /// sprint function ///
        //if (Input.GetButton("Fire3"))
        //{
        //    speed = sprintspeed;
        //}
        //else
        //{
        //    speed = normalspeed;
        //}
    }
}