using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snaker.Module.Service.Core
{
    public abstract class ServiceModule<T> : Module where T : ServiceModule<T>, new()
    {
        private static T instance = default(T);

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }

        protected void CheckSingleton()
        {
            if(instance == null)
            {
                var exception = new Exception("ServiceModule<" + typeof(T).Name + ">无法直接实例化，因为它是一个单例");
                throw exception;
            }
        }
    }
}
