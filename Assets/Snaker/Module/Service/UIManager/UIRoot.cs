using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Snaker.Module.Service.UIManager
{
    public class UIRoot : MonoBehaviour
    {
        public const string LOG_TAG = "UIRoot";

        ///<summary>
        ///从UIRoot下通过类型寻找一个组件对象
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <returns></returns>
        public static T Find<T>() where T : MonoBehaviour
        {
            string name = typeof(T).Name;
            GameObject obj = Find(name);
            if(obj != null)
            {
                return obj.GetComponent<T>();
            }

            return null;
        }

        ///<summary>
        ///从UIRoot下通过类型寻找一个组件对象
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "name"></param>
        /// <returns></returns>
        public static T Find<T>(string name) where T : MonoBehaviour
        {
            GameObject obj = Find(name);
            if(obj != null)
            {
                return obj.GetComponent<T>();
            }

            return null;
        }

        ///<summary>
        ///从UIRoot下通过类型寻找一个组件对象
        /// </summary>
        /// <param name = "name"></param>
        /// <returns></returns>
        public static GameObject Find(string name)
        {
            Transform obj = null;
            GameObject root = FindUIRoot();
            if(root != null)
            {
                obj = root.transform.Find(name);
            }

            if(obj != null)
            {
                return obj.gameObject;
            }

            Debugger.LogError(LOG_TAG, "Find() UI:{0} 不存在！", name);
            return null;
        }

        ///<summary>
        ///当前场景中寻找UIRoot对象
        /// </summary>
        /// <param name = "child"></param>
        public static GameObject FindUIRoot()
        {
            GameObject root = GameObject.Find("UIRoot");
            if(root == null || root.GetComponent<UIRoot>() == null)
            {
                Debugger.LogError(LOG_TAG, "FindUIRoot() UIRoot or UIRoot.cs Is Not Exist");
                return null;
            }

            return root;
        }

        ///<summary>
        ///当一个UIPage/UIWindow/UIWidget添加到UIRoot下面
        /// </summary>
        /// <param name = "child"></param>
        public static void AddChild(UIPanel child, bool isFirst = false)
        {
            GameObject root = FindUIRoot();
            if(root == null || child == null)
            {
                return;
            }

            child.transform.SetParent(root.transform, false);

            if (isFirst)
                child.transform.SetAsFirstSibling();

            return;
        }

    }
}
