using System;
using System.IO;
using System.Text;

namespace UdpServerLib
{
    /// <summary>
    /// 간단한 패킷 빌더/파서 유틸리티
    /// 구조: [4byte 헤더 매직] [2byte 커맨드] [4byte 길이] [페이로드]
    /// </summary>
    public class PacketBuilder
    {
        public static readonly byte[] Magic = { 0xAB, 0xCD, 0xEF, 0x01 };

        private readonly MemoryStream _stream;
        private readonly BinaryWriter _writer;

        public PacketBuilder()
        {
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);
        }

        public PacketBuilder Write(byte value) { _writer.Write(value); return this; }
        public PacketBuilder Write(short value) { _writer.Write(value); return this; }
        public PacketBuilder Write(int value) { _writer.Write(value); return this; }
        public PacketBuilder Write(long value) { _writer.Write(value); return this; }
        public PacketBuilder Write(float value) { _writer.Write(value); return this; }
        public PacketBuilder Write(double value) { _writer.Write(value); return this; }
        public PacketBuilder Write(bool value) { _writer.Write(value); return this; }
        public PacketBuilder Write(byte[] data) { _writer.Write(data); return this; }

        public PacketBuilder Write(string value, Encoding encoding = null)
        {
            var enc = encoding ?? Encoding.UTF8;
            var bytes = enc.GetBytes(value ?? string.Empty);
            _writer.Write((ushort)bytes.Length);
            _writer.Write(bytes);
            return this;
        }

        /// <summary>헤더(매직 + 커맨드 + 길이)를 포함한 최종 패킷 빌드</summary>
        public byte[] Build(ushort command = 0)
        {
            var payload = _stream.ToArray();
             var packet = new MemoryStream();
            var pw = new BinaryWriter(packet);

            pw.Write(Magic);
            pw.Write(command);
            pw.Write((int)payload.Length);
            pw.Write(payload);

            return packet.ToArray();
        }

        /// <summary>헤더 없이 raw 바이트만 반환</summary>
        public byte[] BuildRaw() => _stream.ToArray();
    }

    /// <summary>
    /// 수신된 패킷 파서
    /// </summary>
    public class PacketParser
    {
        private readonly BinaryReader _reader;
        public ushort Command { get; }
        public int PayloadLength { get; }
        public bool IsValid { get; }

        public PacketParser(byte[] data)
        {
            if (data == null || data.Length < 10) // 매직(4) + 커맨드(2) + 길이(4) = 10
            {
                IsValid = false;
                return;
            }

            _reader = new BinaryReader(new MemoryStream(data));

            // 매직 검증
            var magic = _reader.ReadBytes(4);
            for (int i = 0; i < 4; i++)
            {
                if (magic[i] != PacketBuilder.Magic[i]) { IsValid = false; return; }
            }

            Command = _reader.ReadUInt16();
            PayloadLength = _reader.ReadInt32();
            IsValid = true;
        }

        /// <summary>Raw 바이트 파싱 (헤더 없이)</summary>
        public static PacketParser FromRaw(byte[] data) =>
            new PacketParser(data) { };

        public byte ReadByte() => _reader.ReadByte();
        public short ReadShort() => _reader.ReadInt16();
        public int ReadInt() => _reader.ReadInt32();
        public long ReadLong() => _reader.ReadInt64();
        public float ReadFloat() => _reader.ReadSingle();
        public double ReadDouble() => _reader.ReadDouble();
        public bool ReadBool() => _reader.ReadBoolean();
        public byte[] ReadBytes(int count) => _reader.ReadBytes(count);

        public string ReadString(Encoding encoding = null)
        {
            var enc = encoding ?? Encoding.UTF8;
            ushort len = _reader.ReadUInt16();
            var bytes = _reader.ReadBytes(len);
            return enc.GetString(bytes);
        }
    }
}
