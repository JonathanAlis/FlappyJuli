using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeadJuli : MonoBehaviour
{
    int numdeads;
    public GameObject deadPrefab;
    List<GameObject> deads;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8,9, true);
        Physics2D.IgnoreLayerCollision(10,9, true);
        numdeads=PlayerPrefs.GetInt("deadjulis",0);
        Debug.Log(numdeads);
        deads= new List<GameObject>();

        for(int i=0;i<numdeads;i++){
            GameObject me;
            me=Instantiate(deadPrefab,new Vector3(Random.Range(-8.0f,8.0f),Random.Range(-2.0f,5.0f),0),Quaternion.identity);
            deads.Add(me);
        }
        //transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
