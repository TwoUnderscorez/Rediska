﻿using System;
using System.Collections.Generic;
using System.IO;
using Rediska.Tests.Reading;

namespace Rediska.Tests
{
    public sealed class TemporaryResponse
    {
        private readonly MemoryStream stream = new MemoryStream();
        private long chunkStart = 0;
        private State state = Start.Singleton;

        public IReadOnlyCollection<Input> Feed(ArraySegment<byte> segment)
        {
            var position = stream.Position;
            stream.Write(segment.Array, segment.Offset, segment.Count);
            stream.Position = position;

            var result = new List<Input>();
            var count = 0;

            while (true)
            {
                count++;
                var newState = state.Transit(stream);
                if (newState.IsTerminal)
                {
                    state = Start.Singleton;
                    result.Add(CreateInput());
                    chunkStart = stream.Position;
                    continue;
                }

                state = newState;

                if (stream.Position == stream.Length)
                {
                    Console.WriteLine(count);
                    return result;
                }
            }

        }

        private Input CreateInput()
        {
            return new PlainInput(
                new BinaryReader(
                    new MemoryStream( // todo dispose
                        stream.GetBuffer(),
                        (int)chunkStart,
                        (int)stream.Position,
                        false
                    )
                )
            );
        }
    }
}