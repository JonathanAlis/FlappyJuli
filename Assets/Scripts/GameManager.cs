using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    public UnityEvent Onstart;
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    public static GameManager Instance;
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    //public Text scoreText;
    enum PageState{
        None,
        Start,
        GameOver,
        CountDown
    }
    int score=0;
    bool gameOver=false;
    public bool GameOver{get{return gameOver;}}

void Awake(){
    Instance=this;
}
void SetPageState(PageState state){
    switch(state){
        case PageState.None:
        startPage.SetActive(false);
        gameOverPage.SetActive(false);
        countDownPage.SetActive(false);
        break;
        case PageState.Start:
        startPage.SetActive(true);
        gameOverPage.SetActive(false);
        countDownPage.SetActive(false);
        break;
        case PageState.GameOver:
        startPage.SetActive(false);
        gameOverPage.SetActive(true);
        countDownPage.SetActive(false);
        break;
        case PageState.CountDown:
        startPage.SetActive(false);
        gameOverPage.SetActive(false);
        countDownPage.SetActive(true);
        break;
    }
}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
