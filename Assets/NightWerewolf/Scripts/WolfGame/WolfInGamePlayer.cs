#region Author
// Copyright (c) 2015 Multiverse
// 
// Namespace:   $NAMESPACE$
// Class:       $CLASS$
// Author:      ZengWeihuan
// CreateTime:  2021-04-17-22:36
#endregion


using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NightWerewolf
{
    public class WolfInGamePlayer : NetworkBehaviour
    {
        [SyncVar(hook = "OnNameChange")]
        public string Name;
        public TextMeshPro        NameText;
        public WolfNetworkManager WolfNetworkManager;
        [SyncVar(hook = "OnRoleChange")]
        public Role               m_role;
        public Role        Role
        {
            get => m_role;
            set
            {
                m_role        = value;
                RoleText.text = m_role.ToFriendlyString();
            }
        }

        public GameObject  RoleCard;
        public TextMeshPro RoleText;

        public override void OnStartClient()
        {
            base.OnStartClient();
            RoleCard.SetActive(false);
            RoleText.gameObject.SetActive(false);
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            WolfNetworkManager = (WolfNetworkManager) NetworkManager.singleton;
            CmdChangeName(WolfNetworkManager.PlayerName);
            
            GameSceneRef.Instance.ExitGameButton.onClick.AddListener(() =>
            {
                if (WolfNetworkManager.mode == NetworkManagerMode.Host)
                {
                    WolfNetworkManager.StopHost();
                }
                else if(WolfNetworkManager.mode == NetworkManagerMode.ClientOnly)
                {
                    WolfNetworkManager.StopClient();
                }
            });

            GameSceneRef.Instance.StartGameButton.onClick.AddListener(CmdStartGame);

            Camera.main.transform.parent = transform;
            Camera.main.transform.localPosition = new Vector3(0,0,-100);

            if (WolfNetworkManager.mode == NetworkManagerMode.Host)
            {
                GameSceneRef.Instance.ConfigGameButton.gameObject.SetActive(true);
                GameSceneRef.Instance.KickButton.gameObject.SetActive(true);
            }
            else if(WolfNetworkManager.mode == NetworkManagerMode.ClientOnly)
            {
                GameSceneRef.Instance.ConfigGameButton.gameObject.SetActive(false);
                GameSceneRef.Instance.KickButton.gameObject.SetActive(false);
            }

            GameSceneRef.Instance.Self = this;
        }

        [Command]
        void CmdChangeName(string _name)
        {
            Name = _name;
        }

        private void OnNameChange(string _old, string _name)
        {
            NameText.text = _name;
        }
        
        [Command]
        void CmdStartGame()
        {
            foreach (var uiGameConfigRole in GameSceneRef.Instance.UiGameConfigRoles)
            {
                if (uiGameConfigRole.Selected)
                {
                    var position = GameSceneRef.Instance.RandomInGame();
                    var card     =  Instantiate(WolfNetworkManager.GetCardPrefab(), position, Quaternion.identity);
                    card.GetComponent<RoleCard>().Role = uiGameConfigRole.Role;
                    NetworkServer.Spawn(card);
                }
            }
        }
        
        // 拾取角色卡
        [Command]
        public void CmdPickUpRoleCard(GameObject _roleCard)
        {
            var roleCardComponent = _roleCard.GetComponent<RoleCard>();
            m_role = roleCardComponent.Role;
            NetworkServer.Destroy(_roleCard);
        }

        [Command]
        public void CmdChangeCard(GameObject _player)
        {
            ExchangeRole(this, _player.GetComponent<WolfInGamePlayer>());
        }

        [Command]
        public void CmdChangeCardAnoter(GameObject _player1, GameObject _player2)
        {
            ExchangeRole(_player1.GetComponent<WolfInGamePlayer>(), _player2.GetComponent<WolfInGamePlayer>());
        }

        private void ExchangeRole(WolfInGamePlayer _player1, WolfInGamePlayer _player2)
        {
            var temp = _player2.Role;
            _player2.Role = _player1.Role;
            _player1.Role = temp;
        }

        // Role属性监听
        void OnRoleChange(Role _old, Role _role)
        {
            switch (_role)
            {
                case Role.InitRole:
                    RoleCard.SetActive(false);
                    RoleText.gameObject.SetActive(false);
                    break;
                default:
                    RoleCard.SetActive(true);
                    RoleText.gameObject.SetActive(true);
                    if(isLocalPlayer) RoleText.text = _role.ToFriendlyString();
                    break;
            }
        }
    }
}