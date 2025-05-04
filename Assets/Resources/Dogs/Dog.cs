using UnityEngine;

public class Dog : GameEntity
{
    [SerializeField] int power;
    protected int state = 0;
    protected Grapple grapple;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Grapple")){
            grapple = other.gameObject.GetComponent<Grapple>();
            if(grapple.getState() == 0)
                return;
            state = 2;
            Destroy(rb);
            transform.SetParent(other.gameObject.transform);
            grapple.retract();
        }

        if (other.gameObject.CompareTag("Player") && state != 2){
            Player.instance.hit(transform.position);
            Player.instance.takeDamage(power);
        }
    }

    public void capture()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 2)
        {
            if(grapple.getState() == 0)
            {
                capture();
            }
            return;

            // Vector3 pos = transform.localPosition;
            // transform.localPosition = new Vector3(incrementDistance(pos.x, 4 * Time.deltaTime), 
            //                                       incrementDistance(pos.y, 4 * Time.deltaTime),
            //                                       pos.z);
        }
    }

    float incrementDistance(float x, float i)
    {
        if(x > 0)
        {
            return Mathf.Max(x - i, 0);
        }
        return Mathf.Min(x + i, 0);
    }
}
