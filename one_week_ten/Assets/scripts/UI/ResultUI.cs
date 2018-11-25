///<summary>
/// 概要：
///
/// <filename>
/// ResultUI.cs
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

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private Text score_ui;


    /// <summary>
    /// スコアを入れる。
    /// </summary>
    /// <param name="_score"></param>
    public void SetResultScore(int _score)
    {
        score_ui.text= _score.ToString();
    }
}
