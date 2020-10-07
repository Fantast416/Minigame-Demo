using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePlayer : MonoBehaviour
{
    private ArrayList m_List = new ArrayList();
    public Transform leftDownPos;
    public Transform rightUpPos;
    private int length;

    private ArrayList runAnim_List = new ArrayList();
    private ArrayList jumpAnim_List = new ArrayList();
    private ArrayList fallAnim_List = new ArrayList();
    private ArrayList idleAnim_List = new ArrayList();
    private int Animlength;

     private Animator myAnim; //为了使用player的动画功能
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();

        m_List = PlayerController.m_List;
        length = PlayerController.m_ListLength - 1;
        runAnim_List = PlayerController.runAnim_List;
        jumpAnim_List = PlayerController.jumpAnim_List;
        fallAnim_List = PlayerController.fallAnim_List;
        idleAnim_List = PlayerController.idleAnim_List;
        
        Animlength = PlayerController.Anim_ListLength - 1;
        Debug.Log(length);
    }

    void Update(){
        
    }
    // Update is called once per frame
    void LateUpdate()    
    {   
        UpdatePos();
        UpdateAnim();
    }

    void UpdatePos(){
        if(length>=0){
            float x = ((Vector3)m_List[length]).x;
            float y = ((Vector3)m_List[length]).y;
            float deltax = x- leftDownPos.position.x;
            float deltay = y - leftDownPos.position.y;
            float newx = rightUpPos.position.x - deltax;
            float newy = rightUpPos.position.y - deltay;
            transform.position =  new Vector2(newx,newy);
            length--;
        }  
    }

    void UpdateAnim(){
        if(Animlength>=0){
            bool run = (bool)runAnim_List[Animlength];
            bool jump =(bool)jumpAnim_List[Animlength];
            bool fall =(bool)fallAnim_List[Animlength];
            bool idle =(bool)idleAnim_List[Animlength];
             myAnim.SetBool("run",run);
             myAnim.SetBool("jump",jump);
              myAnim.SetBool("fall",fall);
               myAnim.SetBool("idle",idle);

            Debug.Log(run);
            Debug.Log(jump);
            Debug.Log(fall);
            Debug.Log(idle);
            Animlength--;
        }  
    }
}
