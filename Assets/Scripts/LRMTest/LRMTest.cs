using System;
using System.Collections;
using System.Collections.Generic;
using LightReflectiveMirror;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class LRMTest : MonoBehaviour
{
    public  Transform                      scrollParent;
    public  GameObject                     serverEntry;
    private LightReflectiveMirrorTransport _lightReflectiveMirrorTransport;

    private void Start()
    {
        if (_lightReflectiveMirrorTransport == null)
        {
            _lightReflectiveMirrorTransport = (LightReflectiveMirrorTransport) Transport.activeTransport;
            _lightReflectiveMirrorTransport.serverListUpdated.AddListener(ServerListUpdate);
        }
    }

    private void OnDisable()
    {
        _lightReflectiveMirrorTransport.serverListUpdated.RemoveListener(ServerListUpdate);
    }

    public void Refresh()
    {
        _lightReflectiveMirrorTransport.RequestServerList();
    }

    void ServerListUpdate()
    {
        foreach (Transform t in scrollParent)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < _lightReflectiveMirrorTransport.relayServerList.Count; i++)
        {
            var newEntry = Instantiate(serverEntry, scrollParent);
            newEntry.transform.GetChild(0).GetComponent<Text>().text = _lightReflectiveMirrorTransport.relayServerList[i].serverName;
            var serverId = _lightReflectiveMirrorTransport.relayServerList[i].serverId;
            newEntry.GetComponent<Button>().onClick.AddListener((() =>
            {
                ConnectToServer(serverId);
            }));
        }
    }

    void ConnectToServer(string _serverId)
    {
        NetworkManager.singleton.networkAddress = _serverId;
        NetworkManager.singleton.StartClient();
    }
}
