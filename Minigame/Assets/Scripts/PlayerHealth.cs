using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private Renderer myRender;
    public int numBlinks;
    public float time;
    public float dieTime;

    private Animator myAnim; //为了使用player的动画功能
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<Renderer>();    
        myAnim = GetComponent<Animator>();
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage){
        health -= damage;

        HealthBar.HealthCurrent = health;

        if(health <=0){
            myAnim.SetTrigger("Death");
            Invoke("KillPlayer",dieTime);
        }
        if(health>0)
            BlinkPlayer(numBlinks,time);
    }

    void KillPlayer(){
        Destroy(gameObject);
    }

    /*闪烁次数 && 闪烁时间*/
    void BlinkPlayer(int numBlinks,float seconds){
        StartCoroutine(DoBlinks(numBlinks,seconds));
    }

    /*协程函数 */
    IEnumerator DoBlinks(int numBlinks,float seconds){
        for(int i = 0; i < numBlinks; i++){
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
