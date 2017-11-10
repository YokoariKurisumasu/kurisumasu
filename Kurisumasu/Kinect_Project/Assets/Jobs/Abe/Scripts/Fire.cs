using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class Fire : MonoBehaviour {


    public float fireTime = 5;          //炎が消えるまでの時間

    // Use this for initialization
    void Start () {
        fireTime = 5;
	}

    // Update is called once per frame
    void Update()
    {
        fireTime -= Time.deltaTime;
        if(fireTime < 5)                //５秒経ったら炎を消す
        {
            fireTime = 5;
            Spone.ONE = true;
            Destroy(gameObject);
        }
    }
}
