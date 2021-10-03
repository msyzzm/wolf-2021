#region Author
// Copyright (c) 2015 Multiverse
// 
// Namespace:   $NAMESPACE$
// Class:       $CLASS$
// Author:      ZengWeihuan
// CreateTime:  2021-04-17-22:33
#endregion


using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace NightWerewolf
{
    public class WolfNetworkManager : NetworkManager
    {
        public string PlayerName;

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            base.OnClientSceneChanged(conn);
            print(networkSceneName);
        }

        public GameObject GetCardPrefab()
        {
            for (var i = 0; i < spawnPrefabs.Count; i++)
            {
                if (spawnPrefabs[i].name == "RoleCard")
                {
                    return spawnPrefabs[i];
                }
            }
            return null;
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("WolfNetworkManager OnClientDisconnect");
            base.OnClientDisconnect(conn);
        }

        public override void OnStopClient()
        {
            Debug.Log("NetworkManager OnStopClient");
            base.OnStopClient();
        }
    }
}