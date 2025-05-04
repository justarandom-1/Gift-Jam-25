using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float range;


    private Vector2 dir = new Vector2(0, 0);
    private Transform parent;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private int state = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // void OnTriggerEnter2D (Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Dog") && state != 0){
    //         if(state == 1)
    //             retract();
    //     }
    // }

    public void fire(float angle)
    {
        state = 1;
        angle *= Mathf.Deg2Rad;
        transform.SetParent(null);
        lr.enabled = true;

        dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public void retract()
    {
        state = 2;
    }

    void reload()
    {
        rb.linearVelocity = new Vector2(0, 0);
        state = 0;
        transform.SetParent(parent);
        lr.enabled = false;

        transform.localEulerAngles = new Vector3(0, 0, -90);
        transform.localPosition = new Vector3(0, 0, 0);

    }

    public int getState()
    {
        return state;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 0) return;

        rb.linearVelocity = dir * speed;

        Vector2 distance = parent.position - transform.position;

        

        if(state == 1)
        {
            if(distance.magnitude > range || !spriteRenderer.isVisible)
            {
                retract();
                return;
            }
        }
        else
        {
            if(distance.magnitude < 0.25f)
            {
                reload();
                return;
            }

            dir = distance.normalized * 1.5f;

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, LevelManager.VectorToAngle(distance));
        }
    }
}
