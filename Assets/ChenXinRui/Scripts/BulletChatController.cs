using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletChatController : MonoBehaviour
{
    public static BulletChatController instance;
    public Transform contentBody,flyWordBody;
    public GameObject bulletChatPartPrefab, flyWordPrefab;
  
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }

    public void AddBulletChat(string name,string word) {

        GameObject a= Instantiate(bulletChatPartPrefab, contentBody);
        a.GetComponent<BulletChatPart>().FlyBulletChat(name, word);
        GameObject b = CachePool.Instantiate(flyWordPrefab, flyWordBody);
        b.GetComponent<FlyWord>().Fly(word);
       
    }




}
