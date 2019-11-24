using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game.ui
{
    public class UICanvasBase : UIBase
    {
        GraphicRaycaster Raycaster = null;

        public override void OnCreate()
        {
            base.OnCreate();
            Raycaster = GetComponent<GraphicRaycaster>();
            Raycaster.enabled = false;
        }
        public override void Show()
        {
            Raycaster.enabled = true;
            base.Show();
        }

        public override void Hide()
        {
            Raycaster.enabled = false;
            base.Hide();
        }
    }
}

