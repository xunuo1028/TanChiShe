using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGF;

namespace Snaker.Module.Service.UIManager
{
    public class UIWidget : UIPanel
    {
        ///<summary>
        ///打开UI的参数
        /// </summary>
        protected object m_openArg;

        ///<summary>
        ///调用它打开UIWindow
        /// </summary>
        /// <param name = "arg"></param>
        public sealed override void Open(object arg = null)
        {
            base.Open(arg);
            this.Log("Open() arg:{0}", arg);
            m_openArg = arg;
            if(!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen();
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
