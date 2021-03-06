﻿using System.Linq;
using Rediska.Protocol;
using Rediska.Protocol.Outputs;

namespace Rediska.Tests
{
    public sealed class Content
    {
        private readonly DataType data;

        public Content(DataType data)
        {
            this.data = data;
        }

        public byte[] AsBytes()
        {
            // todo fix
            var content = new byte[1024 * 1024 * 128];
            var plainOutput = new PlainOutput(content);
            data.Write(plainOutput);
            return content.Take(plainOutput.Position).ToArray();
        }
    }
}