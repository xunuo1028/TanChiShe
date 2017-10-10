using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SGF;

namespace Snaker.Module.Service.UIManager
{
    public class UIWindow : UIPanel
    {
        public delegate void CloseEvent();

        ///<summary>
        ///关闭按钮
        /// </summary>
        [SerializeField]
        private Button m_btnClose;

        ///<summary>
        ///窗口关闭事件
        /// </summary>
        public event CloseEvent onClose;

        ///<summary>
        ///打开UI的参数
        /// </summary>
        protected object m_openArg;

        ///<summary>
        ///该UI的当前实例是否曾经被打开过
        /// </summary>
        private bool m_isOpenedOnce;

        ///<summary>
        ///当UI可用时调用
        /// </summary>
        protected void OnEnable()
        {
            this.Log("OnEnable()");
            if(m_btnClose != null)
            {
                m_btnClose.onClick.AddListener(OnBtnClose);
            }

#if UNITY_EDITOR
            if(m_isOpenedOnce)
            {
                OnOpen(m_openArg);
            }
#endif
        }

        ///<summary>
        ///当UI不可用时调用
        /// </summary>
        protected void OnDisable()
        {
            this.Log("OnDisable()");
#if UNITY_EDITOR
            if(m_isOpenedOnce)
            {
                OnClose();
                if(onClose != null)
                {
                    onClose();
                    onClose = null;
                }
            }
#endif
            if(m_btnClose != null)
            {
                m_btnClose.onClick.RemoveAllListeners();
            }
        }

        ///<summary>
        ///当点击关闭按钮时调用
        ///但是并不是每一个window都有关闭按钮
        /// </summary>
        private void OnBtnClose()
        {
            this.Log("OnBtnClose()");
            Close();
        }

        ///<summary>
        ///调用它打开UIWindow
        /// </summary>
        public sealed override void Open(object arg = null)
        {
            base.Open(arg);
            this.Log("Open() arg:{0}", arg);
            m_openArg = arg;
            m_isOpenedOnce = false;
            if(!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
            m_isOpenedOnce = true;
        }

        public sealed override void Close()
        {
            base.Close();
            this.Log("Close()");
            if(this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose();

            if(onClose != null)
            {
                onClose();
                onClose = null;
            }
        }

    }
}
