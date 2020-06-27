﻿namespace Rediska.Commands.PublishSubscribe
{
    using System.Collections.Generic;
    using Protocol;
    using Protocol.Visitors;
    using Utils;

    public sealed class SUBSCRIBE : Command<None>
    {
        private static readonly PlainBulkString name = new PlainBulkString("SUBSCRIBE");

        private readonly IReadOnlyList<Channel> channels;

        public SUBSCRIBE(params Channel[] channels)
            : this(channels as IReadOnlyList<Channel>)
        {
        }

        public SUBSCRIBE(IReadOnlyList<Channel> channels)
        {
            this.channels = channels;
        }

        public override DataType Request => new PlainArray(
            new PrefixedList<DataType>(
                name,
                new ProjectingReadOnlyList<Channel, DataType>(
                    channels,
                    channel => new PlainBulkString(channel.ToBytes())
                )
            )
        );

        // todo parse
        public override Visitor<None> ResponseStructure => ArrayContentExpectation.Singleton.Then(_ => new None());
    }
}