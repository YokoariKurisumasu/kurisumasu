using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class xmasBox : MonoBehaviour
{
    /*****************************************************************************/
    /*この中にあるのは随時調整する値　　　　　　　　　　　　　　　　　　　　　　 */

    //ケーキとプレゼントボックスの距離間の値（随時調整）
    private float cakeDesVal = 2.0f;
    //手をつかんだかの値
    private float grabVal = 1.0f;
    //クリスマスボックスが運べるようになるまでの距離の値
    private float delXmasBox = 2.0f;



    /*****************************************************************************/
    private bool grabFlg = false;
    private Vector3 xmasBoxPos;
    private Vector3 cakePos;
    private bool moveFlg = true;

    //手と手の二点間
    public float len = 0;
    //手とプレゼントボックスの二点間
    public float len2 = 0;
    //プレゼントボックスとケーキの二点間
    public float len3 = 0;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        XmasBoxMove();
    }

    void XmasBoxMove()
    {

        if(Spone.cakeFlg == true)
        {
            GameObject cake = GameObject.Find("cake");
            cakePos = cake.transform.position;

            len3 = ((xmasBoxPos.x - cakePos.x) * (xmasBoxPos.x - cakePos.x) +
                    (xmasBoxPos.y - cakePos.y) * (xmasBoxPos.y - cakePos.y));
        }
        xmasBoxPos = transform.position;

        //両手の座標で二点間を取る
        len = ((BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].x - BodySourceView.bodyPos[(int)Kinect.JointType.HandLeft].x) * (BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].x - BodySourceView.bodyPos[(int)Kinect.JointType.HandLeft].x) +
               (BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].y - BodySourceView.bodyPos[(int)Kinect.JointType.HandLeft].y) * (BodySourceView.bodyPos[(int)Kinect.JointType.HandRight].y - BodySourceView.bodyPos[(int)Kinect.JointType.HandLeft].y));

        //右手首とプレゼントボックスで二点間を取る
        len2 = (((BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].x - xmasBoxPos.x) * (BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].x - xmasBoxPos.x)) +
                ((BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].y - xmasBoxPos.y) * (BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].y - xmasBoxPos.y)));

        //右手と左手の座標が近づいたらグラブフラグをオンにする
        if (Mathf.Sqrt(len) <= 1.0f)
        {
            grabFlg = true;
        }
        else if (Mathf.Sqrt(len) >= 1.0f)
        {
            grabFlg = false;
        }

        //grabFlgがtrueで手の座標がプレゼントボックスの座標に近づいたらプレゼントボックスの座標を右手の座標に常に更新
        if (grabFlg == true && Mathf.Sqrt(len2) <= 2.0f)
        {
            //xmasBoxPos.x = BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].x;
            xmasBoxPos.y = BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].y;
            //xmasBoxPos.z = BodySourceView.bodyPos[(int)Kinect.JointType.WristRight].z;
        }

        if (Mathf.Sqrt(len3) >= cakeDesVal)
        {
            Destroy(gameObject);
        }

        transform.position = xmasBoxPos;


    }

}
