using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BirdControl : MonoBehaviour
{
    public Sprite []birdSprite; //kus animasyonu icin dizi olusturuluyor
    SpriteRenderer spriteRenderer;
    bool movingControl = true;
    int birdCounter = 0;
    float birdAnimSpeed = 0;
    int point = 0;
    Rigidbody2D physics;
    bool gameOver=true;

    public Text scoreText;

    GameControl gameControl;

    AudioSource []sounds;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<GameControl>();
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameOver==true)
        {
            physics.velocity = new Vector2(0, 0);  //hızı sıfır yaptık ondan sonra yükseliyor(yercekimine karsi)
            physics.AddForce(new Vector2(0,200));
            sounds[0].Play();
            
        }
        if (physics.velocity.y>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 40);

        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -40);

        }
        Animation();
    }      

    void Animation()
    {
        birdAnimSpeed += Time.deltaTime;

        if (birdAnimSpeed > .1f)
        {
            birdAnimSpeed = 0;
            if (movingControl)
            {
                spriteRenderer.sprite = birdSprite[birdCounter];
                birdCounter++;
                if (birdCounter == birdSprite.Length)
                {
                    birdCounter--;
                    movingControl = false;
                }

            }
            else
            {
                birdCounter--;
                spriteRenderer.sprite = birdSprite[birdCounter];
                if (birdCounter == 0)
                {
                    birdCounter++;
                    movingControl = true;
                }


            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Point")
        {
            point++;
            scoreText.text = "Score = " + point;
            Destroy(GameObject.FindGameObjectWithTag("Point"));
            sounds[1].Play();
            
            

        }
        if (col.gameObject.tag == "Pipe")
        {
            gameOver = false;
            gameControl.gameOver();
            sounds[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;
            

        }

    }
    
}
