using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game.ui;

namespace game.utility
{
    public class SceneManager : Singleton<SceneManager>
    {
        public string currentSceneName = "Start";
        public string nextSceneName = null;
        public void LoadScene(string sceneName = null)
        {
            if(sceneName == null)
            {
                sceneName = nextSceneName;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            EventManager.instance.Send<string>(EventGroup.SCENE, (short)SceneEvent.SCENE_OUT, currentSceneName);
            EventManager.instance.Send<string>(EventGroup.SCENE, (short)SceneEvent.SCENE_IN, nextSceneName);
            currentSceneName = nextSceneName;
            nextSceneName = null;
            UIManager.instance.FadeIn();

        }
    }

}
