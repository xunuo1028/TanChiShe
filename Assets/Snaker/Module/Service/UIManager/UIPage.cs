using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SGF;


namespace Snaker.Module.Service.UIManager
{
    public class UIPage : UIPanel
    {
        //返回按钮
        [SerializeField]
        private Button m_btnGoBack;

        //打开UI的参数
        protected object m_openArg;

        //该UI的当前实例是否曾经被打开过
        private bool m_isOpenedOnce;

        //当UIPage被激活时调用
        protected void OnEnable()
        {
            this.Log("OnEnable()");
            if(m_btnGoBack != null)
            {
                m_btnGoBack.onClick.AddListener(OnBtnGoBack);
            }

#if UNITY_EDITOR
            if(m_isOpenedOnce)
            {
                OnOpen(m_openArg);
            }
#endif
        }

        protected void OnDisable()
        {
            this.Log("OnDisable()");
#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                OnClose();
            }
#endif
            if(m_btnGoBack != null)
            {
                m_btnGoBack.onClick.RemoveAllListeners();
            }
        }

        private void OnBtnGoBack()
        {
            this.Log("OnBtnGoBack()");
            UIManager.Instance.GoBackPage();
        }

        public sealed override void Open(object arg = null)
        {
            base.Open(arg);
            this.Log("Open()");
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
        }


    }
}
