using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNum : MonoBehaviour
{
    public int showhealth;
    public int showNote;
    public Text text1;
    public Text text2;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        showhealth = player.GetComponent<PlayerControl>().health;
        showNote = player.GetComponent<PlayerControl>().getNum;
        string health = "X " + showhealth;
        string note = showNote + " X";

        text1.text = health;
        text2.text = note;
    }
}
