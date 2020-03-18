using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipespawn : MonoBehaviour
{
    public float destroyPosX=-20;
    public float speed=-20;
    public float spawnTime=2;
    float lastSpawnTime;
    public GameObject pipePrefab;
    List<GameObject> pipes;
    public float spawnPosX=10;
    public float maxY=5;
    public float minY=-5;
    GameObject gameOver;
    bool stop;
    // Start is called before the first frame update
    void Start()
    {
        gameOver=transform.GetChild(1).gameObject;
        lastSpawnTime=-spawnTime;        
        pipes= new List<GameObject>();
        gameOver.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*0,Screen.height/2,0))+new Vector3(0,0,10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop){
            if(Time.time-lastSpawnTime>spawnTime){
                //Debug.Log("New pipe");            
                //spawnPos.y=Random.RandomRange(minY,maxY);
                spawnPosX=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height/2,0)).x;

                GameObject me=Instantiate(pipePrefab,new Vector3(spawnPosX,Random.Range(minY,maxY),0),Quaternion.identity);
                pipes.Add(me);
                lastSpawnTime=Time.time;
            }
            //for each pipe...
            foreach(GameObject pipe in pipes){
                if (pipe!=null){
                    pipe.transform.position=new Vector3(pipe.transform.position.x + speed * Time.deltaTime,pipe.transform.position.y,0);
                    if (pipe.transform.position.x<destroyPosX){
                        Destroy(pipe);
                        break;
                    }
                }
            }
        }
    }
    void Stop(){
        stop=true;
    }
    void OnEnable()
    {
        Tap.OnDead+=Stop;    
    }
    void OnDisable()
    {
        Tap.OnDead-=Stop;    
    }
}
