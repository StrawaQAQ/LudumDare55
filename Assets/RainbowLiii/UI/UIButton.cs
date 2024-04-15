using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToGameScene()
    {
        SceneManager.LoadScene(2);
    }
    public void ToTeaching()
    {
        SceneManager.LoadScene(1);
    }
    public void ToMain()
    {
        SceneManager.LoadScene(0);
    }
}
