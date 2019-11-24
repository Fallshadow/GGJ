using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using game.utility;

namespace game.ui
{
    public class UIManager : SingletonMonoBehaviorNoDestroy<UIManager>
    {
        public Stack<UIBase> uiStack = new Stack<UIBase>();
        public Dictionary<string, UIBase> uiDict = new Dictionary<string, UIBase>();
        public Image maskImageFront;
        public Transform UIMainRoot;
        public float maskSpeed;
        public float maskValue;


        private void Start()
        {
            OnOpenCanvas(GetCanvas<MenuCanvas>(data.ResourcesPathSetting.MenuCanvas));
        }

        public void SceneMainUIChange()
        {

        }


        public T GetCanvas<T>(string prefabUIName, Transform parent = null) where T : UICanvasBase
        {
            return GetUi(prefabUIName, parent) as T;
        }
        public T GetWindow<T>(string prefabUIName, Transform parent = null) where T : UIWindowBase
        {
            return GetUi(prefabUIName, parent) as T;
        }
        public T GetUIbase<T>(string prefabUIName, Transform parent = null) where T : UIBase
        {
            return GetUi(prefabUIName, parent) as T;
        }

        /// <summary>
        /// 打开一个新界面
        /// </summary>
        /// <param name="uICanvasBase"></param>
        public void OnOpenCanvas(UICanvasBase uICanvasBase)
        {
            uiStack.Push(uICanvasBase);
            ShowTopCanvas();
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        /// <param name="uICanvasBase"></param>
        public void OnCloseCanvas(UICanvasBase uICanvasBase)
        {
            CloseTopCanvas();
        }

        /// <summary>
        /// 立即遮罩
        /// </summary>
        public void Mask()
        {
            maskImageFront.raycastTarget = true;
            ChangeMaskAlpha(maskValue,true);
        }

        /// <summary>
        /// 立即取消遮罩
        /// </summary>
        public void DisMask()
        {
            maskImageFront.raycastTarget = false;
            ChangeMaskAlpha(0, true);
        }

        /// <summary>
        /// 渐变遮罩，淡出
        /// </summary>
        public void FadeOut()
        {
            maskImageFront.raycastTarget = true;
            ChangeMaskAlpha(1);
        }

        /// <summary>
        /// 渐变取消遮罩，淡入
        /// </summary>
        public void FadeIn()
        {
            maskImageFront.raycastTarget = false;
            ChangeMaskAlpha(0);
        }


        #region 我的私有工具方法
        /// <summary>
        /// 改变遮罩透明度
        /// </summary>
        /// <param name="targetAlpha"></param>
        /// <param name="immediate"></param>
        private void ChangeMaskAlpha(float targetAlpha, bool immediate = false)
        {
            Color tempColor = maskImageFront.color;
            if (immediate)
            {
                tempColor.a = targetAlpha;
                maskImageFront.color = tempColor;
                return;
            }
            StartCoroutine(Fade(targetAlpha));
        }

        /// <summary>
        /// 渐变遮罩，如果有下一个场景，跳转
        /// </summary>
        /// <param name="targetAlpha"></param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            Color tempColor = maskImageFront.color;
            while (Mathf.Abs( maskImageFront.color.a - targetAlpha)>0.01f)
            {
                tempColor.a = Mathf.Clamp01(Mathf.Lerp(tempColor.a,targetAlpha,1f));
                maskImageFront.color = tempColor;
                yield return new WaitForSeconds(0.05f);
            }
            if (SceneManager.instance.nextSceneName != null)
            {
                SceneManager.instance.LoadScene();
            }
        }
        private void ShowTopCanvas()
        {
            uiStack.Peek().SetState(UIState.UI_Show);
        }

        private void CloseTopCanvas()
        {
            uiStack.Peek().SetState(UIState.UI_Hide);
            uiStack.Pop();
        }

        /// <summary>
        /// 从资源中创建UI
        /// </summary>
        /// <param name="prefabUIName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private UIBase CreateUI(string prefabUIName, Transform parent = null)
        {
            string path = data.ResourcesPathSetting.prefabUIPath + prefabUIName;
            UIBase uIBase = Resources.Load<UIBase>(path);
            if (uIBase == null)
            {
                Debug.LogError($"[UIManager]Can't find prefab : {path}");
                return null;
            }
            if (parent == null)
            {
                parent = UIMainRoot;
            }
            uIBase = Instantiate(uIBase, parent);
            uIBase.OnCreate();
            uiDict.Add(prefabUIName, uIBase);
            return uIBase;
        }

        /// <summary>
        /// 获得UIBase
        /// </summary>
        /// <param name="prefabUIName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private UIBase GetUi(string prefabUIName, Transform parent = null)
        {
            uiDict.TryGetValue(prefabUIName, out UIBase uIBase);
            if (uIBase != null)
            {
                return uIBase;
            }
            uIBase = CreateUI(prefabUIName, parent);
            return uIBase;
        }


        #endregion
    }

}
