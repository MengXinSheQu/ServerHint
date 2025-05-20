// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ServerHint
{
#if EXILED
    using Exiled.API.Interfaces;
#endif

#if EXILED
    public class Config : IConfig
#else
    public class Config
#endif
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; }
    }
}