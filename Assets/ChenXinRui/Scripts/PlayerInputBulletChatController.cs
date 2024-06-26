using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputBulletChatController : MonoBehaviour
{
    public TMP_InputField InputField;
    public Scrollbar scrollbar;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) SendBulletChat();
    }

    public void SendBulletChat()
    {
        InputField.text = InputField.text.Replace("\n", string.Empty);
        if (!string.IsNullOrEmpty(InputField.text))
        {
            BulletChatController.instance.AddBulletChat("Myself", InputField.text);
            StopCoroutine(UpdateScrollbar());
            StartCoroutine(UpdateScrollbar());
           
            InputField.ActivateInputField();
            InputField.text = string.Empty;
        }

    }
    public IEnumerator UpdateScrollbar()
    {
        scrollbar.value = 0;
        yield return new WaitForSeconds(0.05f);
        scrollbar.value = 0;

    }

}
