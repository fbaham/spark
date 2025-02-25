﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spark.Engine.Core;
using Spark.Engine.Service;

namespace Spark.Service
{
    public class ServiceListener : IServiceListener, ICompositeServiceListener
    {
        private readonly ILocalhost _localhost;
        readonly List<IServiceListener> _listeners;

        public ServiceListener(ILocalhost localhost, IServiceListener[] listeners = null)
        {
            _localhost = localhost;
            if(listeners != null)
                _listeners = new List<IServiceListener>(listeners.AsEnumerable());
        }

        public void Add(IServiceListener listener)
        {
            _listeners.Add(listener);
        }

        [Obsolete("Use InformAsync(IServiceListener, Uri, Entry) instead")]
        private void Inform(IServiceListener listener, Uri location, Entry entry)
        {
            listener.Inform(location, entry);
        }

        private async Task InformAsync(IServiceListener listener, Uri location, Entry entry)
        {
            await listener.InformAsync(location, entry).ConfigureAwait(false);
        }

        public void Clear()
        {
            _listeners.Clear();
        }

        [Obsolete("Use InformAsync(Entry) instead")]
        public void Inform(Entry interaction)
        {
            // todo: what we want is not to send localhost to the listener, but to add the Resource.Base. But that is not an option in the current infrastructure.
            // It would modify interaction.Resource, while 
            foreach (IServiceListener listener in _listeners)
            {
                Uri location = _localhost.GetAbsoluteUri(interaction.Key);
                Inform(listener, location, interaction);
            }
        }

        public async Task InformAsync(Entry interaction)
        {
            // todo: what we want is not to send localhost to the listener, but to add the Resource.Base. But that is not an option in the current infrastructure.
            // It would modify interaction.Resource, while 
            foreach (IServiceListener listener in _listeners)
            {
                Uri location = _localhost.GetAbsoluteUri(interaction.Key);
                await InformAsync(listener, location, interaction).ConfigureAwait(false);
            }
        }

        [Obsolete("Use InformAsync(Uri, Entry) instead")]
        public void Inform(Uri location, Entry entry)
        {
            foreach (IServiceListener listener in _listeners)
            {
                Inform(listener, location, entry);
            }
        }

        public async Task InformAsync(Uri location, Entry interaction)
        {
            foreach (var listener in _listeners)
            {
                await listener.InformAsync(location, interaction).ConfigureAwait(false);
            }
        }
    }
}
