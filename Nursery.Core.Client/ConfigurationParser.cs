using LivingThing.Core.Frameworks.Common.Configurations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nursery.Core.Client
{
    public class ConfigurationParser: IConfigurationParser
    {
        IConfiguration Configuration;

        public ConfigurationParser(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual T GetValue<T>(string key, Func<T> defaultValue = null)
        {            
            IConfigurationSection section = Configuration.GetSection(key);
            var val = default(T);
            if (section != null)
            {
                val = section.Get<T>();
                if (!EqualityComparer<T>.Default.Equals(val, default(T)))
                {
                    return val;
                }
            }
            section = null;
            var paths = key.Split(new char[] { '/', '\\' });
            foreach (var path in paths)
            {
                if (section == null)
                {
                    section = Configuration.GetSection(path);
                }
                else
                {
                    section = section.GetSection(path);
                }
                if (section == null)
                    break;
            }
            //            var val = Configuration.GetValue<T>(key);
            if (section != null)
            {
                val = section.Get<T>();
            }
            if (EqualityComparer<T>.Default.Equals(val, default(T)) && defaultValue != null)
            {
                val = defaultValue.Invoke();
            }
            return val;
        }

    }
}
