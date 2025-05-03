using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 dir = new Vector2(0, 0);
    private Transform parent;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    public void fire(float angle)
    {
        state = 1;
        angle *= Mathf.Deg2Rad;
        transform.SetParent(null);
        lr.enabled = true;

        dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    public void retract()
    {
        state = 2;
    }

    void reload()
    {
        rb.velocity = new Vector2(0, 0);
        state = 0;
        transform.SetParent(parent);
        lr.enabled = false;
    }

    public int getState()
    {
        return state;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 0) return;

        rb.velocity = dir * speed;

        Vector2 distance = parent.position - transform.position;

        if(state == 2)
        {
            if(distance.magnitude < 0.25f)
            {
                reload();
                return;
            }

            dir = distance.normalized;

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, VectorToAngle(distance * -1));
        }
    }

    public static float VectorToAngle(Vector2 v)
    {
        v.Normalize();
        float a = Mathf.Asin(v.y) * Mathf.Rad2Deg;
        if(v.x < 0)
            a = 180 - a;
        return a;
    }
}
