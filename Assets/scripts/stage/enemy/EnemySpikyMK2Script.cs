﻿using System;
using UnityEngine;
using System.Collections;



/// <summary>
/// Spiky MK-II 적 캐릭터를 정의합니다.
/// </summary>
public class EnemySpikyMK2Script : EnemyScript
{
    #region 컨트롤러가 사용할 Unity 객체를 정의합니다.
    /// <summary>
    /// 
    /// </summary>
    Rigidbody2D _rigidbody;
    /// <summary>
    /// 
    /// </summary>
    BoxCollider2D _boxCollider2D;


    #endregion










    #region Unity에서 접근 가능한 공용 객체를 정의합니다.
    /// <summary>
    /// 자신의 밑에 지면이 존재하는지 검사하기 위해 사용합니다.
    /// </summary>
    public Transform _groundCheck;
    /// <summary>
    /// 자신이 진행하는 방향에 벽이 존재하는지 검사하기 위해 사용합니다.
    /// </summary>
    public Transform _pushCheck;
    /// <summary>
    /// 무엇이 벽인지를 결정합니다. 기본값은 "Wall, MapBlock"입니다.
    /// </summary>
    public LayerMask _whatIsWall;
    /// <summary>
    /// 무엇이 땅인지를 결정합니다. 기본값은 "Ground, TiledGeometry"입니다.
    /// </summary>
    public LayerMask _whatIsGround;


    #endregion










    #region 캐릭터의 상태 필드 및 프로퍼티를 정의합니다.
    /// <summary>
    /// 캐릭터가 움직이는 속도를 정의합니다.
    /// </summary>
    public float _movingSpeed = 8;
    /// <summary>
    /// 종방향 최대 속력을 정의합니다.
    /// </summary>
    public float _yMovingSpeedMax = 10;
    /// <summary>
    /// 종방향 가속도를 정의합니다.
    /// </summary>
    public float _yMovingAccelaration = 10;

    #endregion










    #region MonoBehaviour 기본 메서드를 재정의합니다.
    /// <summary>
    /// MonoBehaviour 개체를 초기화합니다.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // 필드를 초기화합니다.
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        // 초기화를 마무리합니다.
        MoveLeft();
    }
    /// <summary>
    /// 프레임이 갱신될 때 MonoBehaviour 개체 정보를 업데이트합니다.
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// 
    /// </summary>
    protected override void FixedUpdate()
    {
        float vx = _rigidbody.velocity.x;
        float vy = _rigidbody.velocity.y - _yMovingAccelaration * Time.fixedDeltaTime;
        _rigidbody.velocity = new Vector2(vx, vy);
    }


    #endregion










    #region Collider2D의 기본 메서드를 재정의합니다.
    /// <summary>
    /// 충돌체가 트리거 내부로 진입했습니다.
    /// </summary>
    /// <param name="other">자신이 아닌 충돌체 개체입니다.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 colliderPos = transform.position;
        Vector3 otherCenter = other.bounds.center;

        // 땅과 접촉한 경우의 처리입니다.
        if (_boxCollider2D.IsTouchingLayers(_whatIsGround))
        {
            if (otherCenter.y < colliderPos.y)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _yMovingSpeedMax);
            }
            else
            {
                float vx = _rigidbody.velocity.x;
                float vy = -Mathf.Abs(_rigidbody.velocity.y);
                _rigidbody.velocity = new Vector2(vx, vy);
            }
        }
        // 벽과 접촉한 경우의 처리입니다.
        if (_boxCollider2D.IsTouchingLayers(_whatIsWall))
        {
            if (otherCenter.x < colliderPos.x)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
    }
    /// <summary>
    /// 충돌체가 여전히 트리거 내부에 있습니다.
    /// </summary>
    /// <param name="other">자신이 아닌 충돌체 개체입니다.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        // 트리거가 발동한 상대 충돌체가 플레이어라면 대미지를 입힙니다.
        if (other.CompareTag("Player"))
        {
            GameObject pObject = other.gameObject;
            PlayerController player = pObject.GetComponent<PlayerController>();


            // 플레이어가 무적 상태이거나 죽었다면
            if (player.Invencible || player.IsDead)
            {
                // 아무 것도 하지 않습니다.

            }
            // 그 외의 경우
            else
            {
                // 플레이어에게 대미지를 입힙니다.
                player.Hurt(Damage);
            }
        }
    }


    #endregion










    #region EnemyScript의 메서드를 오버라이드합니다.
    /// <summary>
    /// 캐릭터가 사망합니다.
    /// </summary>
    public override void Dead()
    {
        // 폭발 효과를 생성하고 효과음을 재생합니다.
        SoundEffects[0].Play();
        Instantiate(effects[0], transform.position, transform.rotation);


        // 사망 시 아이템 드롭 루틴입니다.
        int dropItem = UnityEngine.Random.Range(0, _items.Length);
        if (_items[dropItem] != null)
        {
            CreateItem(_items[dropItem]);
        }


        // 캐릭터가 사망합니다.
        base.Dead();
    }


    #endregion










    #region 보조 메서드를 정의합니다.
    /// <summary>
    /// 왼쪽으로 이동합니다.
    /// </summary>
    void MoveLeft()
    {
        if (FacingRight)
            Flip();
        _rigidbody.velocity = new Vector2(-_movingSpeed, _rigidbody.velocity.y);
    }
    /// <summary>
    /// 오른쪽으로 이동합니다.
    /// </summary>
    void MoveRight()
    {
        if (FacingRight == false)
            Flip();
        _rigidbody.velocity = new Vector2(_movingSpeed, _rigidbody.velocity.y);
    }
    /// <summary>
    /// 주변을 방황합니다.
    /// </summary>
    /// <returns>StartCoroutine 호출에 적합한 값을 반환합니다.</returns>
    IEnumerator WalkAround()
    {
        while (_health != 0)
        {
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 1)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
            yield return new WaitForSeconds(3);
        }
    }


    #endregion










    #region 구형 정의를 보관합니다.
    [Obsolete("뭔진 알겠는데 이거 빼도 되지 않나요?")]
    /// <summary>
    /// 
    /// </summary>
    public bool canJump;


    #endregion
}