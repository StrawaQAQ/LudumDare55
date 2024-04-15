using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{
    public static FollowController instance;
    private CameraSize cameraSize;
    public GameObject follower;
    public Transform player;
    private int index = 1;
    List<GameObject> allFollower = new();

    public static int followCoune=>instance.allFollower.Count;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        cameraSize = new CameraSize(Camera.main);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) AddFollower();
    }

    public void AddFollower()
    {
        GameObject newFollower= CachePool.Instantiate(follower,transform);
        FollowPlayer a= newFollower.GetComponent<FollowPlayer>();
        newFollower.transform.position=player.position+ Quaternion.Euler(0,0,Random.Range(0,360))*Vector2.one*Mathf.Sqrt(cameraSize.size.x * cameraSize.size.x + cameraSize.size.y * cameraSize.size.y);
        allFollower.Add(newFollower);
        a.InitData(player, index);
        index++;
    }

    private void UpdateRank(int index_)
    {
       
        if (allFollower.Count > 0)
        {
            if (index_ == allFollower.Count) allFollower.RemoveAt(index_);
            else {
                GameObject topObj = allFollower[allFollower.Count - 1];
                allFollower.RemoveAt(allFollower.Count);
                allFollower[index_] = topObj;
            }
            index--;

        }   

    }

    public void RemoveOneFollower()
    {
        if(allFollower.Count > 0)
        {
            GameObject a = allFollower[allFollower.Count];
            allFollower.RemoveAt(allFollower.Count);
            CachePool.Destroy(a);
        }
       
    }


}
