using System;
using System.Collections.Generic;

namespace CycloneDDSManager.DDS
{
    /// <summary>A DDS topic whose managed payload type is known at compile time.</summary>
    public sealed class DdsTopic<T> : IDisposable
    {
        internal DdsTopic(DdsTopic nativeTopic, DdsObjectSchema schema, string idl)
        {
            NativeTopic = nativeTopic ?? throw new ArgumentNullException(nameof(nativeTopic));
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            Idl = idl;
        }

        internal DdsObjectSchema Schema { get; private set; }
        public DdsTopic NativeTopic { get; private set; }
        public int Handle { get { return NativeTopic.Handle; } }
        public string TopicName { get { return Schema.TopicName; } }
        public string TypeName { get { return Schema.DdsTypeName; } }
        public string Idl { get; private set; }

        public void Dispose()
        {
            DdsTopic topic = NativeTopic;
            if (topic == null) return;
            NativeTopic = null;
            topic.Dispose();
        }
    }

    /// <summary>A typed writer that marshals annotated class or struct values.</summary>
    public sealed class DdsWriter<T> : IDisposable
    {
        private readonly DdsObjectMarshaller<T> _marshaller;

        internal DdsWriter(DdsWriter nativeWriter, DdsObjectSchema schema)
        {
            NativeWriter = nativeWriter ?? throw new ArgumentNullException(nameof(nativeWriter));
            _marshaller = new DdsObjectMarshaller<T>(schema);
        }

        public DdsWriter NativeWriter { get; private set; }
        public int Handle { get { return NativeWriter.Handle; } }

        public void Write(T value)
        {
            using (DdsNativeSampleBuffer buffer = _marshaller.Write(value))
                NativeWriter.Write(buffer.Pointer);
        }

        public void WriteAt(T value, long sourceTimestampNanoseconds)
        {
            using (DdsNativeSampleBuffer buffer = _marshaller.Write(value))
                NativeWriter.WriteAt(buffer.Pointer, sourceTimestampNanoseconds);
        }

        public void WriteDispose(T value)
        {
            using (DdsNativeSampleBuffer buffer = _marshaller.Write(value))
                NativeWriter.WriteDispose(buffer.Pointer);
        }

        public void DisposeInstance(T keyValue)
        {
            using (DdsNativeSampleBuffer buffer = _marshaller.Write(keyValue))
                NativeWriter.DisposeInstance(buffer.Pointer);
        }

        public ulong RegisterInstance(T keyValue)
        {
            using (DdsNativeSampleBuffer buffer = _marshaller.Write(keyValue))
                return NativeWriter.RegisterInstance(buffer.Pointer);
        }

        public void UnregisterInstance(T keyValue)
        {
            using (DdsNativeSampleBuffer buffer = _marshaller.Write(keyValue))
                NativeWriter.UnregisterInstance(buffer.Pointer);
        }

        public void Flush() { NativeWriter.Flush(); }

        public void Dispose()
        {
            DdsWriter writer = NativeWriter;
            if (writer == null) return;
            NativeWriter = null;
            writer.Dispose();
        }
    }

    public struct DdsReceivedSample<T>
    {
        internal DdsReceivedSample(T data, DdsSampleInfo info)
        {
            Data = data;
            Info = info;
        }

        public T Data { get; private set; }
        public DdsSampleInfo Info { get; private set; }
    }

    /// <summary>
    /// A typed reader. Native loans are copied into managed values and returned
    /// before Read/Take returns, so class instances remain safe to retain.
    /// </summary>
    public sealed class DdsReader<T> : IDisposable
    {
        private readonly DdsObjectMarshaller<T> _marshaller;

        internal DdsReader(DdsReader nativeReader, DdsObjectSchema schema)
        {
            NativeReader = nativeReader ?? throw new ArgumentNullException(nameof(nativeReader));
            _marshaller = new DdsObjectMarshaller<T>(schema);
        }

        public DdsReader NativeReader { get; private set; }
        public int Handle { get { return NativeReader.Handle; } }

        public IReadOnlyList<DdsReceivedSample<T>> Read(int maxSamples = 32)
        {
            using (DdsLoanedSamples loan = NativeReader.Read(maxSamples))
                return Copy(loan);
        }

        public IReadOnlyList<DdsReceivedSample<T>> Read(DdsStateMask mask, int maxSamples = 32)
        {
            using (DdsLoanedSamples loan = NativeReader.Read(mask, maxSamples))
                return Copy(loan);
        }

        public IReadOnlyList<DdsReceivedSample<T>> Take(int maxSamples = 32)
        {
            using (DdsLoanedSamples loan = NativeReader.Take(maxSamples))
                return Copy(loan);
        }

        public IReadOnlyList<DdsReceivedSample<T>> Take(DdsStateMask mask, int maxSamples = 32)
        {
            using (DdsLoanedSamples loan = NativeReader.Take(mask, maxSamples))
                return Copy(loan);
        }

        public IReadOnlyList<DdsReceivedSample<T>> ReadInstance(ulong instanceHandle, int maxSamples = 32)
        {
            using (DdsLoanedSamples loan = NativeReader.ReadInstance(instanceHandle, maxSamples))
                return Copy(loan);
        }

        public IReadOnlyList<DdsReceivedSample<T>> TakeInstance(ulong instanceHandle, int maxSamples = 32)
        {
            using (DdsLoanedSamples loan = NativeReader.TakeInstance(instanceHandle, maxSamples))
                return Copy(loan);
        }

        public void WaitForHistoricalData(TimeSpan maxWait)
        {
            NativeReader.WaitForHistoricalData(maxWait);
        }

        public void Dispose()
        {
            DdsReader reader = NativeReader;
            if (reader == null) return;
            NativeReader = null;
            reader.Dispose();
        }

        private IReadOnlyList<DdsReceivedSample<T>> Copy(DdsLoanedSamples loan)
        {
            var result = new DdsReceivedSample<T>[loan.Count];
            for (int index = 0; index < loan.Count; index++)
            {
                result[index] = new DdsReceivedSample<T>(
                    _marshaller.Read(loan.GetPointer(index)),
                    loan.GetInfo(index));
            }
            return result;
        }
    }
}
