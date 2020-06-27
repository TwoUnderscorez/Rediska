﻿namespace Rediska.Protocol.Visitors
{
    using System;
    using System.Collections.Generic;
    using Array = Protocol.Array;

    public sealed class ArrayContentExpectation : Visitor<IReadOnlyList<DataType>>
    {
        public static ArrayContentExpectation Singleton { get; } = new ArrayContentExpectation();

        public override IReadOnlyList<DataType> Visit(Integer integer) =>
            throw new ArgumentException("Array expected");

        public override IReadOnlyList<DataType> Visit(SimpleString simpleString) =>
            throw new ArgumentException("Array expected");

        public override IReadOnlyList<DataType> Visit(Error error) =>
            throw new ArgumentException("Array expected");

        public override IReadOnlyList<DataType> Visit(Array array) => array;

        public override IReadOnlyList<DataType> Visit(BulkString bulkString) =>
            throw new ArgumentException("Array expected");
    }
}