using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AIState { Guard, Chase, Kill }

public class AIController : MonoBehaviour
{
    public Transform player;
    public Transform castPoint;
    public float agroRange;
    public float killRange;
    public float hearingRange;
    public float moveSpeed;
    public AIState currentState;
    public Rigidbody2D rb;
    public Vector2 originalPos;

    public Text enemyText;
    public Text hearingText;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       currentState = AIState.Guard;   
       originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Find the distance to the player.
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        //Change Range Depending on Volume
        if (PlayerController.volume == 5)
        {
            agroRange = 25;
            killRange = 5;
        }
        else
        {
            agroRange = 50;
            killRange = 10;
        }

        //Set Hearing Range
        if (distToPlayer <= hearingRange && PlayerController.volume >= 10)
        {   
            hearingText.text = "Enemy: What was that?";
        }
        else
        {        
            hearingText.text = "Enemy: I can't hear anything";
        }

        //Set Agro Range
        if (distToPlayer <= agroRange)
        {
            currentState = AIState.Chase;
        }
        else if (distToPlayer > agroRange)
        {
            currentState = AIState.Guard; 
        }

        //Set Kill Range
        if (distToPlayer <= killRange)
        {
            currentState = AIState.Kill;
            enemyText.text = "Enemy: What a fool.";
        }

        //Start Chasing
        if (currentState == AIState.Chase)
        {
            ChasePlayer();
            enemyText.text = "Enemy: Gotcha NOW!";
        }

        //Start Guarding
        if (currentState == AIState.Guard)
        {
            StopChasing();     
            enemyText.text = "Enemy: DAMN! I lost him.";
        }

        //Kill The Player
        if (currentState == AIState.Kill)
        {
            KillPlayer();
        }

    }

    void ChasePlayer()
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;
        }

    void StopChasing()
        {
            rb.velocity = new Vector2(0,0);      
        }

    void KillPlayer()
        {
           Destroy(GameObject.FindWithTag("Player"));
        }   
}
