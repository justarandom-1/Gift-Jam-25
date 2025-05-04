using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int power;
    [SerializeField] protected float speed;
    [SerializeField] protected float curve;

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    [SerializeField] protected float curAngle;
    [SerializeField] protected Vector2 direction;
    private Vector3 initialPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        curAngle = transform.rotation.eulerAngles.z;
        initialPos = transform.position;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            Player.instance.takeDamage(power);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, curve * Time.deltaTime));

        curAngle = transform.rotation.eulerAngles.z;        

        direction = new Vector2(Mathf.Cos(curAngle * Mathf.Deg2Rad), Mathf.Sin(curAngle * Mathf.Deg2Rad));

        rb.linearVelocity = direction * speed;
        
        if((transform.position - initialPos).magnitude > 15)
            Destroy(gameObject);
    }
}
