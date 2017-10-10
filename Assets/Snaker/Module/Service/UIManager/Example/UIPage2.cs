using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snaker.Module.Service.UIManager;
using SGF;

namespace Snaker.Module.Service.UIManager.Example
{
    public class UIPage2 : UIPage
    {
        public void OnBtnOpenWnd1()
        {
            UIManager.Instance.OpenWindow<UIWnd1>().onClose += OnWnd1Close;
        }

        private void OnWnd1Close()
        {
            this.Log("OnWnd1Close()");
        }

        public void OnBtnOpenWidget1()
        {
            UIManager.Instance.OpenWidget("UIWidget1");
        }
    }
}
