﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGF;

namespace Snaker.Module.Service.Core
{
    public abstract class Module
    {
        public virtual void Release()
        {
            this.Log("Release");  
        }
    }
}
