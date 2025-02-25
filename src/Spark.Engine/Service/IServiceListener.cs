﻿using System;
using System.Threading.Tasks;
using Spark.Engine.Core;

namespace Spark.Service
{
    public interface IServiceListener
    {
        [Obsolete("Use InformAsync(Uri, Entry) instead")]
        void Inform(Uri location, Entry interaction);

        Task InformAsync(Uri location, Entry interaction);
    }
}
