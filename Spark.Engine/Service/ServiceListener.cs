﻿using Spark.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spark.Service
{
    
    public class ServiceListener : IServiceListener
    {
        List<IServiceListener> listeners;

        public ServiceListener()
        {
            listeners = new List<IServiceListener>();
        }

        public void Add(IServiceListener listener)
        {
            this.listeners.Add(listener);
        }

        private void Inform(IServiceListener listener, Uri location, Interaction interaction)
        {
            try
            {
                listener.Inform(location, interaction);
            }
            catch(Exception e)
            {
                throw e;
                //TODO: Package this up as a sensible error, not just a 500?
            }
        }

        public void Clear()
        {
            listeners.Clear();
        }

        public void Inform(Uri location, Interaction interaction)
        {
            foreach(IServiceListener listener in listeners)
            {
                Inform(listener, location, interaction);
            }
        }
    }
}
