using Pixeye.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;

public struct CameraAnchorPoint
{
    private Vector2 offSet;
    private Transform camera;
    private Transform player;
    private float deltaX, deltaY;
    public float PosX => deltaX + camera.position.x + offSet.x;
    public float PosY => deltaY + camera.position.y + offSet.y;

    public float moveToPosX => player.position.x - deltaX - offSet.x;

    public float moveToPosY => player.position.y - deltaY - offSet.y;

    public CameraAnchorPoint(Transform camera, Transform player, float deltaX, float deltaY,Vector2 offSet)
    {
        this.offSet = offSet;
        this.player = player;
        this.camera = camera;
        this.deltaX = deltaX;
        this.deltaY = deltaY;
    }
}

public struct CameraSize
{
    private Camera camera;

    public CameraSize(Camera camera)
    {
        this.camera = camera;
    }
    public Vector2 size
    {
        get
        {
            float cameraOrthoSize = camera.orthographicSize;
            // 计算摄像机的宽度和高度
            float cameraWidth = cameraOrthoSize * 2.0f * camera.aspect;
            float cameraHeight = cameraOrthoSize * 2.0f;

            return new Vector2(cameraWidth, cameraHeight);

        }
    }
}


public class CameraController : MonoBehaviour
{
    [Foldout("其他设置", true)]
    public GizmosSetting CameraRange;
    private CameraSize cameraSize;
    public bool closeAuxiliaryLine;
    public Transform camera, player;
    #region 水平移动设置
    [Foldout("水平移动设置", true)]
    public Vector2 offSet;
    [Header("delta_1为靠内的，delta_2为长靠外的")]
    public float deltaX_1;
    public float deltaX_2;
    [Range(1, 100)] public float moveSpeedX;
    public float followSmoothTime_X;
    private float smoothTime_X => (deltaX_1 + deltaX_2) / moveSpeedX;
    [SerializeField] private bool arrivalAnchorX = true;
    private bool isLeftAnchor = false;
    
    private List<CameraAnchorPoint> anchorPoint_X = new List<CameraAnchorPoint>();
    #endregion


    #region 竖直移动设置
    [Foldout("竖直移动设置", true)]
    [Header("deltaY_1为靠内的，deltaY_2为靠外的")]
    public float deltaY_1;
    public float deltaY_2;
    [Range(1, 100)] public float moveSpeedY;
    public float followSmoothTime_Y;
    private float smoothTime_Y => (deltaY_1 + deltaY_2) / moveSpeedY;
    [SerializeField] private bool arrivalAnchorY = true;
    private bool isDownAnchor = false;
    private List<CameraAnchorPoint> anchorPoint_Y = new List<CameraAnchorPoint>();

    #endregion



    private void Awake()
    {

        cameraSize = new CameraSize(Camera.main);
        anchorPoint_X.Add(new CameraAnchorPoint(camera, player, -deltaX_2, 0, offSet));
        anchorPoint_X.Add(new CameraAnchorPoint(camera, player, -deltaX_1, 0, offSet));
        anchorPoint_X.Add(new CameraAnchorPoint(camera, player, deltaX_1, 0, offSet));
        anchorPoint_X.Add(new CameraAnchorPoint(camera, player, deltaX_2, 0, offSet));

        anchorPoint_Y.Add(new CameraAnchorPoint(camera, player, 0, -deltaY_2, offSet));
        anchorPoint_Y.Add(new CameraAnchorPoint(camera, player, 0, -deltaY_1, offSet));
        anchorPoint_Y.Add(new CameraAnchorPoint(camera, player, 0, deltaY_1, offSet));
        anchorPoint_Y.Add(new CameraAnchorPoint(camera, player, 0, deltaY_2, offSet));


    }


    private void FixedUpdate()
    {

        CameraHorizontalMove();
        CameraVerticalMove();

        MoveToAnchorX();
        MoveToAnchorY();
    }

    private void Update()
    {

    }
    Vector3 currentVelocity_X = Vector3.zero;
    Vector3 currentVelocity_Y = Vector3.zero;

