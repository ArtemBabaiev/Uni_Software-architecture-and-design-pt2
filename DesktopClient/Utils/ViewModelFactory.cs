using System;
using System.Collections.Generic;

namespace DesktopClient.Utils
{
    internal static class ViewModelFactory
    {
        private static Dictionary<Type, object> viewModels = new Dictionary<Type, object>();
        private static readonly object _lock = new object();

        public static T GetViewModel<T>()
        {
            var classType = typeof(T);
            if (!viewModels.ContainsKey(classType))
            {
                lock (_lock)
                {
                    if (!viewModels.ContainsKey(classType))
                    {
                        viewModels.Add(classType, Activator.CreateInstance(classType));
                    }
                }
            }

            return (T)viewModels[classType];
        }

    }
}
