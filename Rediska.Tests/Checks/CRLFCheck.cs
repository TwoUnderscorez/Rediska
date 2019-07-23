﻿using System;
using Rediska.Protocol;

namespace Rediska.Tests.Checks
{
    public sealed class CRLFCheck : Check
    {
        public static CRLFCheck Singleton { get; } = new CRLFCheck();

        public override void Verify(Input input)
        {
            throw new Exception("Kill CRLFCheck");
        }
    }
}