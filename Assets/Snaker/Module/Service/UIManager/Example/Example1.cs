using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snaker.Module.Service.UIManager;
using SGF;

namespace Snaker.Module.Service.UIManager.Example
{
    public class Example1
    {
        public void Init()
        {
            UIManager.Instance.Init("ui/Example/");
            UIManager.MainPage = "UIPage1";
            UIManager.Instance.EnterMainPage();
        }
    }
}
