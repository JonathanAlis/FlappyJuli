using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuJuli : MonoBehaviour
{
    float startPosY;
    Rigidbody2D juli;
    Transform wing1,wing2;
    Quaternion wingRotation1,wingRotation2;
    float timer, aniTimer;
    public float spd=25;
    Vector3 orScale;
    Animator faceAni;
    // Start is called before the first frame update
    void Start()
    {
        startPosY=transform.position.y;
        juli=GetComponent<Rigidbody2D>();
        wingRotation1 = Quaternion.Euler(0,0,90);
        wingRotation2 = Quaternion.Euler(0,0,-90);
        wing1= transform.GetChild(0).transform;
        wing2= transform.GetChild(1).transform;
        timer=0f;
        aniTimer=0f;
        orScale=transform.localScale;
        var width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        
        float screenRatio=Screen.width*1.0f/Screen.height;
        if (screenRatio<1){
            transform.localScale=orScale*screenRatio;
        }
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height/2,0)));
        faceAni=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        aniTimer+=Time.deltaTime;
        if (aniTimer>30){
            int choice=Random.Range(0,3);
            switch(choice)
            {
                case 0:
                faceAni.SetBool("gum",true);
                faceAni.SetBool("bored",false);
                faceAni.SetBool("thinking",false);
                break;
                case 1:
                faceAni.SetBool("gum",false);
                faceAni.SetBool("bored",true);
                faceAni.SetBool("thinking",false);
                break;
                case 2:
                faceAni.SetBool("gum",false);
                faceAni.SetBool("bored",false);
                faceAni.SetBool("thinking",true);
                break;
            }
            aniTimer=20f;
        }
        //Debug.Log(transform.position.y);
        if (transform.position.y<=startPosY){
            timer=1/spd;
            juli.velocity=Vector2.up * 5;                                    
        }
        if (timer<1/spd && timer>0){
            wing1.localRotation = Quaternion.Lerp(wing1.rotation, wingRotation1, spd*timer);
            wing2.localRotation = Quaternion.Lerp(wing2.rotation, wingRotation2, spd*timer);
        }else{
            wing1.localRotation=Quaternion.Lerp(wing1.rotation, Quaternion.Euler(0,0,0), -spd*timer);
            //wing1.localRotation=Quaternion.Euler(0,0,0);
            //wing2.localRotation=Quaternion.Euler(0,0,0);
            wing2.localRotation=Quaternion.Lerp(wing2.rotation, Quaternion.Euler(0,0,0), -spd*timer);
        }
        timer-=Time.fixedDeltaTime;

        transform.position=new Vector3(0,transform.position.y,10)+Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4,Screen.height/2,0));
        float screenRatio=Screen.width*1.0f/Screen.height;
        if (screenRatio<1){
            transform.localScale=orScale*screenRatio;
            //transform.position=transform.position+new Vector3(0,0,10)+Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,3*Screen.height/4,0));
        }
    }
}
