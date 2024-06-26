using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletChatController : MonoBehaviour
{
    public static BulletChatController instance;
    public Transform contentBody,flyWordBody;
    public GameObject bulletChatPartPrefab, flyWordPrefab;
    public AudioSource AudioSource;
    public TextMeshProUGUI followerCount;
    public PlayerInputBulletChatController PlayerInputBulletChatController;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

    }
    private void FixedUpdate()
    {
        followerCount.text = FollowController.followCount.ToString();
    }

    public void AddBulletChat(string name,string word) {

        GameObject a= Instantiate(bulletChatPartPrefab, contentBody);
        a.GetComponent<BulletChatPart>().FlyBulletChat(name, word);
        GameObject b = CachePool.Instantiate(flyWordPrefab, flyWordBody);
        b.GetComponent<FlyWord>().Fly(word);
        AudioSource.Play();

        StopCoroutine(PlayerInputBulletChatController.UpdateScrollbar());
        StartCoroutine(PlayerInputBulletChatController.UpdateScrollbar());

    }

    public void AddBulletChat(string word)
    {
        GameObject b = CachePool.Instantiate(flyWordPrefab, flyWordBody);
        b.GetComponent<FlyWord>().Fly(word);
        AudioSource.Play();
    }




}
