using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text controls;

    public Text winText;

    public Text loseText;

    private int liveValue = 3;

    public Text livesText;

    private int scoreValue = 0;
    private bool facingRight = true;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool walking = false;
    Animator anim;
    public float run;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Points: " + scoreValue.ToString();
        winText.text = "";
        loseText.text = "";
        controls.text = "Controls: A and D walk left and right. W Jumps. Holding Spacebar Runs.";
        livesText.text = "Lives: " + liveValue.ToString();
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        
    }

    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
        else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }
    }
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
         if (Input.GetKeyDown(KeyCode.D))
        {
            walking = true;
            anim.SetBool("isWalking",true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            walking = false;
            anim.SetBool("isWalking",false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            walking = true;
            anim.SetBool("isWalking",true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            walking = false;
            anim.SetBool("isWalking",false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rd2d.AddForce(new Vector2(hozMovement * speed * run, vertMovement * speed));
            anim.SetBool("isRunning",true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isRunning",false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Points: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            controls.text = "";
            if (scoreValue == 4)
            {
                transform.position = new Vector2(127f,2f);
                liveValue = 3;
                livesText.text = "Lives: "+ liveValue.ToString();
            }
            if (scoreValue >= 8)
            {
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
                winText.text = "You Win! Game Created by Jordan Carswell";
            }
        }
        if (collision.collider.tag =="Enemy")
        {
            liveValue -= 1;
            livesText.text = "Lives: " + liveValue.ToString();
            Destroy(collision.collider.gameObject);
            
            if( liveValue <= 0)
                {
                    Destroy(this.gameObject);
                    loseText.text = "You Lose! Game Created by Jordan Carswell";
                }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isOnGround == true && Input.GetKey(KeyCode.W))
        {
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anim.SetBool("isJumping",true);
            }
        }
        else
        {
            anim.SetBool("isJumping",false);
        }
    }
}