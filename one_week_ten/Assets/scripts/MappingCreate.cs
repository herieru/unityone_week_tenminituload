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
    }

    /// <summary>
    /// マップを生成する
    /// </summary>
    private void create_map()
    {

    }
}
