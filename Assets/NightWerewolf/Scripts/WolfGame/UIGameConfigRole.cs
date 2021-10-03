#region Author
// Copyright (c) 2015 Multiverse
// 
// Namespace:   $NAMESPACE$
// Class:       $CLASS$
// Author:      ZengWeihuan
// CreateTime:  2021-04-22-0:03
#endregion


using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NightWerewolf
{
    public class UIGameConfigRole : MonoBehaviour
    {
        private Role     m_role;
        public  Role     Role
        {
            get => m_role;
            set
            {
                m_role = value;
                var str = m_role.ToFriendlyString();
                Text.text = str;
            }
        }

        public TMP_Text Text;
        public Button   Button;
        public bool     Selected = true;

        private void Awake()
        {
            OnClick();
            Button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Selected = !Selected;
            var color = Button.GetComponent<Image>().color;
            color.a                            = Selected ? 1 : .5f;
            Button.GetComponent<Image>().color = color;
        }
    }
}