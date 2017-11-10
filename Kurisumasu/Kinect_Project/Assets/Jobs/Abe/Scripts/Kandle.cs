using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class Kandle : MonoBehaviour {

    public bool candleswitch = false;       //ロウソクに炎がついているかどうか
    public bool firestop = false;           //炎が２つ出ないようにするスイッチ           
    public GameObject candlefire;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cpos = transform.position;                                      //ロウソクのポジション

        if (Spone.trgFire == true)
        {
            Vector3 fpos = GameObject.Find("CampFire(Clone)").transform.position;   //炎のポジション

            fpos.x = BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].x;
            fpos.y = BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].y;
            fpos.z = 10;

            GameObject.Find("CampFire(Clone)").transform.position = fpos;

            if (Vector2.Distance(fpos, cpos) < 3)                                    //炎がロウソクに近づいた時
            {
                candleswitch = true;
            }
        }

        if(candleswitch == true && firestop == false)                                                     //ロウソクに火をつける処理
        {
            Instantiate(candlefire,new Vector3(cpos.x,cpos.y + 1.5f,10),Quaternion.identity);
            candleswitch = false;
            firestop = true;
        }
	}
}
