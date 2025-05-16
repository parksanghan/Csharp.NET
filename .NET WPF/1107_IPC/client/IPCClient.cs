using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    public class IPCClient
    {
        private NamedPipeClientStream pipeclient = null;
        private StreamString ss = null;
        public bool start()
        {
            try
            {
                pipeclient =
                            new NamedPipeClientStream(".", "testremocon",
                                PipeDirection.InOut, PipeOptions.None,
                                TokenImpersonationLevel.Impersonation);
                pipeclient.Connect();
                ss = new StreamString(pipeclient);
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool stop()
        {
            try
            {
                pipeclient.Close();
                return true;
            }
            catch { return false; }
        }


        public void senddata(string msg)
        {
            ss.WriteString(msg);
        }


    }


    // Defines the data protocol for reading and writing strings on our stream.
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len;
            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            var inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }
}
    
 
