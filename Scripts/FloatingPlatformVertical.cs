using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformVertical : MonoBehaviour
{
    public Transform pos1, pos2;
    public float move_speed = 50f;
    public Transform StartPos;
    Vector3 nextpos;

    void Start()
    {
        nextpos = StartPos.position;
    }
    void Update()
    {
        if (transform.position == pos1.position)
            nextpos = pos2.position;
        if (transform.position == pos2.position)
            nextpos = pos1.position;
        transform.position = Vector3.MoveTowards(transform.position, nextpos, move_speed * Time.deltaTime);
    }
    private void ShowLine()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
