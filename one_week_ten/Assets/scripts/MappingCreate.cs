//===================================================================================================
/// <summary>
/// 概要：このクラスは、指定した値のマップを作成するためのものです。
///
/// 縦　横　の2分率で、表示を行い、それに対して、マップを作成するためのものです。
/// 
///　なお、1枚目を生成した時点で、バックグランド的に、生成を行っており、最大5枚くらいスタックします。
///　
/// 
///
///
/// MappingCreate.cs
/// </summary>
/// <Author>
/// 堀 明博 aki_hori@asobimo.com
///                                         後から他人が大幅な改変や流用を行った場合は2行目以降に記載
/// </Author>
//===================================================================================================

using UnityEngine;
using System.Collections;
using UniRx;

public class MappingCreate
{
	/// <summary>
    /// 最大の 1 辺範囲
    /// </summary>
	private readonly int MAX_WIDTH_BLOCK  = 10;

    /// <summary>
    /// 最初に作成を行うためのマップの数
    /// </summary>
    private readonly int FIRST_CREATE_MAPS = 5;

    /// <summary>
    /// マップのマネージャ
    /// </summary>
    private MapManager map_manager;


    /// <summary>
    /// ゴールまでの距離
    /// </summary>
    private int goal_length;

    /// <summary>
    /// 派生するラインの数
    /// </summary>
    private int line_count;

    /// <summary>
    /// ゴールまでの曲がる回数
    /// </summary>
    private int turn_count_to_goal;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="_manager"></param>
    public MappingCreate(MapManager _manager)
    {
        map_manager = _manager;
        goal_length = 7;　　　//値は適当
        line_count = 1;         //値は適当
        turn_count_to_goal = 1;//値は適当

        create_new_map();
    }

    /// <summary>
    /// マップを生成する
    /// </summary>
    private void create_new_map()
    {
        GameObject _map_field_object = new GameObject("MappingField");
        MapField _map_field = _map_field_object.AddComponent<MapField>();
        _map_field.CreateSettingMapFieldInfo(MAX_WIDTH_BLOCK, MAX_WIDTH_BLOCK);


        finish_map();
    }


    /// <summary>
    /// 具体的なマップを作成する。
    /// </summary>
    /// <param name="_map_field"></param>
    private void create_map_info(MapField _map_field)
    {
        int _goal_length = goal_length;
        int _turn_count_to_goal = turn_count_to_goal;
        int _line_count = line_count;

        if(_turn_count_to_goal <= 1)
        {
            first_compile_map(_map_field);
        }
        else
        {

        }
    }

    /// <summary>
    /// 曲がる数が１の時に生成を行うためのもの
    /// </summary>
    /// <param name="_map_field"></param>
    private void first_compile_map(MapField _map_field)
    {
       //構造体で、判断を行う機構が必要

    }

    /// <summary>
    /// 自由に勝手にマップを作成する。
    /// </summary>
    /// <param name="mapField"></param>
    /// <param name="_goal_length"></param>
    /// <param name="_turn_count"></param>
    /// <param name="_line_count"></param>
    private void free_compile_map(MapField mapField,int _goal_length,int _turn_count,int _line_count)
    {


    }


    /// <summary>
    /// マップを一つ生成し終わる度に呼び出される。
    /// それぞれのレベルが上がっていく
    /// </summary>
    private void finish_map()
    {
        goal_length += 1;
        turn_count_to_goal += 1;
        line_count += 1;
    }





    public Tuple<int, int> create_goal_pos()
    {
        return new Tuple<int, int>(Random.Range(0, MAX_WIDTH_BLOCK - 1), 0);
    }



    /// <summary>
    /// マップを作成を行うために、情報を保持するためのものです
    /// </summary>
    private class CreateBlocks
    {
        public bool IsActive;
        /// <summary>
        /// 上下左右が移動できるかどうか？
        /// </summary>
        public bool IsMoveRight;
        public bool IsMoveUp;
        public bool IsMoveLeft;
        public bool IsMoveDown;


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="_is_active"></param>
        /// <param name="_right"></param>
        /// <param name="_up"></param>
        /// <param name="_left"></param>
        /// <param name="_down"></param>
        public CreateBlocks(bool _is_active, bool _right, bool _up, bool _left, bool _down)
        {
            IsActive = _is_active;
            IsMoveRight = _right;
            IsMoveUp = _up;
            IsMoveLeft = _left;
            IsMoveDown = _down;
        }
        
    }

    public enum BlockType
    {
        None,   //そもそもいけない
        Normal, //普通のブロック
        Start,  //スタートブロック
        Goal,   //ゴールブロック

    }




}
