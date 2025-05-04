using UnityEngine;

public class FinalBoss : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected int mode = 0;

    [SerializeField] GameObject rings;

    protected int shots = 0;

    protected float fireAngle = 0;


    void selectNextMode()
    {
        int next;
        do{next = Random.Range(0, 3);}
        while(next == mode);

        mode = next;

        switch(mode)
        {
            case 0:
                fireRate = 1.5f;
                break;
        }

        shots = 0;
        timer = Random.Range(0, fireRate);
    }

    protected override void Attack()
    {
        switch(mode)
        {
            case 0:               
                if(shots >= 6)
                {
                    selectNextMode();
                    return;
                }
                Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
                audioSource.PlayOneShot(fireSFX);
                break;
            case 1:
                if(shots >= 5)
                {
                    selectNextMode();
                    return;
                }
                else
                {
                    timer = 0.5f;
                }

                Instantiate(rings, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.identity);

                audioSource.PlayOneShot(fireSFX);

                break;

            case 2:
                break;
            case 3:
                break;
        }

        // if(shots == 0)
        // {
        //     fireAngle = LevelManager.VectorToAngle(Player.instance.getPosition() - (Vector2)transform.position);
        // }

        // shots += 1;

        // Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
        
        // if(shots == 4)
        // {
        //     shots = 0;
        // }
        // else
        // {
        //     timer = 0.15f;
        // }

        shots++;
    }
}
