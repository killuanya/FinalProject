using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text countText;
    private int count;
    private int lives;
    public Text livesText;
    public Text winText;
    Animator anim;
    private bool facingRight = true;
    public AudioSource musicSource;
    public AudioSource coinSource;
    public AudioSource jumpSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip coinClip;
    public AudioClip jumpClip;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        SetLivesText();
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
        coinSource.clip = coinClip;
        jumpSource.clip = jumpClip;
        musicSource.Play();
}


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinSource.Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            Debug.LogFormat("Live subtracted from {0}", other.gameObject.name);
            lives = lives - 1;
            SetLivesText();
        }
        if (other.gameObject.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
            speed = speed + 10;
        }
        
    }


        void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if (count == 4)
        {
            transform.position = new Vector2(152.9f, 35.68f);
            lives = 3;
            SetLivesText();
        }
        if (count == 8)
        {
            winText.text = "You win! Game created by Emily Rogers!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

       
    }
    void SetLivesText()
    {
        livesText.text = "Lives:" + lives.ToString();
        if (lives < 1)
        {
            winText.text = "You lose! Game created by Emily Rogers!";
            Destroy(gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                jumpSource.Play(); 

            
            }
            anim.SetInteger("JumpState", 0);
            
        }
    }
    private void Update()
    {


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        //Right movement
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("RunState", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("RunState", 0);
        }
        //Left movement
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("RunState", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("RunState", 0);
        }
        //Jumping
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("JumpState", 1);
        }
    }
}
