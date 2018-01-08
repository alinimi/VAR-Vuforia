﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    int TapCount;
    public float MaxDubbleTapTime;
    float NewTime;
    public GameObject menu;

    Color[] Colors = { Color.cyan, Color.green, Color.grey };
    int colorIndex = 0;
    // Use this for initialization
    void Start()
    {
        InitColors();
    }

    public void InitColors()
    {
        Debug.Log("start color");

        foreach (var child in transform.GetComponentsInChildren<Renderer>())
            child.material.color = Colors[colorIndex];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {

                NewTime = Time.time + MaxDubbleTapTime;
            }
            else if (TapCount == 2 && Time.time <= NewTime)
            {

                //Whatever you want after a dubble tap    
                print("Dubble tap");

                menu.SetActive(true);

                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            if(TapCount == 1)
            {
                changeColor();
            }
            TapCount = 0;
        }
    }

    public void changeColor()
    {
        Debug.Log("change color");
        colorIndex = (colorIndex + 1) % Colors.Length;
        foreach(var child in transform.GetComponentsInChildren<Renderer>())
            child.material.color = Colors[colorIndex];
    }
}
