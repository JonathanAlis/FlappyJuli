using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadJuli : MonoBehaviour
{
    float xRadius, yRadius, f, startPhase;
    int a,b; //curve parameters
    Rigidbody2D rigidbody;
    float time2flap, total2flap, randomizeTimer;
    Quaternion wingRotation1, wingRotation2;
    Transform wing1, wing2;
    float scaleX;
    Vector2 center;
    Vector3 orScale;
    void Start()
    {
        rigidbody=GetComponent<Rigidbody2D>();

        randomizeValues();
        wingRotation1 = Quaternion.Euler(0,0,90);
        wingRotation2 = Quaternion.Euler(0,0,-90);
        wing1= transform.GetChild(0).transform;
        wing2= transform.GetChild(1).transform;
        scaleX=transform.localScale.x;
        time2flap=1.5f-Sigmoid(rigidbody.velocity.x-rigidbody.velocity.y);
        total2flap=time2flap;
        orScale=transform.localScale;
        
    }

    void randomizeValues(){
        xRadius=Random.Range(2.0f,5.0f);
        yRadius=Random.Range(2.0f,3.0f);
        b=Random.Range(3,5);
        a=1+Random.Range(b,10);
        f=Random.Range(0.3f,2.0f)*((2*Random.Range(0,2))-1);
        startPhase=Random.Range(0.0f,6.0f);//2pi approx
        //center=new Vector2(Random.Range(-4.0f,4.0f),Random.Range(-3.0f,3.0f));
        randomizeTimer=Random.Range(5.0f,10.0f);

    }
    // Update is called once per frame
    void Update()
    {

        
        float t=Time.time;
        float dt=Time.deltaTime;
        if(GetComponent<Renderer>().isVisible){
            
            //transform.position=new Vector2(curveX(a,b,f,t),curveY(a,b,f,t))+center;
            Vector2 newPos=new Vector2(curveX(a,b,f,t+startPhase)*xRadius,yRadius*curveY(a,b,f,t+startPhase))+center;
            Vector2 lastPos=new Vector2(curveX(a,b,f,t-dt+startPhase)*xRadius,yRadius*curveY(a,b,f,t-dt+startPhase))+center;
            
            Vector2 vel=(newPos-lastPos)/dt;
            rigidbody.velocity=vel;
            if (vel.x>0){
                transform.localScale=new Vector3(scaleX,transform.localScale.y,0);
            }else{
                transform.localScale=new Vector3(-scaleX,transform.localScale.y,0);
            }
            //Vector3 acc=accel(Time.time,Time.deltaTime,f,a,b,xRadius,yRadius);
            //rigidbody.AddForce(acc,ForceMode2D.Force);
        }else{
            rigidbody.velocity=new Vector2(-transform.position.x,-transform.position.y);
        }

        randomizeTimer-=dt;
        if (randomizeTimer<0){
            randomizeValues();
        }

        time2flap-=dt;

        if (time2flap<0){
            wing1.localRotation=Quaternion.Euler(0,0,0);
            wing2.localRotation=Quaternion.Euler(0,0,0);    
            time2flap=1.5f-Sigmoid(0.5f*rigidbody.velocity.x+0.5f*rigidbody.velocity.y);
            total2flap=time2flap;
            //Debug.Log(time2flap);
        }else{
            //wing1.localRotation = Quaternion.Lerp(wing1.rotation, Quaternion.Euler(0,0,0), Time.deltaTime);
            //wing2.localRotation = Quaternion.Lerp(wing2.rotation, Quaternion.Euler(0,0,0), Time.deltaTime);            
            //wing1.localRotation=Quaternion.Euler(0,0,0);
            //wing2.localRotation=Quaternion.Euler(0,0,0);    
            wing1.localRotation = Quaternion.Lerp(wing1.rotation, wingRotation1, 1-time2flap/total2flap);
            wing2.localRotation = Quaternion.Lerp(wing2.rotation, wingRotation2, 1-time2flap/total2flap);            
                       
        }
        float screenRatio=Screen.width*1.0f/Screen.height;
        
        if (screenRatio<1){
            transform.localScale=orScale*screenRatio;
            //transform.position=transform.position+new Vector3(0,0,10)+Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,3*Screen.height/4,0));
        } 
    }
    Vector3 accel(float t, float dt, float f, int a, int b,float maxX,float maxY){
        float tm1=t-dt;
        float tm2=tm1-dt;
        float posX=curveX(a,b,f,t);
        float posY=curveY(a,b,f,t);
        float posXm1=curveX(a,b,f,tm1);
        float posYm1=curveY(a,b,f,tm1);
        float posXm2=curveX(a,b,f,tm2);
        float posYm2=curveY(a,b,f,tm2);
        float velX=(posX-posXm1)/dt;
        float velY=(posY-posYm1)/dt;
        float velXm1=(posXm1-posXm2)/dt;
        float velYm1=(posYm1-posYm2)/dt;
        float accX=(velX-velXm1)/dt;
        float accY=(velY-velYm1)/dt;        
        return new Vector3(maxX*accX,maxY*accY,0);

    }
    float curveX(int a, int b, float f, float t){
        return ((a-b)*Mathf.Cos(t*f)+b*Mathf.Cos((1.0f*a/b-1)*(t*f)))/Mathf.Max(a-b,b);
    } 
    float curveY(int a, int b, float f, float t){
        return ((a-b)*Mathf.Sin(t*f)-b*Mathf.Sin((1.0f*a/b-1)*(t*f)))/Mathf.Max(a-b,b);
    }
    public static float Sigmoid(float value) {
        float k = Mathf.Exp(value);
        return k / (1.0f + k);
    }
}
