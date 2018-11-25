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
    private bool is_exist_block;

    /// <summary>
    /// このマップブロックのゲームオブジェクト
    /// </summary>
	private GameObject created_prefab;


    /// <summary>
    /// 生成を行ったゲームオブジェクトのマテリアル。
    /// </summary>
    private Material prefab_material;

    /// <summary>
    /// ブロックのタイプ
    /// </summary>
    private MappingCreate.BlockType block_type;


    /// <summary>
    /// このブロックの位置を渡すためのモノ
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
            return is_exist_block;
        }
    }

    /// <summary>
    /// ゴールか？
    /// </summary>
    public bool IsGoal
    {
        get
        {
            return block_type == MappingCreate.BlockType.Goal;
        }
    }

    /// <summary>
    /// ブロックが存在するか？
    /// </summary>
    public bool IsBlockExits
    {
        get
        {
            return block_type != MappingCreate.BlockType.None;
        }
    }

    /// <summary>
    /// ブロックがない状態か？
    /// </summary>
    public bool IsBlockNon
    {
        get
        {
            return block_type == MappingCreate.BlockType.None;
        }
    }
    

    

	public MapBlock(GameObject _block_object,int _x_pos,int _y_pos)
    {
        created_prefab = _block_object;
        created_prefab.SetActive(false);
        is_exist_block = false;
        x_pos = _x_pos;
        y_pos = _y_pos;
        block_type = MappingCreate.BlockType.None;
        prefab_material = created_prefab.GetComponent<Renderer>().material;
        prefab_material.color = Color.green;
    }

    /// <summary>
    /// アクティブの状態を切り替える
    /// </summary>
    /// <param name="_active"></param>
    public void SettingActive(bool _active)
    {
        created_prefab.SetActive(_active);
        is_exist_block = _active;
        block_type = MappingCreate.BlockType.Normal;
        Debug.LogFormat("実際にActiveにしたもの：x_{0}.y_{1}", x_pos, y_pos);
    }

    /// <summary>
    /// ゴールとスタートを設定する時に使う
    /// 事前に、ブロックとして状態がなっている前提
    /// </summary>
    /// <param name="_block_type"></param>
    public void SettingBlockType(MappingCreate.BlockType _block_type)
    {

        Debug.LogWarningFormat("色がついたもの：x_{0}.y_{1}", x_pos, y_pos);

        switch (_block_type)
        {
            case MappingCreate.BlockType.Start:
                prefab_material.color = Color.yellow;
                block_type = MappingCreate.BlockType.Start;
                break;
            case MappingCreate.BlockType.Goal:
                prefab_material.color = Color.red;
                block_type = MappingCreate.BlockType.Goal;
                break;
        }
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
