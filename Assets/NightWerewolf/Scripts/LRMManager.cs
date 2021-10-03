
using System.Collections.Generic;
using LightReflectiveMirror;
using Mirror;
using UnityEngine;

namespace NightWerewolf
{
    public class LRMManager : MonoBehaviour
    {
        public static LRMManager Instance;

        public Transform m_scrollParent;

        public LobbyRoomItem m_lobbyRoomItem;

        public LightReflectiveMirrorTransport m_lightReflectiveMirrorTransport;

        public NetworkManager m_localPrefab;

        public NetworkManager m_lrmPrefab;

        public bool m_local;

        public List<Room> m_rooms;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (m_local)
            {
                Instantiate(m_localPrefab);
            }
            else
            {
                Instantiate(m_lrmPrefab);
                if (m_lightReflectiveMirrorTransport == null)
                {
                    m_lightReflectiveMirrorTransport = (LightReflectiveMirrorTransport) Transport.activeTransport;
                    m_lightReflectiveMirrorTransport.serverListUpdated.AddListener(ServerListUpdate);
                }
            }
        }

        private void OnDisable()
        {
            if (m_local) return;
            m_lightReflectiveMirrorTransport.serverListUpdated.RemoveListener(ServerListUpdate);
        }

        public void CreateRoom(string _playerName, string _serverName)
        {
            if (m_local)
            {
                NetworkManager.singleton.StartHost();
            }
            else
            {
                ((WolfNetworkManager) NetworkManager.singleton).PlayerName = _playerName;
                m_lightReflectiveMirrorTransport.serverName = _serverName;
                NetworkManager.singleton.StartHost();
            }
        }

        public void Refresh()
        {
            m_lightReflectiveMirrorTransport.RequestServerList();
        }

        void ServerListUpdate()
        {
            foreach (Transform t in m_scrollParent)
            {
                Destroy(t.gameObject);
            }

            m_rooms = m_lightReflectiveMirrorTransport.relayServerList;
            for (int i = 0; i < m_lightReflectiveMirrorTransport.relayServerList.Count; i++)
            {
                var newEntry = Instantiate(m_lobbyRoomItem, m_scrollParent);
                newEntry.m_roomName.text = m_lightReflectiveMirrorTransport.relayServerList[i].serverName;
                var id = m_lightReflectiveMirrorTransport.relayServerList[i].serverId;
                newEntry.m_roomID.text = $"<size=32><color=#BEB5B6>{id}</color></size>";
                newEntry.m_button.onClick.AddListener(() => { ConnectToServer(id); });
            }
        }

        void ConnectToServer(string _serverId)
        {
            NetworkManager.singleton.networkAddress = _serverId;
            ((WolfNetworkManager) NetworkManager.singleton).PlayerName = UIGameList.Instance.NameInputField.text;
            NetworkManager.singleton.StartClient();
        }
    }
}