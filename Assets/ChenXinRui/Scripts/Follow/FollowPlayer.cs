using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour, getDamage
{
    public float deltaDistance;
    public float openDegree;
    private Rigidbody2D rb;
    private Transform player;
    private int index;
    public float chasingSpeed;
    public float followSpeed;
    private bool arrival = false;
    private float distance;
    private float degree;
    private Vector2 savePlayerSpeedDir;

    private Vector2 lastPos;
    private CapsuleCollider2D Collider2D;
    private void Awake()
    {
        Collider2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //lastPos=transform.position;
        if (!arrival)
        {

            if (MoveToTarget(chasingSpeed))
            {
                arrival = true;
            }
         

        }
        else
        {
            MoveToTarget(followSpeed);
          
            
        }
        lastPos = transform.position;
        savePlayerLastPos = player.position;
    }
    private Vector2 saveTarget;
    public LayerMask LayerMask;
    public  float deltaDegree = 30f;
    public float delta2 = 5f;
    private Vector3 savePlayerLastPos;
    private bool MoveToTarget(float speed)
    {
        Vector2 targetPos;
        Vector2 Dir = player.GetComponent<Rigidbody2D>().velocity.normalized;
        if (Dir != Vector2.zero) savePlayerSpeedDir = Dir;
        else if (savePlayerSpeedDir != Vector2.zero) Dir = savePlayerSpeedDir;
        else Dir = Vector2.up;
        Dir = Quaternion.Euler(0, 0, 180) * Dir;
        if((savePlayerLastPos-player.position).magnitude<0.0005f) Dir = Quaternion.Euler(0, 0, 360* which / allNumber) * Dir;
        else Dir = Quaternion.Euler(0, 0, degree) * Dir;
        targetPos = (Vector2)player.position + Dir * distance;
        saveTarget = targetPos; 


        bool value = TransformMove.MoveTowards(transform, targetPos, speed * Time.fixedDeltaTime);


        return value;
    }
    public int which;
    public int allNumber;
    public void InitData(Transform player,int index)
    {
        this.player = player;
        this.index = index;
        int number = Mathf.FloorToInt(Mathf.Sqrt(index));
        allNumber = 2 * number + 1;
        which = index - number * number;
        degree = -(openDegree / 2) + openDegree / allNumber * which;
        distance =number/Mathf.Cos(degree*Mathf.Deg2Rad)* deltaDistance;


    }
    public void UpdateFollowData(int index)
    {
        int number = Mathf.FloorToInt(Mathf.Sqrt(index));
        allNumber = 2 * number + 1;
        which = index - number * number;
        degree = -(openDegree/2) + openDegree / allNumber * which;
        distance = number / Mathf.Cos(degree * Mathf.Deg2Rad)* deltaDistance;
    }

    public void TakeDamage(int damage)
    {
        FollowController.instance.UpdateRank(index);
        CachePool.Destroy(gameObject);

    }
}
