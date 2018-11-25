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
using System.Linq;

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

    /// <summary>
    /// 開始してもいいかどうか？
    /// </summary>
    private bool isStartOk;

    /// <summary>
    /// 現在遊んでいるじょうきょうの
    /// </summary>
    private MapField now_playing_map;

    /// <summary>
    /// 完了通知
    /// </summary>
    public Subject<MapField>CompleteObserver
    {
        get
        {
            return complete_create_subject;
        }
    }

    /// <summary>
    /// 準備が完了したかどうか？
    /// </summary>
    public bool IsCompleteStart
    {
        get { return isStartOk; }
        set{ isStartOk = value; }
    }

    /// <summary>
    /// 現在遊んでいるステージの内容を読み込む
    /// </summary>
    public  MapField NowPlayingStage
    {
        get
        {
            return map_fields[0];
        }
    }

    /// <summary>
    /// スコア相当のレベル
    /// </summary>
    private int lv;

    /// <summary>
    /// 最終的なスコア
    /// </summary>
    public int Lv
    {
        get
        {
            return lv;
        }
    }



	// Use this for initialization
	void Start ()
    {
        lv = 0;
        map_fields = new List<MapField>();
        complete_create_subject = new Subject<MapField>();
        complete_create_subject.Subscribe(_t => create_completed(_t));
        map_creater = new MappingCreate(this);
        
        for(int _i = 0;_i < 4;_i++)
        {
            map_creater.CreateNewMap();
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// マップの生成が完了した。
    /// </summary>
    /// <param name="_create_field">作成したマップ</param>
    private void create_completed(MapField _create_field)
    {
        map_fields.Add(_create_field);

        if(map_fields.Count > 1)
        {
            _create_field.ChangeBliend(false);
        }

        if(map_fields.Count >= MapMax)
        {
            isStartOk = true;
        }
    }

    /// <summary>
    /// 次のステージになるように
    /// </summary>
    public void NextStage()
    {
        var _first_map = map_fields.FirstOrDefault();
        map_fields.Remove(_first_map);
        _first_map.ChangeBliend(false);
        map_creater.create_map_info(_first_map);

        var _next_map = map_fields.FirstOrDefault();
        _next_map.ChangeBliend(true);
        lv++;
    }

    /// <summary>
    /// リセット
    /// </summary>
    public void Reset()
    {
        lv = 0;
        map_creater.Reset();
        for(int _i =0;_i < 5;_i++)
        {
            map_creater.create_map_info(map_fields[_i]);
        }

    }

}
