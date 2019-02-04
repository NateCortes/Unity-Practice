using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float degrees_per_hour = 30f, degrees_per_minute = 6f, degrees_per_second = 6f;
    public Transform hours_transform, minutes_trasnform, seconds_transform;
    public bool continuous = false;
    Rect gui_rect = new Rect(10, 10, 100, 40);

    void Awake()
    {
        DateTime time = DateTime.Now;
        Debug.Log(time);
        Debug.Log(Quaternion.Euler(0f, time.Hour * degrees_per_hour, 0f));
        Debug.Log(Quaternion.Euler(0f, time.Minute * degrees_per_minute, 0f));
        Debug.Log(Quaternion.Euler(0f, time.Second * degrees_per_second, 0f));
    }

    void OnGUI()
    {
        continuous = GUI.Toggle(gui_rect, continuous, "smooth update");
    }

    void Update()
    {
        if(!continuous)
        {
            update_discrete();
        }

        if(continuous)
        {
            update_continuous();
        }
    }

    void update_discrete()
    {
        DateTime time = DateTime.Now;
        hours_transform.localRotation = Quaternion.Euler(0f, time.Hour * degrees_per_hour, 0f);
        minutes_trasnform.localRotation = Quaternion.Euler(0f, time.Minute * degrees_per_minute, 0f);
        seconds_transform.localRotation = Quaternion.Euler(0f, time.Second * degrees_per_second, 0f);
    }

    void update_continuous()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hours_transform.localRotation = Quaternion.Euler(0f, (float) time.TotalHours * degrees_per_hour, 0f);
        minutes_trasnform.localRotation = Quaternion.Euler(0f, (float) time.TotalMinutes * degrees_per_minute, 0f);
        seconds_transform.localRotation = Quaternion.Euler(0f, (float) time.TotalSeconds * degrees_per_second, 0f);
    }
}
