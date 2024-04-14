using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    [SerializeField]
    public float[] volume;
    [Header("获取的麦克风音量")]
    public float realVolume;//获取的麦克风音量
    private AudioClip[] micRecord;
    [Header("麦克风设备")]
    public string[] Devices;
    private void Awake()
    {
        Devices = Microphone.devices;
        if (Devices.Length > 0)
        {
            micRecord = new AudioClip[Devices.Length];
            volume = new float[Devices.Length];
            for (int i = 0; i < Devices.Length; i++)
            {
                if (Microphone.devices[i].IsNormalized())
                {
                    micRecord[i] = Microphone.Start(Devices[i], true, 999, 44100);
                }
            }
        }
        else
        {
            Debug.LogError("找不到麦克风");
        }
    }
    void Update()
    {
        if (Devices.Length > 0)
        {
            for (int i = 0; i < Devices.Length; i++)
            {
                volume[i] = GetMaxVolume(i);
                if (volume[i] != 0)
                {
                    realVolume = volume[i];
                }
            }
        }
    }

    //每一振处理那一帧接收的音频文件
    float GetMaxVolume(int x)
    {
        float maxVolume = 0f;
        //剪切音频
        float[] volumeData = new float[128];
        int offset = Mathf.Max(0, Microphone.GetPosition(Devices[x]) - 127);
        if (offset < 0)
        {
            return 0;
        }
        micRecord[x].GetData(volumeData, offset);

        for (int i = 0; i < 128; i++)
        {
            float tempMax = Mathf.Abs(volumeData[i]); // 使用绝对值来确保我们只关心音量大小，而不是正负  
            if (maxVolume < tempMax)
            {
                maxVolume = tempMax;
            }
        }

        // 将maxVolume从-1到1的范围映射到0到100的范围  
        return Mathf.RoundToInt(maxVolume * 100f); // 线性映射，并四舍五入到整数 
    }

  }
