using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class open_menu : LevelManager
{
    // Start is called before the first frame update
    // private int displayedLevel;
    // private GameObject currentDisplay;
    // private Button leftButton;
    // private Button rightButton;
    // private AudioClip transitionSFX;
    protected override void Start()
    {
        base.Start();

        maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

        // if(3 <= maxLevel && maxLevel < 6){
        //     GameObject.Find("darkBeach").GetComponent<Image>().enabled = true;
        // }else if(6 <= maxLevel && maxLevel < 8){
        //     GameObject.Find("NU").GetComponent<Image>().enabled = true;
        // }else if(maxLevel == 8){
        //     GameObject.Find("End").GetComponent<Image>().enabled = true;
        //     audioSource.Stop();
        //     audioSource.clip = Resources.Load<AudioClip>("Music/Answer");
        //     audioSource.Play();
        // }

        if(maxLevel == 0){
            for(int i = maxLevel + 1; i <= 5; i++)
            {
                GameObject.Find("Level" + i.ToString()).SetActive(false);
            }
            return;
        }
        
        GameObject.Find("StartMenu").SetActive(false);

        for(int i = 1; i <= 5; i++)
        {
            if(i >= maxLevel + 1)
                GameObject.Find("Level" + i.ToString()).GetComponent<Button>().interactable = false;
            GameObject.Find("Level" + i.ToString()).GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        }


    }
    // public void slideMenu(int i = 1){

    //     if(!currentDisplay.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("onDisplay")){
    //         return;
    //     }

    //     currentDisplay.GetComponent<Animator>().Play("slideOut" + i);
    //     displayedLevel += i;
    //     currentDisplay = GameObject.Find("levelPreview (" + displayedLevel + ")");
    //     currentDisplay.GetComponent<Animator>().Play("slideIn" + i);

    //     leftButton.interactable = displayedLevel > 0;
    //     rightButton.interactable = displayedLevel < maxLevel + 1 && displayedLevel < 8;

    //     audioSource.PlayOneShot(transitionSFX, 0.7F);
    // }

    public void StartLevel(string level){
        
        PlayerPrefs.SetInt("maxLevel", Mathf.Max(maxLevel, 1));


        nextScene = level;
        EndLevel();
        
        // if(wait == 0){
        //     wait = 1.3F;
        //     audioSource.PlayOneShot(Resources.Load<AudioClip>("select"));
        //     GetComponent<Animator>().Play("fadeToBlack");
        // }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(state == 2)
        {
            audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime, 0);

            if(audioSource.volume == 0)
            {
                SceneManager.LoadScene(nextScene);
            }
            return;
        }
    }
}
