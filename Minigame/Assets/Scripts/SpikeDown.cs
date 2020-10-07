using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDown : MonoBehaviour
{
    public float waitTime;
    private PlayerHealth playerHealth;
    private Rigidbody2D myRigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.gravityScale = 0;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waitTime>=0){
            waitTime -= Time.deltaTime;
        }
        if(waitTime<0){
            myRigidbody2D.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D"){
            if(playerHealth!=null){
                playerHealth.DamagePlayer(20);        
            }
        }
        
    }
}
