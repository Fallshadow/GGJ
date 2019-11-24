using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.ui
{
    public enum UIState
    {
        UI_Idle = 0,
        UI_Show = 1,
        UI_Hide = 2
    }

    public abstract class UIBase : MonoBehaviour
    {
        UIState state;


        public void SetState(UIState newState)
        {
            state = newState;
            switch (state)
            {
                case UIState.UI_Idle:
                    break;
                case UIState.UI_Show:
                    Show();
                    break;
                case UIState.UI_Hide:
                    Hide();
                    break;
                default:
                    break;
            }
        }

        public virtual void OnCreate() {
            gameObject.SetActive(false);
        }
        public virtual void Show() {
            gameObject.SetActive(true);
            OnShowDoTween();
        }
        public virtual void Hide() {
            OnHideDoTween();
        }
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }


        protected virtual void OnShowDoTween() { }
        protected virtual void OnHideDoTween() {
            //TODO:变成回调函数，回调close
            Close();
        }

    }
}

