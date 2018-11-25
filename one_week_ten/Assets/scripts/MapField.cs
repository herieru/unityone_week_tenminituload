//===================================================================================================
/// <summary>
/// 概要：このクラスは、MapBlockを管理するためのものです。
/// このクラスはMappingCreateから作成され、情報の取得を行います。
///
///
///
/// MapField.cs
/// </summary>
/// <Author>
/// 堀 明博 aki_hori@asobimo.com
///                                         後から他人が大幅な改変や流用を行った場合は2行目以降に記載
/// </Author>
//===================================================================================================

using UnityEngine;
using System.Collections;

public class MapField:MonoBehaviour
{
    public class PosInfo
    {
        public int x;
        public int y;

        public PosInfo(int _x,int _y)
        {
            x = _x;
            y = _y;
        }
    }


    /// <summary>
    /// まっぷ情報の全部
    /// </summary>
    public MapBlock[,] map_blocks;

    //スタート位置と終了位置
    private PosInfo start_pos;
    private PosInfo end_pos;

    /// <summary>
    /// 開始位置
    /// </summary>
    public PosInfo StartPos
    {
        get
        {
            return start_pos;
        }
    }

    /// <summary>
    /// 終了位置
    /// </summary>
    public PosInfo EndPos
    {
        get
        {
            return end_pos;
        }
    }

    /// <summary>
    /// 実際の開始位置
    /// </summary>
    public Vector3 Start3DPos
    {
        get
        {
            var _start_pos = StartPos;
            return new Vector3(_start_pos.x * 2, 1, _start_pos.y);
            
        }
    }
    


	/// <summary>
	/// ブロックの作成を行う
	/// </summary>
	/// <param name="_width">横に配置されるブロック数</param>
	/// <param name="_height">縦に配置されるブロック数</param>
	public void CreateSettingMapFieldInfo(int _width,int _height)
	{
        map_blocks = new MapBlock[_width,_height];

        for(int _x = 0;_x < _width;_x++)
        {
            for(int _y = 0;_y < _height;_y++)
            {
                GameObject _block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _block.transform.position = new Vector3(_x * 2.0f, 0, _y * 2.0f);
                _block.transform.SetParent(this.transform);
                _block.name = string.Format("Block_{0}_{1}", _x,_y);
                MapBlock _map_block = new MapBlock(_block, _x, _y);
                map_blocks[_x, _y] = _map_block;
            }
        }
	}

    /// <summary>
    /// マップの情報を以下のように書き換える
    /// </summary>
    /// <param name="_mapping_map"></param>
    /// <param name="_width"></param>
    /// <param name="_height"></param>
    public void Setting_MapFieldInfo(bool[,] _mapping_map,int _width,int _height)
    {

        for(int _x = 0;_x < _width;_x++)
        {
            for(int _y = 0; _y < _height;_y++)
            {

                bool _map_flg = _mapping_map[_x,_y];
                if(_map_flg)
                {
                    Debug.LogFormat("以下の座標のものがActive：x_{0}.y_{1}", _x, _y);
                    map_blocks[_x, _y].SettingActive(true);
                }
            }
        }
    }

    /// <summary>
    /// 開始地点の設定
    /// </summary>
    /// <param name="_pos_x"></param>
    /// <param name="_pos_y"></param>
    public void SetStartPos(int _pos_x,int _pos_y)
    {
        map_blocks[_pos_x, _pos_y].SettingBlockType(MappingCreate.BlockType.Start);
        start_pos = new PosInfo(_pos_x, _pos_y);
    }


    /// <summary>
    /// 終了地点での設定
    /// </summary>
    /// <param name="_pos_x"></param>
    /// <param name="_pos_y"></param>
    public void SetGoalPos(int _pos_x ,int _pos_y)
    {
        map_blocks[_pos_x, _pos_y].SettingBlockType(MappingCreate.BlockType.Goal);
        end_pos = new PosInfo(_pos_x, _pos_y);
    }

    /// <summary>
    /// 非表示・表示を切り替える
    /// </summary>
    public void ChangeBliend(bool _switch)
    {
        this.gameObject.SetActive(_switch);
    }
}
