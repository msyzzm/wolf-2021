#region Author
// Copyright (c) 2015 Multiverse
// 
// Namespace:   $NAMESPACE$
// Class:       $CLASS$
// Author:      ZengWeihuan
// CreateTime:  2021-04-18-8:44
#endregion


using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NightWerewolf
{
    public class GameSceneRef : MonoBehaviour
    {
        public Action OnGameSceneStart;
            
        public static GameSceneRef           Instance;
        public        Button                 ExitGameButton;
        public        Button                 StartGameButton;
        public        Button                 ConfigGameButton;
        public        Button                 CloseEyeButton;
        public        Button                 OpenEyeButton;
        public        Transform              LeftTop;
        public        Transform              RightBottom;
        public        GameObject             GameRoleConfigParent;
        public        GameObject             GameRoleConfigPrefab;
        public        List<UIGameConfigRole> UiGameConfigRoles;
        public        AwakeScreenEffect      AwakeScreenEffect;
        public        Button                 ViewCardButton;
        public        Button                 ChangeCardButton;
        public        Button                 ChangeCardAnotherButton;
        public        Button                 CancelButton;
        public        WolfInGamePlayer       Self;
        public        WolfInGamePlayer       Selected;
        public        bool                   WaitForAnother;

        public Button KickButton;

        public int connId = 1;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            OnGameSceneStart?.Invoke();
            InitGameConfigRole();
            CloseEyeButton.onClick.AddListener(CloseEye);
            OpenEyeButton.onClick.AddListener(OpenEye);
            OpenEyeButton.gameObject.SetActive(false);
            
            ShowOperation(false);
            // 取消选择
            CancelButton.onClick.AddListener(() =>
            {
                ShowOperation(false);
                Selected       = null;
                WaitForAnother = false;
            });
            
            // 查看卡牌
            ViewCardButton.onClick.AddListener(() =>
            {
                Debug.Log(Selected.Role.ToFriendlyString());
            });
            
            // 交换卡牌
            ChangeCardButton.onClick.AddListener(() =>
            {
                Self.CmdChangeCard(Selected.gameObject);
            });
            
            // 与另一人交换卡牌
            ChangeCardAnotherButton.onClick.AddListener(() =>
            {
                WaitForAnother = true;
            });
            
            KickButton.onClick.AddListener(()=>
            {
                NetworkServer.connections[connId].Disconnect();
            });
        }

        public Vector3 RandomInGame()
        {
            var x = Random.Range(LeftTop.position.x, RightBottom.position.x);
            var y = Random.Range(LeftTop.position.y, RightBottom.position.y);
            return new Vector3(x,y,0);
        }

        public void InitGameConfigRole()
        {
            for (int i = 1; i <= (int)Role.Curator; i++)
            {
                AddGameConfigRole((Role)i);
            }
        }

        public void AddGameConfigRole(Role _role)
        {
            var gameObject = Instantiate(GameRoleConfigPrefab, GameRoleConfigParent.transform);
            var uiGameConfigRole = gameObject.GetComponent<UIGameConfigRole>();
            uiGameConfigRole.Role = _role;
            UiGameConfigRoles.Add(uiGameConfigRole);
        }

        public void OpenEye()
        {
            OpenEyeButton.gameObject.SetActive(false);
            AwakeScreenEffect.OpenEye(CloseEyeButton);
        }
        
        public void CloseEye()
        {
            CloseEyeButton.gameObject.SetActive(false);
            AwakeScreenEffect.CloseEye(OpenEyeButton);
        }

        public void ShowOperation(bool _show)
        {
            ViewCardButton.gameObject.SetActive(_show);
            ChangeCardButton.gameObject.SetActive(_show);
            ChangeCardAnotherButton.gameObject.SetActive(_show);
            CancelButton.gameObject.SetActive(_show);
        }
        
    }
}