    private void CameraHorizontalMove()
    {
        if (player != null)
        {
            if (!arrivalAnchorX) return;
            if (player.position.x > camera.position.x + offSet.x)
            {
                if (player.position.x < anchorPoint_X[2].PosX && !isLeftAnchor) MoveToX(camera, anchorPoint_X[2].moveToPosX, ref currentVelocity_X, followSmoothTime_X, 100, Time.fixedDeltaTime);
                else if (player.position.x > anchorPoint_X[2].PosX && player.position.x < anchorPoint_X[3].PosX)
                {
                    isLeftAnchor = false;
                }
                else if (player.position.x > anchorPoint_X[3].PosX)
                {
                    arrivalAnchorX = false;
                    isLeftAnchor = true;
                }
            }
            else
            {
                if (player.position.x > anchorPoint_X[1].PosX && isLeftAnchor) MoveToX(camera, anchorPoint_X[1].moveToPosX, ref currentVelocity_X, followSmoothTime_X, 100, Time.fixedDeltaTime);//TransformMove.MoveX(camera, anchorPoint[1].moveToPosX, 100);
                else if (player.position.x > anchorPoint_X[0].PosX && player.position.x < anchorPoint_X[1].PosX)
                {
                    isLeftAnchor = true;
                }
                else if (player.position.x < anchorPoint_X[0].PosX)
                {
                    arrivalAnchorX = false;
                    isLeftAnchor = false;
                }
            }
        }

    }
    private void MoveToAnchorX()
    {
        if (arrivalAnchorX) return;
        if (isLeftAnchor)
        {
            if (MoveToX(camera, anchorPoint_X[1].moveToPosX, ref currentVelocity_X, smoothTime_X, moveSpeedX, Time.fixedDeltaTime)) arrivalAnchorX = true;
        }
        else
        {
            if (MoveToX(camera, anchorPoint_X[2].moveToPosX, ref currentVelocity_X, smoothTime_X, moveSpeedX, Time.fixedDeltaTime)) arrivalAnchorX = true;
        }

    }
    private void MoveToAnchorY()
    {
        if (arrivalAnchorY) return;
        if (isDownAnchor)
        {
            if (MoveToY(camera, anchorPoint_Y[1].moveToPosY, ref currentVelocity_Y, smoothTime_Y, moveSpeedY, Time.fixedDeltaTime)) arrivalAnchorY = true;
        }
        else if (MoveToY(camera, anchorPoint_Y[2].moveToPosY, ref currentVelocity_Y, smoothTime_Y, moveSpeedY, Time.fixedDeltaTime)) arrivalAnchorY = true;
    }
    private void CameraVerticalMove()
    {
        if (!arrivalAnchorY) return;
        if (player.position.y > camera.position.y + offSet.y)
        {
            if (player.position.y < anchorPoint_Y[2].PosY && !isDownAnchor) MoveToY(camera, anchorPoint_Y[2].moveToPosY, ref currentVelocity_Y, followSmoothTime_Y, 100, Time.fixedDeltaTime);
            else if (player.position.y > anchorPoint_Y[2].PosY && player.position.y < anchorPoint_Y[3].PosY)
            {
                isDownAnchor = false;
            }
            else if (player.position.y > anchorPoint_Y[3].PosY)
            {
                arrivalAnchorY = false;
                isDownAnchor = true;
            }
        }
        else
        {
            if (player.position.y > anchorPoint_Y[1].PosY && isDownAnchor) MoveToY(camera, anchorPoint_Y[1].moveToPosY, ref currentVelocity_Y, followSmoothTime_Y, 100, Time.fixedDeltaTime);//TransformMove.MoveX(camera, anchorPoint[1].moveToPosX, 100);
            else if (player.position.y > anchorPoint_Y[0].PosY && player.position.y < anchorPoint_Y[1].PosY)
            {
                isDownAnchor = true;
            }
            else if (player.position.y < anchorPoint_Y[0].PosY)
            {
                arrivalAnchorY = false;
                isDownAnchor = false;
            }
        }


    }

