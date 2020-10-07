using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool IsReverse = false;
    public static ArrayList m_List = new ArrayList();
    public static int m_ListLength = 0;


    public static ArrayList runAnim_List = new ArrayList();
     public static ArrayList jumpAnim_List = new ArrayList();
      public static ArrayList idleAnim_List = new ArrayList();
       public static ArrayList fallAnim_List = new ArrayList();
    public static int Anim_ListLength = 0;

    public float speed = 5.0f;
    public float jumpspeed = 10.0f;
    public float doublejumpspeed = 8.0f;

    public bool isGround = true;
    private bool canDoubleJump = false;

    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D myFeet;
    private Animator myAnim; //为了使用player的动画功能
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        Jump();
        Flash();
        // Attack();
        CheckGrounded();
        SwitchAnimation();
        LogPlayerPos();
        LogPlayerAnim();
    }
    void LogPlayerAnim(){
        if(!IsReverse){
            bool run = myAnim.GetBool("run");
            bool jump =myAnim.GetBool("jump");
            bool fall =myAnim.GetBool("fall");
            bool idle =myAnim.GetBool("idle");
            
            runAnim_List.Add(run);
            jumpAnim_List.Add(jump);
            fallAnim_List.Add(fall);
            idleAnim_List.Add(idle);

            Anim_ListLength++;
        }else{
            Debug.Log("2");
        }
    }

    void LogPlayerPos(){
        if(!IsReverse){
            m_List.Add(transform.position);
            Debug.Log(m_List.Capacity);
            m_ListLength++;
        }else{
            Debug.Log("2");
        }
    }

    void Flip(){
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if(playerHasXAxisSpeed){
            if(myRigidbody2D.velocity.x > 0.1f){
                transform.localRotation = Quaternion.Euler(0,0,0);
            }

            if(myRigidbody2D.velocity.x < -0.1f){
                transform.localRotation = Quaternion.Euler(0,180,0);
            }

        }
    }


    void Run(){
        float moveDir = Input.GetAxis("Horizontal");  //返回值范围为-1 ~ 1 之间 ，带惯性跑动
        Vector2 playerVel = new Vector2(moveDir*speed,myRigidbody2D.velocity.y);  //x方向设置速度，y方向速度保持不便
        myRigidbody2D.velocity = playerVel;  
        /*判断是否有横向速度来改变人物动画效果*/
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("run",playerHasXAxisSpeed);
    }


    void Jump(){
        
        if(Input.GetButtonDown("Jump")){
            if(isGround){ /*一段跳*/
                Vector2 jumpVel = new Vector2(0.0f,jumpspeed);
                myRigidbody2D.velocity = Vector2.up * jumpVel;

                myAnim.SetBool("jump",true);

                canDoubleJump = true;
            }else{
                if(canDoubleJump){    /*若不需要二段跳功能请删除此段*/
                /*二段跳*/
                    Vector2 doublejumpVel = new Vector2(0.0f,doublejumpspeed);
                    myRigidbody2D.velocity = Vector2.up * doublejumpVel;
                    canDoubleJump = false;
                }
            }
        }
    
    }

    void Flash(){
        if(Input.GetButtonDown("Flash")){
            bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
            if(playerHasXAxisSpeed){
                if(myRigidbody2D.velocity.x > 0.1f){
                    transform.position = transform.position + new Vector3(2.0f,0.0f,0.0f);   
                }

                if(myRigidbody2D.velocity.x < -0.1f){
                    transform.position = transform.position + new Vector3(-2.0f,0.0f,0.0f);   
                }

        }         
        }
    }

    /*检测是否在地面上*/
    void CheckGrounded(){
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }


    /*动画切换（跳起到落下）*/
    void SwitchAnimation(){
        myAnim.SetBool("idle",false);

        /*已经在跳跃过程中*/
        if(myAnim.GetBool("jump")){
            /*已经在下降过程*/
            if(myRigidbody2D.velocity.y<0.0f){
                myAnim.SetBool("jump",false);
                myAnim.SetBool("fall",true);
            }
        }else if(isGround){
            myAnim.SetBool("fall",false);
            myAnim.SetBool("idle",true);
        }
    }


    /*如不需要Attack注释如下代码即可*/
//     void Attack(){
//          if(Input.GetButtonDown("Attack")){ 
//             myAnim.SetTrigger("Attack");
//          }
//     }
}
