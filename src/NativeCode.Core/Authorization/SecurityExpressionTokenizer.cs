﻿namespace NativeCode.Core.Authorization
{
    using Core.Types;
    using System.IO;
    using System.Text;

    public class SecurityExpressionTokenizer : Disposable
    {
        private readonly Stream stream;

        public SecurityExpressionTokenizer(string source)
        {
            this.stream = new MemoryStream(Encoding.UTF8.GetBytes(source));
        }

        protected byte[] Buffer { get; } = new byte[4096];

        protected int BufferCount { get; private set; }

        protected int BufferPosition { get; private set; }

        protected Stream Stream => this.stream;

        public Token GetNextToken()
        {
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.stream.Dispose();
            }

            base.Dispose(disposing);
        }

        private void FillBuffer()
        {
            this.BufferCount = this.Stream.Read(this.Buffer, 0, this.Buffer.Length);
        }

        private char MoveNext()
        {
            var next = (char)this.Buffer[this.BufferPosition];
            this.BufferPosition++;

            if (this.BufferCount == this.BufferPosition)
            {
                this.FillBuffer();
            }

            return next;
        }
    }
}
