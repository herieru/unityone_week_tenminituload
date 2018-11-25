///<summary>
/// 概要：UIのマネージャのクラス状況に応じて表示部分を切り替えます。
/// また、その時にあった、情報を与えなければいけません。
/// 拡張性など皆無なのできおつけてください。
///
/// <filename>
/// UIManager.cs
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

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// タイトル中のUI
    /// </summary>
    [SerializeField]
    private GameObject title_root;

    /// <summary>
    /// ゲーム中のUI
    /// </summary>
    [SerializeField]
    private GameObject game_ui_root;

    /// <summary>
    /// 結果画面
    /// </summary>
    [SerializeField]
    private GameObject result_root;

    /// <summary>
    /// ゲーム中のUI
    /// </summary>
    [SerializeField]
    GameUI play_game_ui;

    /// <summary>
    /// 結果画面
    /// </summary>
    [SerializeField]
    ResultUI result_ui;


    /// <summary>
    /// 表示するためのUIを切り替える
    /// </summary>
    /// <param name="_game_state"></param>
    public void SwithcingState(GameControllManager.GameState _game_state)
    {
        switch (_game_state)
        {
            case GameControllManager.GameState.Title:
                title_root.SetActive(true);
                game_ui_root.SetActive(false);
                result_root.SetActive(false);
                break;
            case GameControllManager.GameState.Game:
                title_root.SetActive(false);
                game_ui_root.SetActive(true);
                result_root.SetActive(false);
                break;
            case GameControllManager.GameState.Result:
                title_root.SetActive(false);
                game_ui_root.SetActive(false);
                result_root.SetActive(true);
                break;
        }

    }


    /// <summary>
    /// スコアを表示
    /// </summary>
    /// <param name="_score"></param>
    public void SetScore(int _score)
    {
        play_game_ui.SetScore(_score);
    }

    /// <summary>
    /// 時間制限を表示する。
    /// </summary>
    /// <param name="_time"></param>
    public void SetTime(int _time)
    {
        play_game_ui.SetTime(_time);
    }

    /// <summary>
    /// 結果の値を入れる
    /// </summary>
    /// <param name="_score"></param>
    public void SetResultScore(int _score)
    {
        result_ui.SetResultScore(_score);
    }

}
