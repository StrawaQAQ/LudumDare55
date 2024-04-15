using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletChatPart : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void FlyBulletChat(string name, string word)
    {
        text.text ="<color=blue>" + name + ": </color>" + word;
    }
    public void FlyBulletChat(string word)
    {
        text.text =  word;
    }

}
