using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    Text scoretext;
    int highscore;
    // Start is called before the first frame update
    void Start()
    {       
        scoretext=transform.GetChild(0).GetChild(1).GetComponent<Text>();

        highscore=PlayerPrefs.GetInt("highscore",0);
        if(highscore>0){
            scoretext.text="High Score: "+highscore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }
}
