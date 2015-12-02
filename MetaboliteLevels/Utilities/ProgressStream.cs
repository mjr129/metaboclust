using System;
using System.IO;
using System.Threading.Tasks;

namespace MetaboliteLevels.Utilities
{
    class ProgressStream : Stream
    {
        private Stream _stream;
        private IProgressReporter _progressHandler;

        public ProgressStream(Stream stream, IProgressReporter progressHandler)
        {
            _stream = stream;
            _progressHandler = progressHandler;
        }

        private void ReportProgress()
        {
            if (_progressHandler != null)
            {
                // Convert to percentage now to avoid large int errors later
                _progressHandler.ReportProgress((int)((_stream.Position * 100) / _stream.Length), 100);
            }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override bool CanTimeout
        {
            get
            {
                return _stream.CanTimeout;
            }
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override void Close()
        {
            _stream.Close();
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, System.Threading.CancellationToken cancellationToken)
        {
            return _stream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
        {
            return _stream.CreateObjRef(requestedType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _stream.Dispose();
            }

            base.Dispose(disposing);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _stream.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _stream.EndWrite(asyncResult);
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override Task FlushAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _stream.FlushAsync(cancellationToken);
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result = _stream.Read(buffer, offset, count);
            ReportProgress();
            return result;
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            return _stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override long Position
        {
            get
            {
                return _stream.Position;
            }
            set
            {
                _stream.Position = value;
            }
        }

        public override int ReadByte()
        {
            return _stream.ReadByte();
        }

        public override int ReadTimeout
        {
            get
            {
                return _stream.ReadTimeout;
            }
            set
            {
                _stream.ReadTimeout = value;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override string ToString()
        {
            return _stream.ToString();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, System.Threading.CancellationToken cancellationToken)
        {
            return _stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            _stream.WriteByte(value);
        }

        public override int WriteTimeout
        {
            get
            {
                return _stream.WriteTimeout;
            }
            set
            {
                _stream.WriteTimeout = value;
            }
        }
    }

}
