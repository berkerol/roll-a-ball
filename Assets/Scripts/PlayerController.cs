using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speedMultiplier;
    public Text scoreText;
    public Text livesText;

    private Rigidbody rb;
    private int score;
    private int lives;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        lives = 10;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }

//    private void Update()
//    {
//        if (Input.GetKey(KeyCode.UpArrow))
//        {
//            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
//        }
//
//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
//        }
//
//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
//        }
//
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
//        }
//    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speedMultiplier);
    }

    //private void OnTriggerEnter(Collider other)
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            scoreText.text = "Score: " + ++score;
            Material material = other.gameObject.GetComponent<Renderer>().material;
            if (material.color.Equals(Color.red))
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (material.color.Equals(Color.yellow))
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (material.color.Equals(Color.blue))
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            livesText.text = "Lives: " + --lives;
            if (lives < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}