﻿using System;
using System.Threading.Tasks;
using Spark.Engine.Core;

namespace Spark.Engine.Service.FhirServiceExtensions
{
    public interface IInteractionHandler
    {
        [Obsolete("Use HandleInteractionAsync(Entry) instead")]
        FhirResponse HandleInteraction(Entry interaction);
        Task<FhirResponse> HandleInteractionAsync(Entry interaction);
    }
}
