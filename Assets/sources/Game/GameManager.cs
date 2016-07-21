﻿using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



/// <summary>
/// 게임 관리자입니다.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region 게임 시스템이 공유하는 속성을 정의합니다.
    /// <summary>
    /// 게임 관리자입니다.
    /// </summary>
    public static GameManager Instance { get; set; }


    /// <summary>
    /// 엑스의 최대 체력입니다.
    /// </summary>
    public int MaxHealthX { get { return _gameData.MaxHealthX; } }
    /// <summary>
    /// 제로의 최대 체력입니다.
    /// </summary>
    public int MaxHealthZ { get { return _gameData.MaxHealthZ; } }


    /// <summary>
    /// 맵 상태 집합입니다.
    /// </summary>
    public GameMapStatus[] MapStatuses { get { return _gameData.MapStatuses; } }


    #endregion










    #region 필드를 정의합니다.
    /// <summary>
    /// 게임 데이터 필드입니다.
    /// </summary>
    GameData _gameData;


    #endregion










    #region MonoBehaviour 기본 메서드를 재정의합니다.
    /// <summary>
    /// MonoBehaviour 개체를 초기화합니다.
    /// </summary>
    void Awake()
    {
//        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    /// <summary>
    /// MonoBehaviour 개체를 초기화합니다.
    /// </summary>
    void Start()
    {
        
    }


    #endregion










    #region 메서드를 정의합니다.
    /// <summary>
    /// 맵 상태를 업데이트합니다.
    /// </summary>
    /// <param name="index">상태를 업데이트할 맵의 인덱스입니다.</param>
    /// <param name="mapStatus">맵의 새로운 상태입니다.</param>
    public void UpdateMapStatus(int index, GameMapStatus mapStatus)
    {
        _gameData.MapStatuses[index] = mapStatus;
    }


    /// <summary>
    /// 게임 데이터를 저장합니다.
    /// </summary>
    /// <param name="filename">게임 데이터 파일의 이름입니다.</param>
    void Save(string filename)
    {
        Stream ws = new FileStream(filename, FileMode.Create);
        BinaryFormatter serializer = new BinaryFormatter();

        serializer.Serialize(ws, _gameData);
        ws.Close();
    }
    /// <summary>
    /// 게임 데이터를 저장합니다.
    /// </summary>
    /// <param name="filename">게임 데이터 파일의 이름입니다.</param>
    /// <param name="gameData">저장할 게임 데이터입니다.</param>
    void Save(string filename, GameData gameData)
    {
        Stream ws = new FileStream(filename, FileMode.Create);
        BinaryFormatter serializer = new BinaryFormatter();

        serializer.Serialize(ws, gameData);
        ws.Close();
    }
    /// <summary>
    /// 게임 데이터를 불러옵니다.
    /// </summary>
    /// <param name="filename">게임 데이터 파일의 이름입니다.</param>
    /// <returns>성공하면 GameData를, 실패하면 null을 반환합니다.</returns>
    GameData Load(string filename)
    {
        try
        {
            Stream rs = new FileStream(filename, FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            GameData gameData = (GameData)deserializer.Deserialize(rs);
            rs.Close();

            return gameData;
        }
        catch (Exception)
        {
            return null;
        }
    }
    /// <summary>
    /// 게임 데이터 필드를 업데이트합니다.
    /// </summary>
    /// <param name="gameData">새 게임 데이터입니다.</param>
    void UpdateGameData(GameData gameData)
    {
        // 필드를 업데이트합니다.
        _gameData = gameData;
    }


    #endregion









    #region 요청 메서드를 정의합니다.
    /// <summary>
    /// 게임을 저장합니다.
    /// </summary>
    /// <param name="filename">게임 데이터 파일의 이름입니다.</param>
    public void RequestSave(string filename)
    {
        Save(filename, _gameData);
    }
    /// <summary>
    /// 게임을 저장합니다.
    /// </summary>
    /// <param name="filename">게임 데이터 파일의 이름입니다.</param>
    /// <param name="gameData">저장할 게임 데이터입니다.</param>
    public void RequestSave(string filename, GameData gameData)
    {
        Save(filename, gameData);
    }
    /// <summary>
         /// 게임을 불러옵니다.
         /// </summary>
         /// <param name="filename">게임 데이터 파일의 이름입니다.</param>
         /// <returns>  </returns>
    public GameData RequestLoad(string filename)
    {
        return Load(filename);
    }
    /// <summary>
    /// 게임 데이터를 업데이트합니다.
    /// </summary>
    /// <param name="gameData">새 게임 데이터입니다.</param>
    public void RequestUpdateData(GameData gameData)
    {
        UpdateGameData(gameData);
    }
    /// <summary>
    /// 디스크에서 게임 데이터를 제거합니다.
    /// </summary>
    /// <param name="filename"></param>
    public void RequestDeleteData(string filename)
    {
        File.Delete(filename);
    }


    #endregion










    #region 구형 정의를 보관합니다.


    #endregion
}