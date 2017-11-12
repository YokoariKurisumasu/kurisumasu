using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;

public class BodySourceView : MonoBehaviour
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject Title;
    public bool startFlg = false;
    private bool ONE = true;



    public static Vector3[] bodyPos = new Vector3[25];
    private ulong active = 0;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private short treeFlg = 0; //ツリー出現までのフラグ…　0＝なにもしていない、1＝両手を合わせた、2＝手の座標が頭を超える
    private bool xmasBoxFlg = false; //一旦ばんざいさせてから



    public float timeElapsed = 0;

    public float len = 0;
    public float len2 = 0;

    public float combo_time;



    public float a;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },

        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },

        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },

        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    public bool bodyTrg = false;

    void Start()
    {
        Title.SetActive(true);
        FindObjectOfType<Title>().circle.SetActive(true);
        FindObjectOfType<Title>().gage.SetActive(true);
        startFlg = false;

        treeFlg = 0;
        xmasBoxFlg = false;

    }

    void Update()
    {
        Debug.Log(startFlg);
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
                active = 0;
                bodyTrg = false;
            }
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                active = 0;
                continue;
            }

            if (body.IsTracked && (active == body.TrackingId || active == 0))
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                    //flg = true;
                }

                //範囲以内だったらアクティブそれ以外だったら外す
                if (bodyPos[(int)Kinect.JointType.SpineBase].x <= 5 &&
                   bodyPos[(int)Kinect.JointType.SpineBase].x >= -5 &&
                   bodyPos[(int)Kinect.JointType.SpineBase].z <= 25)
                {
                    bodyTrg = true;
                    active = body.TrackingId;
                }
                else
                {
                    bodyTrg = false;
                    active = 0;
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);

               
            }
            Debug.Log(active);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }

        //Debug.Log((bodyPos[(int)Kinect.JointType.SpineBase]));

        if (bodyTrg == false)
        {
            //startFlgがfalseならTitle関数のみ起動elseならmain部分の関数起動
            if (startFlg == false)
            {
                TitlePause();
            }

            if (startFlg == true)
            {
                if (ONE)
                {
                    //各タイトルに関連する部分のオブジェクトの表示管理
                    Title.SetActive(false);
                    FindObjectOfType<Title>().circle.SetActive(false);
                    FindObjectOfType<Title>().gage.SetActive(false);
                    ONE = false;

                }
                TreeCreate();
                SnowManCreate();
                XmasBoxCreate();
            }
        }

    }

    //体を認識してボーンを生成する
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.5f, 0.5f);

            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;


        }

        bodyTrg = true;

        return body;
    }

    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {

        //Transform jointObj;
        Vector3[] joypos = new Vector3[24];
        int joycnt = 0;
        Kinect.JointType[] joytype = new Kinect.JointType[24];


        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;

            if (_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }



            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);

            jointObj.localPosition += new Vector3((0.25f), 0, 0);
            jointObj.localPosition += new Vector3((jointObj.localPosition.x) * a, 0, 0);

            bodyPos[(int)jt] = jointObj.position;
            joycnt++;

            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if (targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, (GetVector3FromJoint(targetJoint.Value) + new Vector3((0.25f), 0, 0)) + new Vector3((GetVector3FromJoint(targetJoint.Value).x + 0.25f) * a, 0, 0));
                lr.SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }

            lr.enabled = false;

        }

    }

    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
            case Kinect.TrackingState.Tracked:
                return Color.green;

            case Kinect.TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

    //クリスマスツリー出現
    void TreeCreate()
    {
        //右手と左手の座標を二点間で代入
        len = ((bodyPos[(int)Kinect.JointType.HandRight].x- bodyPos[(int)Kinect.JointType.HandLeft].x)* (bodyPos[(int)Kinect.JointType.HandRight].x - bodyPos[(int)Kinect.JointType.HandLeft].x)+
                (bodyPos[(int)Kinect.JointType.HandRight].y - bodyPos[(int)Kinect.JointType.HandLeft].y)*( bodyPos[(int)Kinect.JointType.HandRight].y - bodyPos[(int)Kinect.JointType.HandLeft].y));
        Debug.Log(treeFlg);
        //右手と左手の座標が近い場合
        if (Mathf.Sqrt(len)<= 0.1f)
        {
            treeFlg = 1;
        }
        else
        {
           // treeFlg = 0;
        }

        //右手と左手が合わさった状態で両手の座標が頭を越したとき
        if (treeFlg == 1&&
            bodyPos[(int)Kinect.JointType.HandRight].y >= bodyPos[(int)Kinect.JointType.Head].y &&
            bodyPos[(int)Kinect.JointType.HandLeft].y >= bodyPos[(int)Kinect.JointType.Head].y|| 
            Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("ツリー出現");
            FindObjectOfType<Spone>().trgTree = true;
        }
        else
        {
            FindObjectOfType<Spone>().trgTree = false;
        }

    }

    //雪だるま
    void SnowManCreate()
    {
        if (bodyPos[(int)Kinect.JointType.ElbowRight].y >= bodyPos[(int)Kinect.JointType.ShoulderRight].y &&
           bodyPos[(int)Kinect.JointType.ElbowLeft].y >= bodyPos[(int)Kinect.JointType.ShoulderLeft].y)
        {
            FindObjectOfType<Spone>().trgSnowMan = true;
        }
        else
        {
            FindObjectOfType<Spone>().trgSnowMan = false;
        }

    }

    //クリスマスボックス
    void XmasBoxCreate()
    {
        if (xmasBoxFlg == false &&
              bodyPos[(int)Kinect.JointType.ElbowRight].y >= bodyPos[(int)Kinect.JointType.ShoulderRight].y &&
              bodyPos[(int)Kinect.JointType.ElbowLeft].y >= bodyPos[(int)Kinect.JointType.ShoulderLeft].y)
        {
            Debug.Log("肩");
            xmasBoxFlg = true;
        }
        //両手のｙ座標が腰より下で右手と左手が離れていたら
        if (xmasBoxFlg == true &&
            bodyPos[(int)Kinect.JointType.HandRight].y <= bodyPos[(int)Kinect.JointType.SpineMid].y &&
            bodyPos[(int)Kinect.JointType.HandLeft].y <= bodyPos[(int)Kinect.JointType.SpineMid].y &&
            bodyPos[(int)Kinect.JointType.HandRight].x >= bodyPos[(int)Kinect.JointType.SpineMid].x &&
            bodyPos[(int)Kinect.JointType.HandLeft].x <= bodyPos[(int)Kinect.JointType.SpineMid].x)
        {
            FindObjectOfType<Spone>().trgXmasBox = true;
            Debug.Log("下");
        }

    }

    //右手を上げたらゲームスタート
    void TitlePause()
    {
        if (bodyPos[(int)Kinect.JointType.HandRight].x >= bodyPos[(int)Kinect.JointType.SpineBase].x &&
          bodyPos[(int)Kinect.JointType.HandRight].y >= bodyPos[(int)Kinect.JointType.Head].y||
          Input.GetKey(KeyCode.Alpha6))
        {
            FindObjectOfType<Title>().titleTrg = true;
            //Debug.Log("いいぞ。");
        }
        else
        {
            FindObjectOfType<Title>().titleTrg = false;
        }

    }

}

