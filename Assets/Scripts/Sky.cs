using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public GameObject background;
    public GameObject ground;
    public GameObject starsPrefab;
    List<GameObject> starsCopies;
    GameObject comet;
    public float starsSpawnTime, cometSpawnTime;
    public float starsSpeed=-5;
    public float cometSpeedX=8;
    public float cometSpeedY=0;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        comet=transform.GetChild(0).gameObject;
        starsCopies= new List<GameObject>();
        GameObject s1=Instantiate(starsPrefab,new Vector3(-18,Random.Range(-0.1f,0.2f),0),Quaternion.identity);
        GameObject s2=Instantiate(starsPrefab,new Vector3(0,Random.Range(-0.1f,0.2f),0),Quaternion.identity);
        GameObject s3=Instantiate(starsPrefab,new Vector3(18,Random.Range(-0.1f,0.2f),0),Quaternion.identity);

        starsCopies.Add(s1);
        starsCopies.Add(s2);
        starsCopies.Add(s3);
        randomizeComet();

        stop=false;
    }
    void randomizeComet(){
        cometSpeedX=Random.Range(5.0f,11f);
        if (cometSpeedX<=0){
            cometSpeedX=0.5f;
        }
        cometSpeedY=Random.Range(-1f,1f);
        comet.transform.position=new Vector3(-70-Random.Range(0,70),0,0);
        float yOffset=comet.transform.position.x*cometSpeedY/cometSpeedX;
        comet.transform.position+=new Vector3(0,yOffset+Random.Range(-3f,3f),0);
        comet.transform.localScale=Vector3.one*Random.Range(0.2f,0.5f);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!stop){
            comet.transform.position=new Vector3(comet.transform.position.x + cometSpeedX * Time.deltaTime,comet.transform.position.y+cometSpeedY* Time.deltaTime,0);
            if (comet.transform.position.x>25){
                randomizeComet();
            }
            foreach(GameObject st in starsCopies){
                if (st!=null){
                    st.transform.position=new Vector3(st.transform.position.x + starsSpeed * Time.deltaTime,st.transform.position.y,0);
                    if (st.transform.position.x<-36){
                        st.transform.position=new Vector3(18,Random.Range(-0.1f,0.2f),0);
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
