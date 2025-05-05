using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    // Start is called before the first frame update
    protected AudioSource audioSource;
    public float timeInterval;
    protected Object boneDemon;
    protected Object giantBoneDemon;
    protected Object wingedDemon;
    protected Object whiteWingedDemon;
    protected Object laserDemon;
    protected Object whiteLaserDemon;
    protected Object spiderDemon;
    protected Object golem;
    protected Object mage;
    protected Object bug;
    protected Object sapper;
    protected Object whiteSapper;
    protected Object ghost;
    protected GameObject deathText;
    public static int totalEnemies;
    public static int enemiesKilled;
    public static int stage;
    public static Color tint;
    public static bool keyHeld;
    public float wait;
    private float deathScreenLoad;
    public static int maxEnemies;

    [SerializeField] int maxEnemies_;
    public void Start()
    {
        totalEnemies = 0;
        enemiesKilled = 0;
        stage = 0;

        boneDemon = Resources.Load<GameObject>("Enemies/boneDemon/boneDemon");
        giantBoneDemon = Resources.Load<GameObject>("Enemies/boneDemon/giantBoneDemon");
        wingedDemon = Resources.Load<GameObject>("Enemies/wingedDemon/wingedDemon");
        whiteWingedDemon = Resources.Load<GameObject>("Enemies/wingedDemon/wingedDemonWHITE");
        laserDemon = Resources.Load<GameObject>("Enemies/laserDemon/laserDemon");
        whiteLaserDemon = Resources.Load<GameObject>("Enemies/laserDemon/laserDemonWHITE");
        spiderDemon = Resources.Load<GameObject>("Enemies/spiderDemon/spiderDemon");
        golem = Resources.Load<GameObject>("Enemies/golem/golem");
        mage = Resources.Load<GameObject>("Enemies/mage/mage");
        bug = Resources.Load<GameObject>("Enemies/bug/bug");
        sapper = Resources.Load<GameObject>("Enemies/sapper/sapper");
        whiteSapper = Resources.Load<GameObject>("Enemies/sapper/sapperWHITE");
        ghost = Resources.Load<GameObject>("Enemies/ghost/ghost");

        audioSource = GetComponent<AudioSource>();

        keyHeld = true;

        timeInterval = 0;

        deathScreenLoad = 0.5F;

        if(maxEnemies_ != 0){
            maxEnemies = maxEnemies_;
        }else{
            maxEnemies = 10000;
        }

        tint = Color.white;
    }

    // public virtual void StartLevel(){
    //     if(GameObject.Find("shield") != null){
    //         PlayerController.instance.setShield(GameObject.Find("shield"));
    //     }
    //     levelManager.stage = 2;
    // }

    public void Update(){
        if(stage == -1){
           this.deathScreen();
        }
        this.enemySpawn();
    }

    public void deathScreen(){
        deathScreenLoad -= Time.deltaTime;
        if(audioSource.isPlaying && deathText == null){
            GetComponent<Animator>().Play("cutToBlack");
            deathText = GameObject.Find("DeathText");
            deathText.GetComponent<Animator>().Play("fadeIn");
            audioSource.Pause();
        }
        else{
            if(!Input.anyKey){
                keyHeld = false;
            }
            if(Input.anyKey && !keyHeld && deathScreenLoad <= 0){
                SceneManager.LoadScene("OpeningMenu");
            }
        }
    }
    public virtual void enemySpawn(){
        ;
    }
}