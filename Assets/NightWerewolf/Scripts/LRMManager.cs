using System;
using System.Collections.Generic;
using LightReflectiveMirror;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NightWerewolf
{
    public class LRMManager : MonoBehaviour
    {
        public static LRMManager                     Instance;
        public        Transform                      scrollParent;
        public        GameObject                     serverEntry;
        public        LightReflectiveMirrorTransport LightReflectiveMirrorTransport;
        public NetworkManager LocalPrefab;
        public NetworkManager LRMPrefab;
        public bool local;
        public List<Room> Rooms;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (local)
            {
                Instantiate(LocalPrefab);
            }
            else
            {
                Instantiate(LRMPrefab);
                if (LightReflectiveMirrorTransport == null)
                {
                    LightReflectiveMirrorTransport = (LightReflectiveMirrorTransport) Transport.activeTransport;
                    LightReflectiveMirrorTransport.serverListUpdated.AddListener(ServerListUpdate);
                }
            }
        }

        private void OnDisable()
        {
            if(local) return;
            LightReflectiveMirrorTransport.serverListUpdated.RemoveListener(ServerListUpdate);
        }
        
        public void CreateRoom(string _playerName, string _serverName)
        {
            if (local)
            {
                NetworkManager.singleton.StartHost();
            }
            else
            {
                ((WolfNetworkManager) NetworkManager.singleton).PlayerName  = _playerName;
                LightReflectiveMirrorTransport.serverName = _serverName;
                NetworkManager.singleton.StartHost();
            }
        }

        public void Refresh()
        {
            LightReflectiveMirrorTransport.RequestServerList();
        }

        void ServerListUpdate()
        {
            foreach (Transform t in scrollParent)
            {
                Destroy(t.gameObject);
            }

            Rooms = LightReflectiveMirrorTransport.relayServerList;
            for (int i = 0; i < LightReflectiveMirrorTransport.relayServerList.Count; i++)
            {
                var newEntry = Instantiate(serverEntry, scrollParent);
                newEntry.transform.GetChild(0).GetComponent<TMP_Text>().text = LightReflectiveMirrorTransport.relayServerList[i].serverName;
                var serverId = LightReflectiveMirrorTransport.relayServerList[i].serverId;
                newEntry.GetComponent<Button>().onClick.AddListener((() =>
                {
                    ConnectToServer(serverId);
                }));
            }
        }

        void ConnectToServer(string _serverId)
        {
            NetworkManager.singleton.networkAddress                    = _serverId;
            ((WolfNetworkManager) NetworkManager.singleton).PlayerName = UIGameList.Instance.NameInputField.text;
            NetworkManager.singleton.StartClient();
        }
    }
}
