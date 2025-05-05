using UnityEngine;

public class FinalBoss : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected int mode = 0;

    [SerializeField] GameObject rings;
    [SerializeField] GameObject explosives;

    [SerializeField] GameObject spawner;

    protected int shots = 0;

    protected float fireAngle = 0;


    void selectNextMode()
    {
        int next;
        do{next = Random.Range(0, 4);}
        while(next == mode);

        mode = next;

        switch(mode)
        {
            case 0:
                fireRate = 1.5f;
                break;
        }

        shots = 0;
        timer = fireRate*1.5f;
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
                if(shots >= 15)
                {
                    selectNextMode();
                    return;
                }
                else
                {
                    timer = 0.4f;
                }

                fireAngle = Random.Range(0, 360);

                Instantiate(rings, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));

                if(shots % 2 == 0)
                    audioSource.PlayOneShot(fireSFX);

                break;

            case 2:
                switch(shots)
                {
                    case 0:
                        fireAngle = Random.Range(0, 360);
                        timer = 1;
                        break;
                    case 1:
                        fireAngle += 30;
                        timer = 3;
                        break;
                    case 2:
                        selectNextMode();
                        return;
                }

                Instantiate(explosives, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
                audioSource.PlayOneShot(fireSFX);
                break;
            case 3:
                switch(shots)
                {
                    case 0:
                        timer = 10;
                        break;
                    case 1:
                        selectNextMode();
                        return;
                }

                fireAngle = LevelManager.VectorToAngle(Player.instance.getPosition() - (Vector2)transform.position);

                Instantiate(spawner, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
                audioSource.PlayOneShot(fireSFX);
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
