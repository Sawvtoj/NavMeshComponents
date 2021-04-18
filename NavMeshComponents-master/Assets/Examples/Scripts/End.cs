using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    int endTime;

    void Awake()
    {
        endTime = 20000;
    }

    void Update()
    {
        if (endTime <= 0)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        else
        {
            endTime -= (int)Time.time;
            Debug.Log(endTime);
        }
    }
}
