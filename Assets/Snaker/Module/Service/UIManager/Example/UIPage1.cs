using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snaker.Module.Service.UIManager;
using Snaker.Module.Service.Core;

namespace Snaker.Module.Service.UIManager.Example
{
    public class UIPage1 : UIPage
    {
        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
        }

        public void OnBtnOpenPage2()
        {
            UIManager.Instance.OpenPage("UIPage2");
        }
    }
}
