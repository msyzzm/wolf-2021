using System.Collections;
using System.Collections.Generic;
using NightWerewolf;
using UnityEngine;
using Pathfinding;
using UnityEngine.EventSystems;

public class AstarPlayer : MonoBehaviour
{
    private float _moveSpeed = 10.0f; //AI角色移动速度

    private Seeker  _seeker;        //A*寻路路径计算组件
    private Vector3 _targetPoint;   //寻路目标点
    private Path    _path;          //寻路计算出的路径
    private int     _playerCenterY; //AI角色Y轴坐标值

    private int              _curPathPoint;    //当前路径点数
    private bool             _stopMove = true; //标记AI角色停止移动
    private WolfInGamePlayer m_self;
    void Start()
    {
        _seeker            = GetComponent<Seeker>();
        m_self = GetComponent<WolfInGamePlayer>();

        if (_seeker == null)
        {
            Debug.Log("Seeker Component is null！");
        }
    }

    private void Update()
    {
        if(!m_self.isLocalPlayer) return;
        //监听鼠标左键按下
        if (!Util.IsPointerOverUIObject() && Input.GetMouseButtonDown(0))
        {
            // 用射线创建 TargetPoint
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // print(Input.mousePosition);

            //利用物理引擎发射一条射线，返回true，说明射线与物体发生碰撞
            if (Physics.Raycast(ray,
                                out var hitDynamic,
                                100,
                                1<<LayerMask.NameToLayer("Dynamic")))
            {
                // 点击身份牌
                if (m_self.Role == Role.InitRole && hitDynamic.transform.GetComponent<RoleCard>() != null)
                {
                    var card = hitDynamic.transform;
                    m_self.CmdPickUpRoleCard(card.gameObject);
                }

                // 点击玩家
                var wolfInGamePlayer = hitDynamic.transform.GetComponent<WolfInGamePlayer>();
                if (wolfInGamePlayer != null && wolfInGamePlayer!=m_self)
                {
                    // 选择交换的另一个玩家
                    if (GameSceneRef.Instance.WaitForAnother)
                    {
                        m_self.CmdChangeCardAnoter(GameSceneRef.Instance.Selected.gameObject, wolfInGamePlayer.gameObject);
                        GameSceneRef.Instance.WaitForAnother = false;
                    }
                    else
                    {
                        GameSceneRef.Instance.ShowOperation(true);
                        GameSceneRef.Instance.Selected = wolfInGamePlayer;
                    }
                }
                return;
            }
            
            //利用物理引擎发射一条射线，返回true，说明射线与物体发生碰撞
            if (Physics.Raycast(ray,
                                out var hit,
                                100,
                                    1<<LayerMask.NameToLayer("Ground")))
            {
                _targetPoint = hit.point;
                //计算路径
                _seeker.StartPath(transform.position, _targetPoint, OnPathCompleted);
            }
        }
        
        // 移动
        if (_path != null && !_stopMove)
        {
            //计算Player当前位置到下一个目标点的方向
            Vector3 temp      = _path.vectorPath[_curPathPoint];
            Vector3 nextPoint = new Vector3(temp.x, temp.y + _playerCenterY, temp.z);
            Vector3 dir       = (nextPoint - transform.position).normalized;

            //计算Player当前位置到下一个目标点的距离
            float offset = Vector3.Distance(transform.position, nextPoint);

            if (offset < 0.1f)
            {
                transform.position = nextPoint;
                _curPathPoint++;
                //检查Player是否走到最后一个目标点，是，完成移动
                CheckLastPoint();
            }
            else
            {
                //计算一帧移动多少距离
                Vector3 distance = dir * _moveSpeed * Time.deltaTime;
                if (distance.magnitude > offset)
                {
                    //重新计算移动距离
                    distance = dir * offset;
                    _curPathPoint++;
                    //检查是否到达最后一个节点
                    CheckLastPoint();
                }

                transform.localPosition += distance;
            }
        }
    }

    //检查到达最后一个目标点
    private void CheckLastPoint()
    {
        if (_curPathPoint == _path.vectorPath.Count)
        {
            _curPathPoint = 0;
            _path         = null;
            _stopMove     = true;
        }
    }

    //寻路路径计算完成时回调
    private void OnPathCompleted(Path path)
    {
        _path         = path;
        _curPathPoint = 0;
        _stopMove     = false;
    }
    
}