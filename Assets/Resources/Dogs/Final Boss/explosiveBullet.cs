using UnityEngine;

public class explosiveBullet : Bullet
{
    [SerializeField] GameObject explosion;

    protected override void end()
    {
        Instantiate(explosion, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
        GetComponent<AudioSource>().Play();
        base.end();
    }

    // // Update is called once per frame
    // protected override void Update()
    // {
    //     base.Update();

        
    // }
}
