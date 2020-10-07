using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public float startWaitTime; //等待时间
    private float waitTime;
    private bool MoveDir = false;
    private bool IsMoving = false;

    public Transform originPos;
    public Transform targetPos;
    // Start is called before the first frame update
    void Start()
    {   Debug.Log(IsMoving);
        waitTime = startWaitTime;
        transform.position = originPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartMoving();
    }

    void StartMoving(){
        Transform movPos;
        if(IsMoving){
            Debug.Log("11");
            if(MoveDir == true){
                movPos = targetPos;
            }else{
                movPos = originPos;
            }
            Debug.Log("111");
            transform.position = Vector2.MoveTowards(transform.position,movPos.position,speed * Time.deltaTime);
            if(Vector2.Distance(transform.position,movPos.position)<0.1f){
                if(waitTime <= 0){
                    Debug.Log("1111");
                    IsMoving = false;
                }else{
                    Debug.Log("11111");
                    waitTime -= Time.deltaTime;
                }            
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ontrigger");
        Debug.Log(IsMoving);
        Debug.Log("1");
        Debug.Log(other.gameObject.CompareTag("Player"));
        Debug.Log("2");
        if(!IsMoving && other.gameObject.CompareTag("Player")){
            Debug.Log("Ontrigger2");
            waitTime = startWaitTime;
            MoveDir = !MoveDir;
            IsMoving = true;
        }
    }

}
