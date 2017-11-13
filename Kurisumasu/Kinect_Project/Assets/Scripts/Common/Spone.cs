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
    public GameObject CakePrefub;

    //フラグ
    public bool trgTree = false;
    public bool trgSnowMan = false;
    public bool trgXmasBox = false;
    public bool trgBoxOpen = false;
    public bool trgFire = false;

    private float Fz = 5.0f;

    //インターバルのやつ
    public bool trgInterval = false;    //trueでポーズが反応しない
                                        //falseでポーズが反応する

    public float candyTime = 0;


    //炎が既にでているかどうかのスイッチ
    public static bool fireswitch = false;

    public static bool cakeFlg = false;

    private int count = 0;
    public bool ONE = true;
    public bool ONE2 = true;
    public bool ONE3 = true;
    public bool ONE4 = true;
    private float x = 0;
    private float y = 0;
    private float z = 0;
    private float cx = 0;
    private float cy = 0;
    private float cz = 0;

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

        //ツリー
        if (trgTree == true)
        {
            if (ONE4)
            {
                x = -2.0f;
                y = -2.5f;
                z = 5.0f;
                Instantiate(TreePrefub, new Vector3(x, y, z), Quaternion.Euler(-90, 0, 120));
                ONE4 = false;
            }
        }

        //雪だるま
        if (trgSnowMan == true)
        {
            if (ONE2)
            {
                x = 1.786f;
                y = -0.563f;
                z = 2.801f;
                Instantiate(SnowManPrefub, new Vector3(x, y, z), Quaternion.identity);
                ONE2 = false;
            }
        }

        //クリスマスボックスを作成
        if (trgXmasBox == true)
        {
            if (ONE3)
            {
                x = -1.807f;
                y = -0.602f;
                z = 2.907f;
                cx = -1.348f;
                cy = -0.946f;
                cz = 3.001f;
                Instantiate(XmasBoxPrefub, new Vector3(x, y, z), Quaternion.identity);
                Instantiate(CakePrefub, new Vector3(cx, cy, cz), Quaternion.identity);
                cakeFlg = true;
                ONE3 = false;
            }

        }

        if (trgBoxOpen == true)
        {

        }

        //炎
        if (trgFire == true)
        {
            if (ONE)
            {
                x = BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].x;
                y = BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].y;
                z = 10;
                Instantiate(FirePrefub, new Vector3(x, y, 10), Quaternion.identity);
                ONE = false;
            }
        }

    }
}
