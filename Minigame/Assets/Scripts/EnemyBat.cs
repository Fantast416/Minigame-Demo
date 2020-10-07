using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime; //等待时间
    private float waitTime;

    public Transform movePos;  //坐标变量，下一次要移动到的位置

    //限定蝙蝠飞动范围
    public Transform leftDownPos;
    public Transform rightUpPos;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        
        transform.position = Vector2.MoveTowards(transform.position,movePos.position,speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,movePos.position)<0.1f){
            if(waitTime <=0){
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }else{
                waitTime -= Time.deltaTime;
            }            
        }
    }

    public Vector2 GetRandomPos(){
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x,rightUpPos.position.x),Random.Range(leftDownPos.position.y,rightUpPos.position.y));
        return rndPos;
    }

}
