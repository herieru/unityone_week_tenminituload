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


	/// <summary>
	/// 
	/// </summary>
    /// <param name="_root">ルートとなる基準になるGameObject</param>
	/// <param name="_mapping_map">作成するマップ情報</param>
	/// <param name="_width">横に配置されるブロック数</param>
	/// <param name="_height">縦に配置されるブロック数</param>
	public void CreateSettingMapFieldInfo(GameObject _root,bool[] _mapping_map , int _width,int _height)
	{

	}

    public void Setting_MapFieldInfo(bool[] _mapping_map, int _width, int _height)
    {

    }
}
