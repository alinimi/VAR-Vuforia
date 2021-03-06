﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    int TapCount;
    public float MaxDubbleTapTime;
    float NewTime;
    float tapTime;
    public GameObject menu;

    Color[] Colors = { Color.cyan, Color.green, Color.grey };
    int colorIndex = 0;
    public bool isMenu = false;

    GameObject selectedObject;
    ViewTrigger currentButton;
    // Use this for initialization
    void Start()
    {
        InitColors();
    }

    public void ChangeSelectedObject(ViewTrigger button, GameObject select)
    {
        selectedObject = select;
        if(button != null)
        {
            if (currentButton != null) currentButton.ResetButton();
            currentButton = button;

        }
    }


    public void InitColors()
    {
        Debug.Log("start color");
        if(selectedObject != null)
        {
            foreach (var child in selectedObject.transform.GetComponentsInChildren<Renderer>())
                child.material.color = Colors[colorIndex];

        }

    }

    public void EndMenu()
    {
        StartCoroutine(ExclusionZone());
    }

    IEnumerator ExclusionZone()
    {
        yield return new WaitForSeconds(2);
        isMenu = false;
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isMenu = !isMenu;
            menu.SetActive(isMenu);

        }
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                tapTime = Time.time + 0.3f;
            }
            else if (touch.phase == TouchPhase.Ended && Time.time <= tapTime)
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
                isMenu = !isMenu;
                menu.SetActive(isMenu);
                

                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            if(!isMenu && TapCount == 1)
            {
                changeColor();
            }
            TapCount = 0;
        }
    }

    public void changeColor()
    {
        if(selectedObject!= null)
        {
            Debug.Log("change color");
            colorIndex = (colorIndex + 1) % Colors.Length;
            foreach (var child in selectedObject.transform.GetComponentsInChildren<Renderer>())
                child.material.color = Colors[colorIndex];

        }
    }
}
