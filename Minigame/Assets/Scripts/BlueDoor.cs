using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
         playerHealth = GameObject.FindGameObjectWithTag("ReversePlayer").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("ReversePlayer") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D"){
            if(playerHealth!=null){
                playerHealth.DamagePlayer(20);        
            }
        }
        
    }
}
