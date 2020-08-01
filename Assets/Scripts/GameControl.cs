using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    

    public GameObject backgroundOne;
    public GameObject backgroundTwo;

    public float backgroundSpeed = -1.5f;

    Rigidbody2D physicsOne;
    Rigidbody2D physicsTwo;
    Rigidbody2D physicsThree;

    float lenght = 0;

    public GameObject Pipe;
    public int pipeCounter;
    GameObject []pipes;
    public int pipeCounter2=0;

    float timeChange;
    bool endGame = true;

    void Start()
    {
        physicsOne = backgroundOne.GetComponent<Rigidbody2D>();
        physicsTwo = backgroundTwo.GetComponent<Rigidbody2D>();
        

        physicsOne.velocity = new Vector2(backgroundSpeed, 0);
        physicsTwo.velocity = new Vector2(backgroundSpeed, 0);

        lenght = backgroundOne.GetComponent<BoxCollider2D>().size.x;

        pipes = new GameObject[pipeCounter];


        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = Instantiate(Pipe, new Vector2(-20, -20), Quaternion.identity);
            physicsThree = pipes[i].GetComponent<Rigidbody2D>();
            physicsThree.velocity = new Vector2(backgroundSpeed, 0);

        }
    }

    
    void Update()
    {
        if (endGame)
        {

            if (backgroundOne.transform.position.x <= - lenght)
            backgroundOne.transform.position += new Vector3(lenght * 2, 0);

            if (backgroundTwo.transform.position.x <= - lenght)
            backgroundTwo.transform.position += new Vector3(lenght * 2, 0);

        //------------------pipes spawning---------------------------

        timeChange += Time.deltaTime;
            if (timeChange > 2f)
             {
                timeChange = 0;
            float yAxis = Random.Range(-0.95f,-3.45f);
            pipes[pipeCounter2].transform.position = new Vector3(6, yAxis);
            pipeCounter2++;
            if (pipeCounter2 >= pipes.Length)
                pipeCounter2 = 0;
             }
        }
        
            



    }

    public void gameOver()
    {


        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            physicsOne.velocity = Vector2.zero;
            physicsTwo.velocity = Vector2.zero;
            physicsThree.velocity = Vector2.zero;
        }
        endGame = false;

    }
}
