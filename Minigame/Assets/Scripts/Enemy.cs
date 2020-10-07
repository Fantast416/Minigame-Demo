using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//创建抽象类Enemy
public abstract class Enemy : MonoBehaviour
{
    public int blood;  //蝙蝠血量
    public int damage; //蝙蝠伤害值
    public float flashtime;

    private SpriteRenderer sr;
    private Color originColor;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if(blood<=0){
            Destroy(gameObject);
        }    
    }

    //敌人受到攻击后，要给player进行调用的函数
    public void TakeDamage(int damage){
        blood -= damage;
        FlashColor(flashtime);
    }

    
    void FlashColor(float time){
        sr.color = Color.red;
        Invoke("ResetColor",time); //延时一段时间调用ResetColor函数

    }

    void ResetColor(){
        sr.color = originColor;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D"){
            if(playerHealth!=null){
                playerHealth.DamagePlayer(damage);        
            }

        }
    }
}
