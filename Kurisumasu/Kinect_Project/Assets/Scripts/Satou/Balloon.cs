using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Balloon : MonoBehaviour
{
    //各セリフを入れる
    public GameObject[] objBalloon;
    /*
      0 : 開幕セリフ01
      1 : 開幕セリフ02
      2 : 開幕セリフ03
      3 : ツリーを出せるようになる時のセリフ
      4 : ツリー出現時のセリフ
      5 : 雪だるまを出せるようになる時のセリフ
      6 : 雪だるま出現時のセリフ
      7 : プレゼントを出せるようになる時のセリフ
      8 : プレゼント出現時のセリフ
      9 : ケーキを出せるようになる時のセリフ
      10 : ケーキ出現時のセリフ
      11 : 炎を出せるようになる時のセリフ
      12 : ろうそくに着火時のセリフ
      13 : ゲーム終了時のセリフ
    */

    //各セリフのアクティブを切り替えるトリガー
    public int[] trgBalloon = new int[14];
    /*
      0 = false
      1 = true    
    */

    //for文に使用
    int i;

    // Use this for initialization
    void Start()
    {
        //スタート時にセリフトリガーを全て初期化(false)
        for (i = 0; i <= 13; i++) {
            trgBalloon[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //セリフをtrueにする処理
        trueBalloon();
        //セリフをfalseにする処理
        falseBalloon();
        
    }

    void trueBalloon()
    {
        //開幕のセリフトリガーがtrueのとき
        if (trgBalloon[0] == 1)
        {
            objBalloon[0].SetActive(true);
        }

        //開幕02のセリフトリガーがtrueのとき
        if (trgBalloon[1] == 1)
        {
            objBalloon[1].SetActive(true);
        }

        //開幕03のセリフトリガーがtrueのとき
        if (trgBalloon[2] == 1)
        {
            objBalloon[2].SetActive(true);
        }

        //ツリーを出せるセリフトリガーがtrueのとき
        if (trgBalloon[3] == 1)
        {
            objBalloon[3].SetActive(true);
        }

        //ツリーを出したセリフトリガーがtrueのとき
        if (trgBalloon[4] == 1)
        {
            objBalloon[4].SetActive(true);
        }

        //雪だるまを出せるセリフトリガーがtrueのとき
        if (trgBalloon[5] == 1)
        {
            objBalloon[5].SetActive(true);
        }

        //雪だるまを出したセリフトリガーがtrueのとき
        if (trgBalloon[6] == 1)
        {
            objBalloon[6].SetActive(true);
        }

        //プレゼントを出せるセリフトリガーがtrueのとき
        if (trgBalloon[7] == 1)
        {
            objBalloon[7].SetActive(true);
        }

        //プレゼントを出したセリフトリガーがtrueのとき
        if (trgBalloon[8] == 1)
        {
            objBalloon[8].SetActive(true);
        }

        //ケーキを出せるセリフトリガーがtrueのとき
        if (trgBalloon[9] == 1)
        {
            objBalloon[9].SetActive(true);
        }

        //ケーキを出したセリフトリガーがtrueのとき
        if (trgBalloon[10] == 1)
        {
            objBalloon[10].SetActive(true);
        }

        //炎を出せるセリフトリガーがtrueのとき
        if (trgBalloon[11] == 1)
        {
            objBalloon[11].SetActive(true);
        }

        //ろうそくに着火時のセリフトリガーがtrueのとき
        if (trgBalloon[12] == 1)
        {
            objBalloon[12].SetActive(true);
        }

        //ゲーム終了時のセリフトリガーがtrueのとき
        if (trgBalloon[13] == 1)
        {
            objBalloon[13].SetActive(true);
        }
    }

    void falseBalloon()
    {
        //開幕のセリフトリガーがfalseのとき
        if (trgBalloon[0] == 0)
        {
            objBalloon[0].SetActive(false);
        }

        //開幕02のセリフトリガーがfalseのとき
        if (trgBalloon[1] == 0)
        {
            objBalloon[1].SetActive(false);
        }

        //開幕03のセリフトリガーがfalseのとき
        if (trgBalloon[2] == 0)
        {
            objBalloon[2].SetActive(false);
        }

        //ツリーを出せるセリフトリガーがfalseのとき
        if (trgBalloon[3] == 0)
        {
            objBalloon[3].SetActive(false);
        }

        //ツリーを出したセリフトリガーがfalseのとき
        if (trgBalloon[4] == 0)
        {
            objBalloon[4].SetActive(false);
        }

        //雪だるまを出せるセリフトリガーがfalseのとき
        if (trgBalloon[5] == 0)
        {
            objBalloon[5].SetActive(false);
        }

        //雪だるまを出したセリフトリガーがfalseのとき
        if (trgBalloon[6] == 0)
        {
            objBalloon[6].SetActive(false);
        }

        //プレゼントを出せるセリフトリガーがfalseのとき
        if (trgBalloon[7] == 0)
        {
            objBalloon[7].SetActive(false);
        }

        //プレゼントを出したセリフトリガーがfalseのとき
        if (trgBalloon[8] == 0)
        {
            objBalloon[8].SetActive(false);
        }

        //ケーキを出せるセリフトリガーがfalseのとき
        if (trgBalloon[9] == 0)
        {
            objBalloon[9].SetActive(false);
        }

        //ケーキを出したセリフトリガーがfalseのとき
        if (trgBalloon[10] == 0)
        {
            objBalloon[10].SetActive(false);
        }

        //炎を出せるセリフトリガーがfalseのとき
        if (trgBalloon[11] == 0)
        {
            objBalloon[11].SetActive(false);
        }

        //ろうそくに着火時のセリフトリガーがfalseのとき
        if (trgBalloon[12] == 0)
        {
            objBalloon[12].SetActive(false);
        }

        //ゲーム終了時のセリフトリガーがfalseのとき
        if (trgBalloon[13] == 0)
        {
            objBalloon[13].SetActive(false);
        }

    } 
}