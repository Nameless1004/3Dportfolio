using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFps : MonoBehaviour
{
    public int targetFrameRate;
    public int displayRatePerSec;
    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(0, 0, 0, 1);
    public float width, height;
    private float elapsedTime = float.MaxValue;

    private int fps;

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
    private void Update()
    {
        float time = 1f / displayRatePerSec;
        if (elapsedTime > time)
        {
            elapsedTime = 0f;
            fps = (int)(1f / Time.deltaTime);
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
    private void OnGUI()
    {
        Rect position = new Rect(Screen.width-width, Screen.height - height, Screen.width, Screen.height);
        string text = $"{fps}FPS";
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = color;
        GUI.Label(position, text, style);
    }
}
