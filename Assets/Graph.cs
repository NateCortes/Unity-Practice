using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform point_prefab;
    [Range( 10, 100)]
    public int resolution = 10;
    Transform[] points;

    private void Awake()
    {
        Transform point;
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step, position = Vector3.right;
        points = new Transform[resolution];
        for( int i = 0; i < points.Length; i++)
        {
            point = Instantiate(point_prefab);
            position.x = ((i + .5f) * step - 1f);
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update()
    {
        Vector3 position;
        Transform point;
        for(int i=0; i < points.Length; i++)
        {
            point = points[i];
            position = point.position;
            position.y = Mathf.Sin( Mathf.PI * ( position.x + Time.time));
            point.localPosition = position;
        }
    }
}
