﻿using System;
using UnityEngine;
using System.Collections;



/// <summary>
/// CameraZoneScript의 부모입니다.
/// </summary>
public class CameraZoneParent : MonoBehaviour
{
    #region Unity에서 접근 가능한 공용 필드를 정의합니다.
    /// <summary>
    /// 장면 관리자입니다.
    /// </summary>
    public StageManager _sceneManager;
    /// <summary>
    /// CameraFollow 객체입니다.
    /// </summary>
    public CameraFollowScript _cameraFollow;


    #endregion










    #region 필드를 정의합니다.



    #endregion









    #region 프로퍼티를 정의합니다.
    /// <summary>
    /// 현재 행동중인 플레이어를 가져옵니다.
    /// </summary>
    public PlayerController Player
    {
        get { return _sceneManager._player; }
    }
    /// <summary>
    /// CameraFollow 객체입니다.
    /// </summary>
    public CameraFollowScript CameraFollow
    {
        get { return _cameraFollow; }
    }


    #endregion










    #region MonoBehaviour 기본 메서드를 재정의합니다.
    /// <summary>
    /// MonoBehaviour 개체를 초기화합니다.
    /// </summary>
    void Start()
    {
        /**
        CameraZone[] children = GetComponentsInChildren<CameraZone>();
        foreach (CameraZone child in children)
        {
            child._cameraZoneParent = this;
        }
        */
    }
    /// <summary>
    /// 프레임이 갱신될 때 MonoBehaviour 개체 정보를 업데이트 합니다.
    /// </summary>
    void Update()
    {
        
    }


    #endregion










    #region 메서드를 정의합니다.


    #endregion










    #region 구형 정의를 보관합니다.
    [Obsolete("StageManager.Player로 대체되었습니다.")]
    /// <summary>
    /// 플레이어 객체입니다.
    /// </summary>
    public PlayerController _player;




    #endregion
}