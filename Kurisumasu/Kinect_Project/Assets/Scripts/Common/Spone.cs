using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;

public class Spone : MonoBehaviour
{

    //オブジェクト
    public GameObject TreePrefub;
    public GameObject SnowManPrefub;
    public GameObject XmasBoxPrefub;
    public GameObject FirePrefub;

    //フラグ
    public bool trgTree = false;
    public bool trgSnowMan = false;
    public bool trgXmasBox = false;
    public static bool trgFire = false;
    private float Fz = 5.0f;

    //インターバルのやつ
    public bool trgInterval = false;    //trueでポーズが反応しない
                                        //falseでポーズが反応する

    public float candyTime = 0;

    private int count = 0;
    public static bool ONE = true;
    private float x = 0;
    private float y = 0;
    private float z = 0;

    //n秒ごとに実行する
    public float sTime = 2;
    public float time;
    public float firetime = 0;
    public float shinetime = 0;

    // Use this for initialization
    void Start()
    {

        candyTime = 0;
        time = 2;
    }

    // Update is called once per frame
    void Update()
    {



        if (trgInterval == true)
        {

            time -= Time.deltaTime;
            if (time <= 0.0)
            {

                //タイム管理
                time = sTime;
                trgInterval = false;
            }

        }

        if (trgInterval == false)
        {

        }

        //炎の出現処理
        if(BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].z < BodySourceView.bodyPos[(int)Kinect.JointType.ShoulderRight].z - 1)
        {
            trgFire = true;
        }

        //ツリー
        if (trgTree == true)
        {
            if (ONE)
            {
                x = 2.0f;
                y = -2.5f;
                z = 5.0f;
                Instantiate(TreePrefub, new Vector3(x, y, z), Quaternion.Euler(-90,0,120));
                ONE = false;
            }
        }

        //雪だるま
        if(trgSnowMan == true)
        {
            if (ONE)
            {
                x = Random.Range(-120f, 120f);
                y = Random.Range(-100f, 100f);
                z = 149f;
                Instantiate(SnowManPrefub, new Vector3(x, y, z), Quaternion.identity);
                ONE = false;
            }
        }

        //クリスマスボックスを作成
        if(trgXmasBox == true)
        {
            if (ONE)
            {
                x = Random.Range(-120f, 120f);
                y = Random.Range(-100f, 100f);
                z = 149f;
                Instantiate(XmasBoxPrefub, new Vector3(x, y, z), Quaternion.identity);
                ONE = false;
            }

        }

        //炎
        if(trgFire == true)
        {
            if (ONE)
            {
                x = BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].x;
                y = BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].y;
                z = 10;
                Instantiate(FirePrefub,new Vector3(x,y,10),Quaternion.identity);
                ONE = false;
            }
        }

    }
}
