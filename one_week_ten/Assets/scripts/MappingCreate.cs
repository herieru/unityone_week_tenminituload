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
        goal_length = MAX_WIDTH_BLOCK;　　　//値は適当
        line_count = 1;         //値は適当
        turn_count_to_goal = 1;//値は適当

        CreateNewMap();
    }

    /// <summary>
    /// マップを生成する GameObjectから作成を行う。
    /// </summary>
    public void CreateNewMap()
    {
        GameObject _map_field_object = new GameObject("MappingField");
        MapField _map_field = _map_field_object.AddComponent<MapField>();
        _map_field.CreateSettingMapFieldInfo(MAX_WIDTH_BLOCK, MAX_WIDTH_BLOCK);
        create_map_info(_map_field);
        finish_map();
        
    }


    /// <summary>
    /// 具体的なマップを作成する。
    /// </summary>
    /// <param name="_map_field"></param>
    public void create_map_info(MapField _map_field)
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
            free_compile_map(_map_field);
        }

        map_manager.CompleteObserver.OnNext(_map_field);
    }

    /// <summary>
    /// 曲がる数が１の時に生成を行うためのもの
    /// </summary>
    /// <param name="_map_field"></param>
    private void first_compile_map(MapField _map_field)
    {
        //構造体で、判断を行う機構が必要
        var _goal_point = create_goal_pos();
        int _goal_pos = _goal_point.Item2;
        int _width = _goal_point.Item1;
        bool[,] _map_logic = new bool[MAX_WIDTH_BLOCK, MAX_WIDTH_BLOCK];
        //直線で卸す
        ///for(int _i = _goal_pos;_i >= 0; _i--)
        for(int _i = 0;_i <= _goal_pos;_i++)
        {
            _map_logic[_width, _i] = true;
        }

        var _start_pos = create_start_pos(_goal_point);

        _map_field.Setting_MapFieldInfo(_map_logic, MAX_WIDTH_BLOCK, MAX_WIDTH_BLOCK);
        _map_field.SetStartPos(_start_pos.Item1, _start_pos.Item2);
        _map_field.SetGoalPos(_goal_point.Item1, _goal_point.Item2);
    }

    /// <summary>
    /// 自由に勝手にマップを作成する。
    /// </summary>
    /// <param name="_map_field"></param>
    private void free_compile_map(MapField _map_field)
    {
        //構造体で、判断を行う機構が必要
        var _goal_point = create_goal_pos();
        int _goal_pos = _goal_point.Item2;
        int _width = _goal_point.Item1;
        bool[,] _map_logic = new bool[MAX_WIDTH_BLOCK, MAX_WIDTH_BLOCK];

        var _start_pos = create_random_start_pos(_goal_pos);
        _map_logic = create_aisle(_start_pos, _goal_point);

        _map_field.Setting_MapFieldInfo(_map_logic, MAX_WIDTH_BLOCK, MAX_WIDTH_BLOCK);
        _map_field.SetStartPos(_start_pos.Item1, _start_pos.Item2);
        _map_field.SetGoalPos(_goal_point.Item1, _goal_point.Item2);
        finish_map();
    }

    /// <summary>
    /// ターン回数によってスタートの位置をずらす
    /// </summary>
    /// <returns></returns>
    private Tuple<int,int>create_random_start_pos(int _goal_pos)
    {
        bool _success = false;
        int _start_pos = 0;

        while(true)
        {
            _start_pos = Random.Range(0, MAX_WIDTH_BLOCK - 1);
            if(_start_pos != _goal_pos)
            {
                _success = true;
            }


            if(_success)
            {
                break;
            }
        }

        return new Tuple<int, int>(_start_pos, 0);
    }

    /// <summary>
    /// スタートとゴールを結んだうえで、ちょっとした寄り道を作成する。
    /// </summary>
    /// <param name="_start_pos"></param>
    /// <param name="_goal_pos"></param>
    /// <returns></returns>
    private bool[,]create_aisle(Tuple<int,int>_start_pos,Tuple<int,int>_goal_pos)
    {
        bool[,] _map_detaile = new bool[MAX_WIDTH_BLOCK,MAX_WIDTH_BLOCK];
        ///スタートと、ゴールの部分のベクトルとしての計算結果
        Tuple<int, int> _start_to_goal_vec = new Tuple<int, int>(_goal_pos.Item1 - _start_pos.Item1, _goal_pos.Item2 - _start_pos.Item2);

        int _move = _start_to_goal_vec.Item1 < 0?-1:1;

        Debug.LogFormat("aaa:{0}", Mathf.Abs(_start_to_goal_vec.Item1));
        //横軸に入れる
        for(int _x = 0;_x <= Mathf.Abs(_start_to_goal_vec.Item1);_x++)
        {
            int _x_pos = _start_pos.Item1 + _x * _move;
            Debug.LogFormat("横軸で変わった箇所：x:{0}  y:{1}", _x_pos, _start_pos.Item2);

            _map_detaile[_x_pos, _start_pos.Item2] = true;
        }

        _move = _start_to_goal_vec.Item2 < 0 ? -1 : 1;

        //縦軸に入れる
        for(int _y = 0;_y <= Mathf.Abs(_start_to_goal_vec.Item2);_y++)
        {
            int _y_pos = _start_pos.Item2 + _y * _move;
            Debug.LogFormat("縦軸で変わった箇所：x:{0}  y:{1}", _goal_pos.Item1, _y_pos);
            _map_detaile[_goal_pos.Item1, _y_pos] = true;
        }
        return _map_detaile;
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

    public void Reset()
    {
        goal_length = MAX_WIDTH_BLOCK;　　　//値は適当
        line_count = 1;         //値は適当
        turn_count_to_goal = 1;//値は適当
    }




    /// <summary>
    /// ゴールの設定の場所を返す。　必ずこれは奥になる
    /// </summary>
    /// <returns></returns>
    private Tuple<int, int> create_goal_pos()
    {
        return new Tuple<int, int>(Random.Range(0, MAX_WIDTH_BLOCK - 1), MAX_WIDTH_BLOCK - 1);
    }


    /// <summary>
    /// スタートの位置を決める 基準として、ゴールから垂直に降ろされたところに対して落ちていく。
    /// </summary>
    /// <param name="_goal_pos"></param>
    /// <returns></returns>
    private Tuple<int,int> create_start_pos(Tuple<int,int> _goal_pos)
    {
        return new Tuple<int, int>(_goal_pos.Item1, 0);
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
        /// <summary>
        /// 移動先でない
        /// </summary>
        None,   
        /// <summary>
        /// 普通の移動できるブロック
        /// </summary>
        Normal, 
        /// <summary>
        /// スタートに当たるブロック
        /// </summary>
        Start,  
        /// <summary>
        /// ゴールに当たるブロック
        /// </summary>
        Goal,

    }




}
