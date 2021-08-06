
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NightWerewolf
{
    public class UIOfflineScene : MonoBehaviour
    {
        public Button EnterGameButton;

        void EnterGame()
        {
            SceneManager.LoadScene("RoomList");
        }
        private void OnEnable()
        {
            EnterGameButton.onClick.AddListener(EnterGame);
        }

        private void OnDisable()
        {
            EnterGameButton.onClick.RemoveListener(EnterGame);
        }
    }
}
