using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f;

    public Rigidbody2D rb;
    public Camera cam;
    public static float volume;
    Vector2 movement;
    Vector2 mousePos;

    public Text volumeText;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical"); 
        movement = movement.normalized;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25f;
            volume = 5;
        }
        else
        {
            moveSpeed = 50f;
            volume = 10;
        }

        volumeText.text = "Volume: " + volume;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

}
