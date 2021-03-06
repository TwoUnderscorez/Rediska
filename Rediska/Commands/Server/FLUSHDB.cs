﻿namespace Rediska.Commands.Server
{
    using System;
    using System.Collections.Generic;
    using Protocol;
    using Protocol.Visitors;
    using Utils;

    public sealed class FLUSHDB : Command<None>
    {
        private static readonly PlainBulkString name = new PlainBulkString("FLUSHDB");
        private static readonly BulkString[] syncRequest = {name};
        private static readonly BulkString[] asyncRequest = {name, new PlainBulkString("ASYNC")};
        public static FLUSHDB Sync { get; } = new FLUSHDB(FlushMode.Synchronous);
        public static FLUSHDB Async { get; } = new FLUSHDB(FlushMode.Asynchronous);
        private readonly FlushMode mode;

        public FLUSHDB(FlushMode mode)
        {
            if (mode != FlushMode.Synchronous && mode != FlushMode.Asynchronous)
            {
                throw new ArgumentException(
                    $"Must be either Synchronous or Asynchronous, but {mode} found",
                    nameof(mode)
                );
            }

            this.mode = mode;
        }

        public override IEnumerable<BulkString> Request(BulkStringFactory factory) => mode == FlushMode.Synchronous
            ? syncRequest
            : asyncRequest;

        public override Visitor<None> ResponseStructure => OkExpectation.Singleton;
    }
}