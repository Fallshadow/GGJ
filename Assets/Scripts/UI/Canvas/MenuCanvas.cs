using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game.utility;

namespace game.ui
{
    public class MenuCanvas :UICanvasBase
    {
        private void OnEnable()
        {
            EventManager.instance.Register<string>(EventGroup.SCENE, (short)SceneEvent.SCENE_OUT, OutStart);
            EventManager.instance.Register<string>(EventGroup.SCENE, (short)SceneEvent.SCENE_IN, InStart);
        }
        private void OnDisable()
        {
            EventManager.instance.Unregister<string>(EventGroup.SCENE, (short)SceneEvent.SCENE_OUT, OutStart);
            EventManager.instance.Unregister<string>(EventGroup.SCENE, (short)SceneEvent.SCENE_IN, InStart);
        }


        /// <summary>
        /// 从主菜单离开
        /// </summary>
        /// <param name="sceneName"></param>
        private void OutStart(string sceneName)
        {
            if(sceneName == data.ResourcesPathSetting.StartScene)
            {
                UIManager.instance.OnCloseCanvas(this);
            }
        }

        /// <summary>
        /// 进入主菜单
        /// </summary>
        /// <param name="sceneName"></param>
        private void InStart(string sceneName)
        {
            if(sceneName == data.ResourcesPathSetting.StartScene)
            {
                UIManager.instance.OnOpenCanvas(this);
            }
        }

        #region Button
        public void ButtonStart()
        {
            SceneManager.instance.nextSceneName = data.ResourcesPathSetting.HomeTownScene;
            UIManager.instance.FadeOut();
        }
        #endregion
    }
}

