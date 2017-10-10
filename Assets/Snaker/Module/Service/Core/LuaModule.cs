using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snaker.Module.Service.Core
{
    public class LuaModule : BusinessModule
    {
        private object m_args = null;

        internal LuaModule(string name) : base(name)
        {

        }

        public override void Create(object args = null)
        {
            base.Create(args);
            m_args = args;

            //加载Name所对应的Lua脚本

        }

        public override void Release()
        {
            base.Release();
            //释放Name所对应的Lua脚本

        }
    }
}
