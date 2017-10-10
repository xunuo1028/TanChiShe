using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snaker.Module.Service.Core
{
    public class ModuleManager : ServiceModule<ModuleManager>
    {
        class MessageObject
        {
            public string target;
            public string msg;
            public object[] args;
        }

        private Dictionary<string, BusinessModule> m_mapModules;
        private Dictionary<string, EventTable> m_mapPreListenEvents;
        private Dictionary<string, List<MessageObject>> m_mapCacheMessage;

        private string m_domain;

        public ModuleManager()
        {
            m_mapModules = new Dictionary<string, BusinessModule>();
            m_mapCacheMessage = new Dictionary<string, List<MessageObject>>();
            m_mapPreListenEvents = new Dictionary<string, EventTable>();
        }

        public void Init(string domain = "Snaker.Module.Service.Core.")
        {
            CheckSingleton();
            m_domain = domain;
        }

        public T CreateModule<T>(object args = null) where T : BusinessModule
        {
            return (T)CreateModule(typeof(T).Name, args);
        }

        public BusinessModule CreateModule(string name, object args = null)
        {
            if(m_mapModules.ContainsKey(name))
            {
                return null;
            }

            BusinessModule module = null;
            Type type = Type.GetType(m_domain + "." + name);
            if(type != null)
            {
                module = Activator.CreateInstance(type) as BusinessModule;
            }
            else
            {
                module = new LuaModule(name);
            }
            m_mapModules.Add(name, module);

            //处理预监听的事件
            if(m_mapPreListenEvents.ContainsKey(name))
            {
                EventTable tblEvent = m_mapPreListenEvents[name];
                m_mapPreListenEvents.Remove(name);

                module.SetEventTable(tblEvent);
            }
            module.Create(args);

            //处理缓存的消息
            if(m_mapCacheMessage.ContainsKey(name))
            {
                List<MessageObject> list = m_mapCacheMessage[name];
                for(int i = 0; i < list.Count; i++)
                {
                    MessageObject msgobj = list[i];
                    module.HandleMessage(msgobj.msg, msgobj.args);
                }
                m_mapCacheMessage.Remove(name);
            }

            return module;
        }

        public void ReleaseModule(BusinessModule module)
        {
            if(module != null)
            {
                if(m_mapModules.ContainsKey(module.Name))
                {
                    m_mapModules.Remove(module.Name);
                    module.Release();
                }
                else
                {

                }
            }
        }

        public void ReleaseAll()
        {
            foreach(var @event in m_mapPreListenEvents)
            {
                @event.Value.Clear();
            }

            m_mapPreListenEvents.Clear();

            m_mapCacheMessage.Clear();

            foreach(var module in m_mapModules)
            {
                module.Value.Release();
            }

            m_mapModules.Clear();
        }

        public T GetModule<T>() where T:BusinessModule
        {
            return (T)GetModule(typeof(T).Name);
        }

        public BusinessModule GetModule(string name)
        {
            if(m_mapModules.ContainsKey(name))
            {
                return m_mapModules[name];
            }
            return null;
        }

        //====================================================
        public void SendMessage(string target, string msg, params object[] args)
        {
            BusinessModule module = GetModule(target);
            if(module != null)
            {
                module.HandleMessage(msg, args);
            }
            else
            {
                List<MessageObject> list = m_mapCacheMessage[target];
                MessageObject msgobj = new MessageObject();
                list.Add(msgobj);

                msgobj.target = target;
                msgobj.msg = msg;
                msgobj.args = args;

            }
        } 

        private List<MessageObject> GetCacheMessageList(string target)
        {
            List<MessageObject> list = null;
            if(m_mapCacheMessage.ContainsKey(target))
            {
                list = new List<MessageObject>();
                m_mapCacheMessage.Add(target, list);
            }
            else
            {
                list = m_mapCacheMessage[target];
            }
            return list;
        }

        //====================================================
        public ModuleEvent Event(string target, string type)
        {
            ModuleEvent evt = null;
            BusinessModule module = GetModule(target);
            if(module != null)
            {
                module.Event(type);
            }
            else
            {
                EventTable table = GetPreListenEventTable(target);
                evt = table.GetEvent(type);
            }

            return evt;
        }

        private EventTable GetPreListenEventTable(string target)
        {
            EventTable table = null;
            if(!m_mapPreListenEvents.ContainsKey(target))
            {
                table = new EventTable();
                m_mapPreListenEvents.Add(target, table);
            }
            else
            {
                table = m_mapPreListenEvents[target];
            }
            return table;
        }
    }
}
