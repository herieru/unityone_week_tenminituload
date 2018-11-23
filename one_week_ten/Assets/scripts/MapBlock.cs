//===================================================================================================
/// <summary>
/// 概要：このクラスは、マップブロック単位での、構造体的な扱いのクラスです。
/// 基本的にこのクラスは入れられたマップ情報に対して参照をされるだけの存在です。
///
///
///
/// MapBlock.cs
/// </summary>
/// <Author>
/// 堀 明博 aki_hori@asobimo.com
///                                         後から他人が大幅な改変や流用を行った場合は2行目以降に記載
/// </Author>
//===================================================================================================

using UnityEngine;
using System.Collections;

public class MapBlock
{
    /// <summary>
    /// 横軸の位置
    /// </summary>
    private int x_pos;

    /// <summary>
    /// 縦軸の位置
    /// </summary>
    private int y_pos;

    /// <summary>
    /// このブロックが存在するかどうかの
    /// </summary>
    private bool isExistBlock;

	/// <summary>
	/// 表示を行うかどうかのもの
	/// </summary>
	private bool is_display;


    /// <summary>
    /// このブロックの位置
    /// </summary>
    public Vector2 PosInt
    {
        get
        {
            return new Vector2(x_pos, y_pos);
        }
    }

    /// <summary>
    /// このブロックが存在するか？
    /// </summary>
    public bool ExistBlock
    {
        get
        {
            return isExistBlock;
        }
    }
    

    /// <summary>
    /// このマップブロックのゲームオブジェクト
    /// </summary>
	private GameObject created_prefab;

	public MapBlock(GameObject _block_object,int _x_pos,int _y_pos)
    {
        isExistBlock = true;
        x_pos = _x_pos;
        y_pos = _y_pos;
    }


	/// <summary>
	/// ここに関連している内容の中身を消す
	/// </summary>
	public void BlockMapDestroy()
	{
		if(null != created_prefab)
		{
			GameObject.Destroy(created_prefab);
		}
	}
}
