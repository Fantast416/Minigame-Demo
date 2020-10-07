using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float starttime;
    public float disabletime;

    private Animator Anim;
    private PolygonCollider2D coll2D;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack(){
        if(Input.GetButtonDown("Attack")){ 
            Anim.SetTrigger("Attack"); 
            StartCoroutine(startHitBox());
        }
    }

    IEnumerator startHitBox(){
        yield return new WaitForSeconds(starttime);
        coll2D.enabled = true;
        StartCoroutine(disableHitBox());
    }

    IEnumerator disableHitBox(){
        yield return new WaitForSeconds(disabletime);
        coll2D.enabled = false;
    }

    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("TagEnemy");
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
