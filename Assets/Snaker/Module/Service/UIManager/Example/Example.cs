using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGF;

namespace Snaker.Module.Service.Core.Example
{
    public class ModuleA : BusinessModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModuleEventB);

            ModuleC.Instance.onEvent.AddListener(OnModuleEventC);
            ModuleC.Instance.DoSomething();

            GlobalEvent.onLogin.AddListener(OnLogin);
        }

        private void OnModuleEventC(object args)
        {
            this.Log("OnModuleEventC() args:{0}", args);
        }

        private void OnModuleEventB(object args)
        {
            this.Log("OnModuleEventB() args:{0}", args);
        }

        private void OnLogin(bool args)
        {
            this.Log("OnLogin() args:{0}", args);
        }
    }

    public class ModuleB : BusinessModule
    {
        public ModuleEvent onModuleEventB { get
            {
                return Event("onModuleEventB");
            }
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            onModuleEventB.Invoke("aaaa");
        }

        protected void MessageFromA_1(int args1, int args2, int args3)
        {
            this.Log("MessageFromA_1() args:{0}, {1}, {2}", args1, args2, args3);
        }

        protected void MessageFromA_2(string args1, int args2)
        {
            this.Log("MessageFromA_2() args:{0}, {1}", args1, args2);
        }

        protected override void OnModuleMessage(string msg, object[] args)
        {
            base.OnModuleMessage(msg, args);
            this.Log("OnModuleMessage() msg:{0}, args:{1}, {2}, {3}", msg, args[0], args[1], args[2]);
        }
    }

    public class ModuleC : ServiceModule<ModuleC>
    {
        public ModuleEvent onEvent = new ModuleEvent();
        public void Init()
        {

        }

        public void DoSomething()
        {
            onEvent.Invoke(null);
        }
    }


    public class Example
    {
        public void Init()
        {
            ModuleC.Instance.Init();
            ModuleManager.Instance.Init("Snaker.Module.Service.Core.Example");

            ModuleManager.Instance.CreateModule("ModuleA");
            ModuleManager.Instance.CreateModule("ModuleB");
        }
    }
}
