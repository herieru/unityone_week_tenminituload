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
    public MapBlock[,] map_blocks;

	/// <summary>
	/// 
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

    public void Setting_MapFieldInfo(bool[] _mapping_map)
    {

    }
}
