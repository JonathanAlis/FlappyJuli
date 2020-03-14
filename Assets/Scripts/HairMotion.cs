using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairMotion : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private Sprite[] hairs;
    public GameObject head;
    Transform glasses;
    Rigidbody2D headRB;
    Vector2 acceleration,velocity,lastPos,lastVelocity;    
    public float weightSpeedX=1f;
    public float weightAccX=1f;
    public float weightSpeedY = 1f;
    public float weightAccY = 1f;
    public float windX, windY;
    public float glassesMove, glassPos;
    // Start is called before the first frame update
    int i;
    void Start()
    {
        glasses=head.transform.GetChild(2);
        headRB = head.GetComponent<Rigidbody2D>();
        lastVelocity = new Vector2(0, 0);
        lastPos = headRB.position;

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        hairs = Resources.LoadAll<Sprite>("juli_hair");
        i=16;
        windX=-3;
        windY=0;
        glassPos=glasses.localPosition.y;
    }
    Vector2 Rotate(Vector2 v,float angle){
        return new Vector2(
        v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle),
        v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle)
    );
    }

    void FixedUpdate()
    {
        // force=ma=-kx-> x=~-a;
        //wind, hair x=-v
        
        acceleration = (headRB.velocity - lastVelocity) / Time.fixedDeltaTime;
        acceleration=Rotate(acceleration,head.transform.rotation.z);
        lastVelocity = headRB.velocity;

        velocity=(headRB.position-lastPos)/Time.fixedDeltaTime;
        lastPos = headRB.position;
        
        float hairPosX = (windX-lastVelocity.x) - weightAccX * acceleration.x + (-weightSpeedX * lastVelocity.x);
        float hairPosY = (windY-lastVelocity.y) - weightAccY * acceleration.y + (-weightSpeedY * lastVelocity.y);
        int hx=(int)hairPosX/5;
        int hy=(int)hairPosY/8;
        if (hx>2) hx=2;
        if (hx<-2) hx=-2;
        if (hy>2) hy=2;
        if (hy<-2) hy=-2;
        
        //Debug.Log(new Vector2(windX-lastVelocity.x,windY-lastVelocity.y));
        //Debug.Log(new Vector2Int(hx,hy));
        //Debug.Log(velocity.x);
        //spriteR.sprite = hairs[i++ % hairs.Length];
        spriteR.sprite = hairs[hairPos2index(hx,hy)];

        //40 50 60 70 80
        glasses.transform.localPosition = new Vector2(glasses.transform.localPosition.x, glassesMove*hy+glassPos);   
    }
    int hairPos2index(int x, int y){
        
        //16= 0,0
        //0=0,-2; 1=0,-1; 2=0,2; 3=0,1
        //4=2,0; 5=1,0; 6=-2,0; 7=1,0
        //8=2,2;9=1,1;10=-2,2;11=-1,1
        //12=2,-2;13=1,-1;14=-2,-2;15=-1,-1
        if (x==0 && y==0) return 16;
        if (x==0 && y==-2) return 0;
        if (x==0 && y==-1) return 1;
        if (x==0 && y==2) return 2;
        if (x==0 && y==1) return 3;
        if (x==2 && y==0) return 4;
        if (x==1 && y==0) return 5;
        if (x==-2 && y==0) return 6;
        if (x==1 && y==0) return 7;
        if (x==2 && y==2) return 8;
        if (x==1 && y==1) return 9;
        if (x==-2 && y==2) return 10;
        if (x==-1 && y==1) return 11;
        if (x==2 && y==-2) return 12;
        if (x==1 && y==-1) return 13;
        if (x==-2 && y==-2) return 14;
        if (x==-1 && y==-1) return 15;
        return 16;
    }
}
