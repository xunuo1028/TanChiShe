using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SGF.Utils;
using UnityEngine.EventSystems;

namespace Snaker.Module.Service.UIManager
{
    public static class UIUtils
    {
        ///<summary>
        ///设置一个UI元素是否可见
        /// </summary>
        /// <param name = "ui"></param>
        /// <param name = "value"></param>
        public static void SetActive(UIBehaviour ui, bool value)
        {
            if(ui != null && ui.gameObject != null)
            {
                GameObjectUtils.SetActiveRecursively(ui.gameObject, value);
            }
        }
    }
}
