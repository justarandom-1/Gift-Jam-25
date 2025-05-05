using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class open_menu : levelManager
{
    // Start is called before the first frame update
    private int maxLevel;

    private int displayedLevel;
    private GameObject currentDisplay;
    private Button leftButton;
    private Button rightButton;
    private AudioClip transitionSFX;
    new void Start()
    {
        base.Start();

        maxLevel = PlayerPrefs.GetInt("maxLevel", -1);

        if(3 <= maxLevel && maxLevel < 6){
            GameObject.Find("darkBeach").GetComponent<Image>().enabled = true;
        }else if(6 <= maxLevel && maxLevel < 8){
            GameObject.Find("NU").GetComponent<Image>().enabled = true;
        }else if(maxLevel == 8){
            GameObject.Find("End").GetComponent<Image>().enabled = true;
            audioSource.Stop();
            audioSource.clip = Resources.Load<AudioClip>("Music/Answer");
            audioSource.Play();
        }

        if(maxLevel == -1){
            GameObject.Find("LevelMenu").SetActive(false);
            return;
        }
        
        GameObject.Find("StartMenu").SetActive(false);

        int highestLevel = Mathf.Min(maxLevel + 1, 8);

        displayedLevel = PlayerPrefs.GetInt("displayedLevel", highestLevel);

        currentDisplay = GameObject.Find("levelPreview (" + displayedLevel + ")");

        GameObject leftSelector = GameObject.Find("leftSelector").transform.GetChild(0).gameObject;
        GameObject rightSelector = GameObject.Find("rightSelector").transform.GetChild(0).gameObject;

        leftSelector.GetComponent<Animator>().Play("buttonMoveAlt");
        rightSelector.GetComponent<Animator>().Play("buttonMoveAlt");

        leftButton = leftSelector.GetComponent<Button>();
        rightButton = rightSelector.GetComponent<Button>();

        leftButton.interactable = displayedLevel > 0;
        rightButton.interactable = displayedLevel < maxLevel + 1 && displayedLevel < 8;

        if(maxLevel != 8 && highestLevel != 4){
            GameObject highestLevelDisplay = GameObject.Find("levelPreview (" + highestLevel + ")");
            highestLevelDisplay.transform.GetChild(5).gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
            highestLevelDisplay.transform.GetChild(9).gameObject.GetComponent<TMP_Text>().text = "???";
        }
        currentDisplay.GetComponent<Animator>().Play("onDisplay");

        transitionSFX = Resources.Load<AudioClip>("transition");
    }
    public void slideMenu(int i = 1){

        if(!currentDisplay.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("onDisplay")){
            return;
        }

        currentDisplay.GetComponent<Animator>().Play("slideOut" + i);
        displayedLevel += i;
        currentDisplay = GameObject.Find("levelPreview (" + displayedLevel + ")");
        currentDisplay.GetComponent<Animator>().Play("slideIn" + i);

        leftButton.interactable = displayedLevel > 0;
        rightButton.interactable = displayedLevel < maxLevel + 1 && displayedLevel < 8;

        audioSource.PlayOneShot(transitionSFX, 0.7F);
    }

    new public void StartLevel(){
        if(wait == 0){
            wait = 1.3F;
            audioSource.PlayOneShot(Resources.Load<AudioClip>("select"));
            GetComponent<Animator>().Play("fadeToBlack");
        }
    }

    // Update is called once per frame
    new void Update()
    {
        if(Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.I))
        {
            PlayerPrefs.SetInt("maxLevel", 2);
            SceneManager.LoadScene("OpeningMenu");
        }
        if(Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.N))
        {
            PlayerPrefs.SetInt("maxLevel", 7);
            SceneManager.LoadScene("OpeningMenu");
        }
        if(Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.T))
        {
            PlayerPrefs.SetInt("maxLevel", -1);
            SceneManager.LoadScene("OpeningMenu");
        }


        if(wait > 0){
            wait -= Time.deltaTime;
            audioSource.volume = Mathf.Max(audioSource.volume - Time.deltaTime, 0);
            if(wait <= 0){
                PlayerPrefs.SetInt("displayedLevel", displayedLevel);
                SceneManager.LoadScene("Level" + displayedLevel);
            }
        }
    }
}
