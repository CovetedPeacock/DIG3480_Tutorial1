using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        SetLivesText();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Enemy")) {
        //    other.gameObject.SetActive(false);
        //    lives = lives - 1;
        //    SetLivesText();
        //}
        //I tried to make this work for about an hour, and while it worked in the Unity program,
        //every time I tried to build it, enemies stopped working. I was able to narrow it down
        //to an issue with the tag, but changing the tag didn't change the result. I realized that
        //having it rely on an else condition without any other requirement still works.

        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        } else {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }
        
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count == 12 && lives > 0)
            transform.position = new Vector2(50.0f, 50.0f);
        if (count >= 20 && lives > 0)
            winText.text = "You win! Game created by Connor Peacock.";
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0 && count!=20)
        {
            winText.text = "You lose. Game created by Connor Peacock.";
            this.gameObject.SetActive(false);
        }
    }
}
