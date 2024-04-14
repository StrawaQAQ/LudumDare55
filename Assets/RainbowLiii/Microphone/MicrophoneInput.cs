using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    [SerializeField]
    public float[] volume;
    [Header("��ȡ����˷�����")]
    public float realVolume;//��ȡ����˷�����
    private AudioClip[] micRecord;
    [Header("��˷��豸")]
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
            Debug.LogError("�Ҳ�����˷�");
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

    //ÿһ������һ֡���յ���Ƶ�ļ�
    float GetMaxVolume(int x)
    {
        float maxVolume = 0f;
        //������Ƶ
        float[] volumeData = new float[128];
        int offset = Mathf.Max(0, Microphone.GetPosition(Devices[x]) - 127);
        if (offset < 0)
        {
            return 0;
        }
        micRecord[x].GetData(volumeData, offset);

        for (int i = 0; i < 128; i++)
        {
            float tempMax = Mathf.Abs(volumeData[i]); // ʹ�þ���ֵ��ȷ������ֻ����������С������������  
            if (maxVolume < tempMax)
            {
                maxVolume = tempMax;
            }
        }

        // ��maxVolume��-1��1�ķ�Χӳ�䵽0��100�ķ�Χ  
        return Mathf.RoundToInt(maxVolume * 100f); // ����ӳ�䣬���������뵽���� 
    }

  }
