using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform point_prefab;
    [Range( 10, 100)]
    public int resolution = 10;
    Transform[] points;

    public enum GraphFunctionName {
        Sine, 
        MultiSine,
        ExtendedSine,
        Sine2D,
        MultiSine2D,
        TripSine2D,
        Ripple,
        Cylinder,
        WobbleCylinder,
        Sphere,
        WobbleSphere,
        Torus, 
        WobbleTorus
    }
    public GraphFunctionName function = 0;
    static GraphFunction[] functions = {
        SineFunction,
        MultiSineFunction,
        TripSineFunction,
        Sine2DFunction,
        MultiSine2DFunction,
        TripSine2DFunction,
        Ripple, 
        Cylinder,
        WobbleCylinder,
        Sphere,
        WobbleSphere,
        Torus,
        WobbleTorus
    };

    private void OnGUI()
    {
        function = ( GraphFunctionName) GUI.SelectionGrid(new Rect(10, 10, 500, 50), ( int)function, GraphFunctionName.GetNames( typeof( GraphFunctionName)), functions.Length);
    }

    private void Awake()
    {
        Transform point;
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        points = new Transform[resolution * resolution];
        for( int i = 0; i < points.Length; i++)
        {
            point = Instantiate(point_prefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update()
    {
        float time = Time.time;
        GraphFunction f = functions[ (int)function];

        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for( int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, time);
            }
        }
    }

    const float pi = Mathf.PI;

    static Vector3 SineFunction(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Mathf.Sin(pi * (u + t));
        p.z = v;
        return p;
    }

    static Vector3 Sine2DFunction(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Mathf.Sin(pi * (u + v + t));
        p.z = v;
        return p;
    }

    static Vector3 MultiSineFunction(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        p.y = Mathf.Sin(pi * (u + t));
        p.y += Mathf.Sin(2f * pi * (u + t)) / 2f;
        p.y *= (2f / 3f);
        return p;
    }

    static Vector3 MultiSine2DFunction(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        p.y = Mathf.Sin(pi * (u + t));
        p.y += Mathf.Sin(pi * (v + t));
        p.y *= 0.5f;
        return p;
    }

    static Vector3 TripSineFunction(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        p.y = Mathf.Sin(pi * (u + t));
        p.y += Mathf.Sin(2f * pi * (u + 2*t)) / 2f;
        p.y *= (2f / 3f);
        return p;
    }

    static Vector3 TripSine2DFunction(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        p.y = 4f * Mathf.Sin(pi * (u + v + 0.5f*t));
        p.y += Mathf.Sin(pi * (u + t));
        p.y += 0.5f * Mathf.Sin(2f * pi * (u + 2 * t));
        p.y *= 1f / 5.5f;
        return p;
    }

    static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.z = v;
        float d = Mathf.Sqrt(u * u + v * v);
        p.y = Mathf.Sin(pi * ( 4f * d - t));
        p.y /= 1f + 10f * d;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        p.x = Mathf.Sin(pi * u);
        p.y = v;
        p.z = Mathf.Cos(pi * u);

        return p;
    }

    static Vector3 WobbleCylinder(float u, float v, float t)
    {
        float r = 1f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * .2f;
        Vector3 p;
        p.x = r * Mathf.Sin( pi * u);
        p.y = v;
        p.z = r * Mathf.Cos( pi * u);

        return p;
    }

    static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = Mathf.Cos(pi * 0.5f * v);
        
        p.x = r * Mathf.Sin(pi * u);
        p.y = Mathf.Sin(pi * 0.5f * v);
        p.z = r * Mathf.Cos(pi * u);

        return p;
    }

    static Vector3 WobbleSphere(float u, float v, float t)
    {
        Vector3 p;

        float r = 0.8f + Mathf.Sin( pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);
        
        p.x = s * Mathf.Sin(pi * u);
        p.y = r * Mathf.Sin(pi * 0.5f * v);
        p.z = s * Mathf.Cos(pi * u);

        return p;
    }

    static Vector3 Torus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 1f;
        float r2 = 0.5f;
        float s = r2 * Mathf.Cos(pi * v)  + r1;

        p.x = s * Mathf.Sin(pi * u);
        p.y = r2 * Mathf.Sin(pi * v);
        p.z = s * Mathf.Cos(pi * u);

        return p;
    }

    static Vector3 WobbleTorus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin( pi * ( 6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(pi * v) + r1;

        p.x = s * Mathf.Sin(pi * u);
        p.y = r2 * Mathf.Sin(pi * v);
        p.z = s * Mathf.Cos(pi * u);

        return p;
    }
}
