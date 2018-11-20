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
	/// 表示を行うかどうかのもの
	/// </summary>
	private bool is_display;

	private GameObject created_prefab;

	//一時的なコンストラクタ
	/// <summary>
	/// 
	/// </summary>
	/// <param name="_display"></param>
	/// <param name="_prefab"></param>
	/// <param name="_setting_pos"></param>
	public MapBlock(bool _display,GameObject _prefab,Vector3 _setting_pos)
	{
		if(_display)
		{
			created_prefab = GameObject.Instantiate(_prefab);
			created_prefab.transform.position = _setting_pos;
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
