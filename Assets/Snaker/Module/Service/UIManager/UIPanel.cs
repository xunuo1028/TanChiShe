using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGF;
using UnityEngine;

namespace Snaker.Module.Service.UIManager
{
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void Open(object arg = null)
        {
            this.Log("Open() arg:{0}", arg);
        }

        public virtual void Close()
        {
            this.Log("Close()");
        }

        public bool IsOpen
        {
            get
            {
                return this.gameObject.activeSelf;
            }
        }

        protected virtual void OnClose()
        {
            this.Log("OnClose()");
        }

        protected virtual void OnOpen(object arg = null)
        {
            this.Log("OnOpen()");
        }


    }
}