    private bool MoveToX(Transform transform, float targetX, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        targetX = Mathf.Max(targetX, CameraRange.gizmosCenter.x - CameraRange.size.x / 2 + cameraSize.size.x / 2);
        targetX = Mathf.Min(targetX, CameraRange.gizmosCenter.x + CameraRange.size.x / 2 - cameraSize.size.x / 2);
        return TransformMove.SmoothMoveX(transform, targetX, ref currentVelocity, smoothTime, maxSpeed, deltaTime);

    }

    private bool MoveToY(Transform transform, float targetY, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        targetY = Mathf.Max(targetY, CameraRange.gizmosCenter.y - CameraRange.size.y / 2 + cameraSize.size.y / 2);
        targetY = Mathf.Min(targetY, CameraRange.gizmosCenter.y + CameraRange.size.y / 2 - cameraSize.size.y / 2);
        return TransformMove.SmoothMoveY(transform, targetY, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
    }


    private void OnDrawGizmos()
    {
        if (closeAuxiliaryLine) return;
        Gizmos.color = Color.white;

        Vector2 x_1 = (Vector2)camera.position+ offSet - Vector2.right * deltaX_1;
        Vector2 x_2 = (Vector2)camera.position+ offSet - Vector2.right * deltaX_2;
        Gizmos.DrawLine(x_1 - Vector2.up * 3, x_1 + Vector2.up * 3);
        Gizmos.DrawLine(x_2 - Vector2.up * 3, x_2 + Vector2.up * 3);
        Gizmos.DrawLine(x_1 + Vector2.right * deltaX_1 * 2 - Vector2.up * 3, x_1 + Vector2.right * deltaX_1 * 2 + Vector2.up * 3);
        Gizmos.DrawLine(x_2 + Vector2.right * deltaX_2 * 2 - Vector2.up * 3, x_2 + Vector2.right * deltaX_2 * 2 + Vector2.up * 3);


        Vector2 y_1 = (Vector2)camera.position + offSet - Vector2.up * deltaY_1;
        Vector2 y_2 = (Vector2)camera.position + offSet - Vector2.up * deltaY_2;
        Gizmos.DrawLine(y_1 - Vector2.right * 3, y_1 + Vector2.right * 3);
        Gizmos.DrawLine(y_2 - Vector2.right * 5, y_2 + Vector2.right * 5);
        Gizmos.DrawLine(y_1 + Vector2.up * deltaY_1 * 2 - Vector2.right * 3, y_1 + Vector2.up * deltaY_1 * 2 + Vector2.right * 3);
        Gizmos.DrawLine(y_2 + Vector2.up * deltaY_2 * 2 - Vector2.right * 5, y_2 + Vector2.up * deltaY_2 * 2 + Vector2.right * 5);


    }

}

public static class TransformMove
{
    private const float checkSame = 0.0001f;
    private const float smoothCheckSame = 0.1f;

    /// <summary>
    /// speed请自行乘上deltatime
    /// </summary>
    public static bool MoveX(Transform transform, float targetX, float Speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), Speed);
        return Mathf.Abs(transform.position.x - targetX) <= checkSame;
    }

    public static bool SmoothMoveX(Transform transform, float targetX, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        return Mathf.Abs(transform.position.x - targetX) <= smoothCheckSame;
    }

    /// <summary>
    /// speed请自行乘上deltatime
    /// </summary>
    public static bool MoveY(Transform transform, float targetY, float Speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), Speed);
        return Mathf.Abs(transform.position.y - targetY) <= checkSame;
    }


    public static bool SmoothMoveY(Transform transform, float targetY, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        return Mathf.Abs(transform.position.y - targetY) <= smoothCheckSame;
    }



    /// <summary>
    /// speed请自行乘上deltatime
    /// </summary>
    public static bool MoveZ(Transform transform, float targetZ, float Speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.z, targetZ), Speed);
        return Mathf.Abs(transform.position.z - targetZ) <= checkSame;
    }
    public static bool MoveTowards(Transform transform, Vector3 target, float Speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Speed);
        return (transform.position - target).magnitude <= checkSame;
    }
    public static bool SmoothMoveTowards(Transform transform, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        return (transform.position - target).magnitude <= smoothCheckSame;
    }
    public static bool MoveTowards(Transform transform, Transform target, float Speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position, Speed);
        return (transform.position - target.position).magnitude <= checkSame;
    }


}
