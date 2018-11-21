///<summary>
/// 概要：このクラスは、GameObjectについているMap生成機構です。
/// これはGameObjectに対してつけ、他所から、これに対して、命令を下すと、マップを生成してくれます。
/// 
///
/// <filename>
/// MapManager.cs
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
using UniRx;

public class MapManager : MonoBehaviour
{
    /// <summary>
    /// 一度に生成を行うための数の最大
    /// </summary>
    private readonly int MapMax = 5;


    /// <summary>
    /// マップの情報を保持します。
    /// </summary>
    private List<MapField> map_fields;

    /// <summary>
    /// マップの作成機構
    /// </summary>
    private MappingCreate map_creater;

    /// <summary>
    /// マップの生成が完了したら、通知が来る。
    /// </summary>
    private Subject<MapField> complete_create_subject;


	// Use this for initialization
	void Start ()
    {
        complete_create_subject.Subscribe(_t => create_completed(_t));
        map_creater = new MappingCreate(this);
        map_fields = new List<MapField>();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_create_field"></param>
    private void create_completed(MapField _create_field)
    {

    }
}
