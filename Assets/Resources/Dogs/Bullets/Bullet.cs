using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int power;
    [SerializeField] float speed;
    [SerializeField] float angle;
    [SerializeField] float curve;

    protected SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        
    }
}
