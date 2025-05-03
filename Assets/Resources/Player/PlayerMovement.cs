using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector2 MousePosition;
    [SerializeField] float speed;

    [SerializeField] float rotationSpeed;

    [SerializeField] Vector2 movementVector;

    // [SerializeField] Vector2 f;
    [SerializeField] float angle = Mathf.PI / 2;
    [SerializeField] float range;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Transform turret;
    private Grapple grapple;

    private Transform grappleTransform;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        turret = transform.GetChild(0);
        grappleTransform = turret.GetChild(0).GetChild(0);
        grapple = grappleTransform.gameObject.GetComponent<Grapple>();
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        rb.velocity = movementVector * speed;


        if(movementVector.x == 0 && movementVector.y == 0){
            // animator.SetBool("isMoving", false);
            audioSource.Stop();
        }
        else if(!audioSource.isPlaying)    
            audioSource.Play();

        // animator.SetBool("isMoving", true);

    }

    public void OnFire(InputValue value){
        if(grapple.getState() == 0 && value.Get<float>() == 1){
            grapple.fire(angle);            
        }

        if(grapple.getState() == 1 && (value.Get<float>() == 0 || (transform.position - grappleTransform.position).magnitude > range)){
            grapple.retract();  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(grapple.getState() == 0)
        {
            float a = rotationSpeed * Time.deltaTime;
            angle += a;
            turret.Rotate(new Vector3(0, 0, a));
        }

        // MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Vector2 distance = MousePosition - (Vector2)transform.position;

        // if(distance.magnitude > 0.1f)
        // {
        //     f = distance.normalized * speed;
        //     rb.AddForce(f);
        // }


        // if(Input.GetMouseButtonDown(0))
        // {
        // }
    }
}
