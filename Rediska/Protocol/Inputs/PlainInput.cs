﻿using System.IO;
using System.Text;

namespace Rediska.Protocol.Inputs
{
    public sealed class PlainInput : Input
    {
        private readonly BinaryReader reader;

        public PlainInput(BinaryReader reader)
        {
            this.reader = reader;
        }

        public override Magic ReadMagic()
        {
            var @byte = reader.ReadByte();
            return new Magic(@byte);
        }

        public override string ReadSimpleString()
        {
            var result = new StringBuilder();
            while (true)
            {
                var currentChar = reader.ReadByte();
                if (currentChar == '\r')
                {
                    reader.ReadByte();
                    return result.ToString();
                }

                result.Append((char) currentChar);
            }
        }

        public override long ReadInteger()
        {
            var result = 0L;
            var positive = true;
            while (true)
            {
                var currentChar = reader.ReadByte();
                if (currentChar == '-')
                {
                    positive = false;
                }
                else if (currentChar == '\r')
                {
                    reader.ReadByte();
                    return positive 
                        ? result
                        : -result;
                }
                else
                {
                    result = result * 10 + currentChar - '0';
                }
            }
        }

        public override BulkString ReadBulkString()
        {
            var length = ReadInteger();
            if (length < 0)
                return BulkString.Null;

            var content = reader.ReadBytes(checked((int) length));
            ReadCRLF();
            return new PlainBulkString(content);
        }

        private void ReadCRLF()
        {
            reader.ReadByte();
            reader.ReadByte();
        }
    }
}