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
    public List<GameObject> allFollower = new();

    public static int followCount=>instance.allFollower.Count;

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


    public void AddFollower()
    {
        GameObject newFollower= CachePool.Instantiate(follower,transform);
        FollowPlayer a= newFollower.GetComponent<FollowPlayer>();
        newFollower.transform.position=player.position+ Quaternion.Euler(0,0,Random.Range(0,360))*Vector2.one*Mathf.Sqrt(cameraSize.size.x * cameraSize.size.x + cameraSize.size.y * cameraSize.size.y);
        allFollower.Add(newFollower);
        a.InitData(player, followCount+1);

    }

    public void UpdateRank(int index_)
    {
        if (allFollower.Count > 0)
        {
            if (index_ >= allFollower.Count-1)
            {
                CachePool.Destroy(allFollower[allFollower.Count - 1]);
                allFollower.RemoveAt(allFollower.Count - 1);
            }
            else {
                GameObject topObj = allFollower[allFollower.Count - 1];
                CachePool.Destroy(allFollower[index_]);
                allFollower.RemoveAt(allFollower.Count-1);
                allFollower[index_] = topObj;
                

            }
            for(int i=1;i<=allFollower.Count ;i++ )
            {
                allFollower[i - 1].GetComponent<FollowPlayer>().UpdateFollowData(i);
            }

        }

    }

    public bool RemoveOneFollower()
    {
        if (allFollower.Count > 0)
        {
            GameObject a = allFollower[allFollower.Count-1];
            allFollower.RemoveAt(allFollower.Count-1);
            CachePool.Destroy(a);
            return true;
        }
        return false;

    }


}
