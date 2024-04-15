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
    }
    private Vector2 saveTarget;
    public LayerMask LayerMask;
    public  float deltaDegree = 30f;
    public float delta2 = 5f;

    private bool MoveToTarget(float speed)
    {
        Vector2 targetPos;
        Vector2 Dir = player.GetComponent<Rigidbody2D>().velocity.normalized;
        if (Dir != Vector2.zero) savePlayerSpeedDir = Dir;
        else if (savePlayerSpeedDir != Vector2.zero) Dir = savePlayerSpeedDir;
        else Dir = Vector2.up;
        Dir = Quaternion.Euler(0, 0, 180) * Dir;
        Dir = Quaternion.Euler(0, 0, degree) * Dir;
        targetPos = (Vector2)player.position + Dir * distance;
        saveTarget = targetPos;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, (targetPos - (Vector2)transform.position).normalized, (targetPos - (Vector2)transform.position).magnitude, LayerMask);
        //if (hit.collider != null)
        //{
        //    Vector2 newVector = (targetPos - (Vector2)transform.position).normalized;
        //    float angle = Vector2.Angle(newVector, hit.normal);
        //    if (angle < deltaDegree) newVector = Quaternion.Euler(0, 0,deltaDegree) * newVector;
        //    targetPos = Vector2.Reflect(newVector, hit.normal)*100+hit.point;

        //}



        ////水平检测,移动y
        //if (Physics2D.Raycast((Vector2)transform.position + Collider2D.offset + Vector2.up * Collider2D.size.y * delta / 2, Vector2.right * Mathf.Sign(targetPos.x - transform.position.x), delta2, LayerMask).collider != null)
        //{
        //    Debug.Log("水平");
        //    //TransformMove.MoveTowards(transform, new Vector3(transform.position.x, targetPos.y + 1000, 0), (chasingSpeed + speed) * Time.fixedDeltaTime);
        //    targetPos += Vector2.up * 100;
        //}
        //else if (Physics2D.Raycast((Vector2)transform.position + Collider2D.offset - Vector2.up * Collider2D.size.y * delta / 2, Vector2.right * Mathf.Sign(targetPos.x - transform.position.x), delta2, LayerMask).collider != null)
        //{
        //    Debug.Log("水平");
        //    //TransformMove.MoveTowards(transform, new Vector3(transform.position.x, targetPos.y + 1000, 0), (chasingSpeed + speed) * Time.fixedDeltaTime);
        //    targetPos += Vector2.up * 100;

        //}
        //else if (Physics2D.Raycast((Vector2)transform.position + Collider2D.offset + Vector2.right * Collider2D.size.x * delta / 2, Vector2.up * Mathf.Sign(targetPos.y - transform.position.y), delta2, LayerMask).collider != null)
        //{
        //    Debug.Log("竖直");
        //    //TransformMove.MoveTowards(transform, new Vector3(targetPos.x + 1000, transform.position.y, 0), (chasingSpeed + speed) * Time.fixedDeltaTime);
        //    targetPos += Vector2.right * 100;
        //}
        //else if (Physics2D.Raycast((Vector2)transform.position + Collider2D.offset - Vector2.right * Collider2D.size.x * delta / 2, Vector2.up * Mathf.Sign(targetPos.y - transform.position.y), delta2, LayerMask).collider != null)
        //{
        //    Debug.Log("竖直");
        //    //TransformMove.MoveTowards(transform, new Vector3(targetPos.x + 1000, transform.position.y, 0), (chasingSpeed + speed) * Time.fixedDeltaTime);
        //    targetPos += Vector2.right * 100;
        //}



        bool value = TransformMove.MoveTowards(transform, targetPos, speed * Time.fixedDeltaTime);


        return value;
    }
    public void InitData(Transform player,int index)
    {
        this.player = player;
        this.index = index;
        int number = Mathf.FloorToInt(Mathf.Sqrt(index));
        int allNumber = 2 * number + 1;
        int which = index - number * number;
        degree = -(openDegree / 2) + openDegree / allNumber * which;
        distance =number/Mathf.Cos(degree*Mathf.Deg2Rad)* deltaDistance;


    }
    public void UpdateFollowData(int index)
    {
        int number = Mathf.FloorToInt(Mathf.Sqrt(index));
        int allNumber = 2 * number + 1;
        int which = index - number * number;
        degree = -(openDegree/2) + openDegree / allNumber * which;
        distance = number / Mathf.Cos(degree * Mathf.Deg2Rad)* deltaDistance;
    }

    public void TakeDamage(int damage)
    {
        FollowController.instance.UpdateRank(index);
        CachePool.Destroy(gameObject);

    }
}
