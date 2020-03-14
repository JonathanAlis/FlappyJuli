﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Tap : MonoBehaviour
{
    public float tapForce = 10f;
    public float tiltSmooth = 1.2f;
    public float wingSpeed = 50f;
    public float wingSpeed2 = 50f;
    

    public Vector3 startPos;
    Rigidbody2D rigidbody;
    Quaternion forwardRotation,downRotation,wingRotation1,wingRotation2;
    Transform wing1, wing2;
    float wingTimer, headTimer;
    Animator animator;
    float lastYvel,accY;
    public bool isAlive;
    float curVel,lastVel;
    public int score;
    public Text scoreText;
    public int deadJulis;
    bool gamover;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0,0,-90);
        forwardRotation = Quaternion.Euler(0,0,35);
        wingRotation1 = Quaternion.Euler(0,0,90);
        wingRotation2 = Quaternion.Euler(0,0,-90);
        wing1= transform.GetChild(0).transform;
        wing2= transform.GetChild(1).transform;
        animator=GetComponent<Animator>();
        lastYvel=0f;
        accY=0f;
        curVel=0f;
        lastYvel=0f;
        isAlive=true;
        scoreText = transform.GetChild(4).GetChild(0).GetComponent<Text>();
        deadJulis=PlayerPrefs.GetInt("deadjulis",0);
        transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4,Screen.height/2,0))+new Vector3(0,0,10);
        headTimer=0f;
        wingTimer=0f;
    }

    // Update is called once per frame
    void Update()
    {        
        lastVel= curVel;
        curVel=rigidbody.velocity.y;
        animator.SetFloat("yspd",rigidbody.velocity.y);
        animator.SetFloat("yacc",accY);
        
        if (isAlive){
            if (Input.GetMouseButtonDown(0)){
                transform.rotation = forwardRotation;
                rigidbody.velocity=Vector3.zero;
                rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
                animator.SetBool("flapped",true);
                headTimer=0f;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, invExp(tiltSmooth * headTimer));
            if (rigidbody.velocity.y>0){
                wingTimer+=Time.fixedDeltaTime;
                wing1.localRotation = Quaternion.Lerp(wing1.rotation, wingRotation1, invExp(wingSpeed * wingTimer));
                wing2.localRotation = Quaternion.Lerp(wing2.rotation, wingRotation2, invExp(wingSpeed2 * wingTimer));   
            }else{
                wingTimer=0;
                wing1.localRotation=Quaternion.Euler(0,0,0);
                wing2.localRotation=Quaternion.Euler(0,0,0);
                if (animator.GetBool("flapped")){
                    animator.SetBool("flapped",false);
                }
            }
            headTimer+=Time.fixedDeltaTime;       
            }else{
            //transform.position=new Vector3(transform.position.x -3 * Time.deltaTime,transform.position.y,0);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        animator.SetInteger("random",Random.Range(0,2));
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "ScoreZoneTag"){
            //register score event
            //play a sound
            animator.SetBool("scored",true);   
            score++;
            scoreText.text=""+score;    
            col.enabled=false;                 
        }
        if (col.gameObject.tag == "DeadZoneTag"){
            isAlive=false;
            animator.SetBool("lost",true);
            //rigidbody.simulated = false;
            //transform.rotation = Quaternion.identity;
            col.isTrigger=false;
        }
        if (col.gameObject.tag == "GameOverZoneTag"){
            Debug.Log("gameover");
            animator.SetBool("gamover",true);
            gamover=true;
        }
        if (gamover){
            if (score>PlayerPrefs.GetInt("highscore",0)){
                PlayerPrefs.SetInt("highscore",score);
            }
            deadJulis++;
            PlayerPrefs.SetInt("deadjulis",deadJulis);

            SceneManager.LoadScene("MainMenu");
            //rigidbody.simulated = false;
            //transform.rotation = Quaternion.identity;            
        }
    }
    float invExp(float x){
        return 1-Mathf.Exp(-x);
    }
}