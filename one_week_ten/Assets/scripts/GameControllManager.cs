///<summary>
/// 概要：このクラスは、ゲーム全体をコントロールするためのクラスです。
/// 
///
/// <filename>
/// GameControllManager.cs
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
using UnityEngine.Playables;

public class GameControllManager : MonoBehaviour
{
    public enum GameState
    {
        Title,      //タイトル画面
        Prepare,    //準備画面
        Game,       //ゲーム中の画面
        Result,     //結果発表
        Reset,      //レベルなどのリセット
    }


    /// <summary>
    /// キャラクターを動かすためのモノ
    /// </summary>
    [SerializeField]
    GameObject character_move_object;

    /// <summary>
    /// きゃらくたーのアニメーションを動かすためのもの
    /// </summary>
    [SerializeField]
    PlayableDirector character_director;

    /// <summary>
    /// マップの情報を取得する。
    /// </summary>
    [SerializeField]
    private MapManager map_manager;

    [SerializeField]
    UIManager ui_manager;


    private readonly float LIMIT_TIME = 10.0f;


    /// <summary>
    /// ゲームの状態
    /// </summary>
    private GameState game_state = GameState.Title;


    /// <summary>
    /// 現在のプレイヤーの位置情報を保持しておく。
    /// </summary>
    private int now_player_pos_x;
    private int now_player_pos_y;


    /// <summary>
    /// 制限時間
    /// </summary>
    private float Times = 0;

    /// <summary>
    /// 表示のための時間
    /// </summary>
    private int display_time
    {
        get
        {
            return (int)Times;
        }
    }


    // Use this for initialization
    void Start ()
    {
        ui_manager.SwithcingState(GameState.Title);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch_state();
    }

    private void switch_state()
    {
        switch (game_state)
        {
            //タイトル画面
            case GameState.Title:
                title_stating();
                break;
            case GameState.Prepare:
                prepare_state();
                break;
            //ゲーム画面
            case GameState.Game:
                game_stating();
                break;
            case GameState.Reset:
                result_state();
                break;
            //リザルト画面
            case GameState.Result:
                break;
        }

    }
    #region title_state
    /// <summary>
    /// タイトル中のあれこれ
    /// </summary>
    private void title_stating()
    {

    }

    #endregion title_state

    /// <summary>
    /// 準備画面　初期化など
    /// </summary>
    private void prepare_state()
    {
        if(map_manager.IsCompleteStart)
        {
            game_state = GameState.Game;
            var _map = map_manager.NowPlayingStage;
            character_move_object.transform.position =  _map.Start3DPos;
            map_manager.IsCompleteStart = false;

            var _pos = _map.StartPos;
            now_player_pos_x = _pos.x;
            now_player_pos_y = _pos.y;
            ui_manager.SwithcingState(GameState.Game);
            Times = LIMIT_TIME;
            ui_manager.SetScore(map_manager.Lv);
        }

    }
    
    /// <summary>
    /// 結果表示画面
    /// </summary>
    private void result_state()
    {
        
    }


    #region game_state

    /// <summary>
    /// Game中のState
    /// </summary>
    private void game_stating()
    {
        character_move();
        cheak_end();
        update_time();
        
    }

    #endregion

    #region result_state
    private void result_stating()
    {

    }
    #endregion result_state


    /// <summary>
    /// キャラクターの移動
    /// </summary>
    private void character_move()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(character_director.state != PlayState.Playing)
            {
                //上向きに回転を行って、移動する。
                character_move_object.transform.rotation = Quaternion.Euler(0, 0, 0);
                var _asset = character_director.playableAsset;
                character_director.Play();
                now_player_pos_y += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (character_director.state != PlayState.Playing)
            {
                //左向きに回転を行って、移動する。
                character_move_object.transform.rotation = Quaternion.Euler(0, -90, 0);
                character_director.Play();
                now_player_pos_x -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (character_director.state != PlayState.Playing)
            {
                //右向きに回転を行って、移動する。
                character_move_object.transform.rotation = Quaternion.Euler(0, 180, 0);
                character_director.Play();
                now_player_pos_y -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (character_director.state != PlayState.Playing)
            {
                //下向きに回転を行って、移動する。
                character_move_object.transform.rotation = Quaternion.Euler(0, 90, 0);
                character_director.Play();
                now_player_pos_x += 1;
            }
        }
    }

    /// <summary>
    /// ゲームが終了したかをチェックする。
    /// </summary>
    private void cheak_end()
    {
        var _map = map_manager.NowPlayingStage;
        ///再生していない時に、その足場が、ない状態なら、死亡
        if(character_director.state != PlayState.Playing)
        {
            
            bool _block_non = _map.map_blocks[now_player_pos_x, now_player_pos_y].IsBlockNon;

            if (_block_non)
            {
                game_state = GameState.Result;
                ui_manager.SwithcingState(GameState.Result);
                ui_manager.SetResultScore(map_manager.Lv);
            }
        }

        if (character_director.state != PlayState.Playing)
        {
            ///ゴールかどうか？
            if (_map.map_blocks[now_player_pos_x, now_player_pos_y].IsGoal)
            {
                map_manager.NextStage();
                game_state = GameState.Prepare;
                ui_manager.SetScore(map_manager.Lv);
                Times += 10f;
            }
        }

        ///時間切れになったら終了
        if(Times < 0)
        {
            game_state = GameState.Result;
            ui_manager.SwithcingState(GameState.Result);
        }
    }

    /// <summary>
    /// 時間を更新する
    /// </summary>
    private void update_time()
    {
        Times = Times - Time.deltaTime;
        ui_manager.SetTime(display_time);
    }

    /// <summary>
    /// スタート画面で呼ばれる関数
    /// </summary>
    public void onclick_start()
    {
        game_state = GameState.Prepare;
    }

    /// <summary>
    /// 結果画面から、タイトルに戻る
    /// </summary>
    public void onclick_back_title()
    {
        map_manager.Reset();
        game_state = GameState.Title;
        ui_manager.SwithcingState(GameState.Title);
    }
}
