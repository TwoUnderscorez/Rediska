﻿using Rediska.Protocol;
using Rediska.Protocol.Inputs;

namespace Rediska.Tests.Checks
{
    public sealed class CompositeCheck : Check
    {
        private readonly Check[] checks;

        public CompositeCheck(params Check[] checks)
        {
            this.checks = checks;
        }

        public override void Verify(Input input)
        {
            foreach (var check in checks)
            {
                check.Verify(input);
            }
        }
    }
}