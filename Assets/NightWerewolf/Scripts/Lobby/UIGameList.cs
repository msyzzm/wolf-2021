#region Author
// Copyright (c) 2015 Multiverse
// 
// Namespace:   $NAMESPACE$
// Class:       $CLASS$
// Author:      ZengWeihuan
// CreateTime:  2021-04-17-23:22
#endregion

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NightWerewolf
{
    public class UIGameList : MonoBehaviour
    {
        public static UIGameList     Instance;
        public        Button         CreateRoomButton;
        public        TMP_InputField NameInputField;

        private void Awake()
        {
            Instance = this;
        }

        void CreateRoom()
        {
            LRMManager.Instance.CreateRoom(NameInputField.text, NameInputField.text);
        }

        private void OnEnable()
        {
            CreateRoomButton.onClick.AddListener(CreateRoom);
        }

        private void OnDisable()
        {
            CreateRoomButton.onClick.RemoveListener(CreateRoom);
        }
    }
}