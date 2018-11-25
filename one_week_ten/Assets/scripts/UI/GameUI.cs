///<summary>
/// 概要：GameのUIを操作するためのものです。
/// 
///
/// <filename>
/// GameUI.cs
/// </filename>
///
/// <creatername>
/// 作成者：堀　明博
/// </creatername>
/// 
/// <address>
/// mailladdress:herie270714@gmail.com
/// </address>
///</summary>


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Text time;

    [SerializeField]
    private Text score;

    /// <summary>
    /// 時間の表示を行う。
    /// </summary>
    /// <param name="_time">設定したい時間</param>
    public void SetTime(int _time)
    {
        time.text = _time.ToString();
    }

    public void SetScore(int _score)
    {
        score.text = _score.ToString();
    }
}
