﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SGF;

namespace Snaker.Module.Service.Core
{
    public abstract class BusinessModule : Module
    {
        private string m_name = string.Empty;
        public string Name
        {
            get
            {
                if(string.IsNullOrEmpty(m_name))
                {
                    m_name = this.GetType().Name;
                }
                return m_name;
            }
        }

        public string Title;

        //=================================================

        public BusinessModule()
        {

        }

        internal BusinessModule(string name)
        {
            m_name = name;
        }

        internal void SetEventTable(EventTable tblEvnet)
        {
            m_tblEvent = tblEvnet;
        }

        private EventTable m_tblEvent;

        public ModuleEvent Event(string type)
        {
            return GetEventTable().GetEvent(type);
        }

        protected EventTable GetEventTable()
        {
            if(m_tblEvent == null)
            {
                m_tblEvent = new EventTable();
            }

            return m_tblEvent;
        }

        //==================================================

        internal void HandleMessage(string msg, object[] args)
        {
            this.Log("HandleMessage() msg:{0}, args{1}", msg, args);

            MethodInfo mi = this.GetType().GetMethod(msg, BindingFlags.NonPublic | BindingFlags.Instance);
            if(mi != null)
            {
                mi.Invoke(this, BindingFlags.NonPublic, null, args, null);
            }
            else
            {
                OnModuleMessage(msg, args);
            }
        }

        protected virtual void OnModuleMessage(string msg, object[] args)
        {
            this.Log("OnModuleMessage() msg:{0}, args{1}", msg, args);
        }

        //================================================
        public virtual void Create(object args = null)
        {
            this.Log("Create() args = {0}", args);
        }

        public override void Release()
        {
            if(m_tblEvent != null)
            {
                m_tblEvent.Clear();
                m_tblEvent = null;
            }
            base.Release();
        }

        public virtual void Show()
        {
            this.Log("Show()");
        }
    }
}
