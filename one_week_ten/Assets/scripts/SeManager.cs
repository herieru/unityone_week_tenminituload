///<summary>
/// 概要：音を鳴らすための素材を保持するためのモノ
///
/// <filename>
/// SeManager.cs
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

public class SeManager : MonoBehaviour
{
    [SerializeField]
    List<AudioSource> source = new List<AudioSource>();

    /// <summary>
    /// 指定の音楽を再生する
    /// </summary>
    /// <param name="_no"></param>
    public void PlaySe(int _no)
    {
        if(source.Count <= _no)
        {
            Debug.LogErrorFormat("not_setting_audio_source :no{0}",_no);
            return;
        }


    }

  
}
