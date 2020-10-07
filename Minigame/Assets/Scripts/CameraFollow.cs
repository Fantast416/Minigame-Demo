using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //target 代表player的位置
    public float smoothing; //为了让相机更平滑，smoothing是一个平滑因子


    public Vector2 minPosition;
    public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate(){
        if(target != null){  //判断先决条件 —— target存在，即玩家对象还没死亡
            if(transform.position != target.position){
                /*Lerp 为一个线性插值函数，其意为在第1和第2个参数之间按照平滑参数进行插值得到值*/
                //transform.position = target.position;
                Vector2 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x,minPosition.x,maxPosition.x); //限制目标位置在最小的x和最大的x之间
                targetPos.y = Mathf.Clamp(targetPos.y,minPosition.y,maxPosition.y); //限制目标位置在最小的x和最大的x之间
                transform.position = Vector3.Lerp(transform.position,targetPos,smoothing);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*可让外界利用函数设置Camera界限*/
    public void SetCamPosLimit(Vector2 minPos,Vector2 maxPos){
        minPosition = minPos;
        maxPosition = maxPos;
    }

}   
