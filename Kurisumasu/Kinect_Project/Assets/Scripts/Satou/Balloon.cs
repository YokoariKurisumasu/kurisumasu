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
    public int[] trgBalloon = new int[13];
    /*
      0 = false
      1 = true    
    */

    //時間経過
    public float gameTime = 0;

    // Use this for initialization
    void Start()
    {
        gameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}