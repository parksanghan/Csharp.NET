using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace UdpServerLib
{
    /// <summary>
    /// UDP 클라이언트 세션 정보
    /// </summary>
    public class UdpSession
    {
        // 기존 코드: public string SessionId { get; } = Guid.NewGuid().ToString("N")[..8];
        // C# 7.3 이하 호환을 위해 Substring 사용
        public string SessionId { get; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public IPEndPoint EndPoint { get; }
        public DateTime FirstSeen { get; } = DateTime.Now;
        public DateTime LastSeen { get; private set; } = DateTime.Now;
        public long ReceivedPackets { get; private set; }
        public long ReceivedBytes { get; private set; }

        // 사용자 정의 데이터 저장용
        public ConcurrentDictionary<string, object> Properties { get; } = new ConcurrentDictionary<string, object>();

        public UdpSession(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
        }

        internal void Update(int dataLength)
        {
            LastSeen = DateTime.Now;
            ReceivedPackets++;
            ReceivedBytes += dataLength;
        }

        public override string ToString() =>
            $"[{SessionId}] {EndPoint} | 패킷: {ReceivedPackets} | 마지막: {LastSeen:HH:mm:ss}";
    }

    /// <summary>
    /// UDP 세션 관리자 - UdpServer와 함께 사용
    /// </summary>
    public class UdpSessionManager
    {
        private readonly ConcurrentDictionary<string, UdpSession> _sessions = new ConcurrentDictionary<string, UdpSession>();
        private readonly TimeSpan _sessionTimeout;

        public event EventHandler<UdpSession> SessionCreated;
        public event EventHandler<UdpSession> SessionExpired;

        public int SessionCount => _sessions.Count;
        public TimeSpan SessionTimeout => _sessionTimeout;

        public UdpSessionManager(TimeSpan? sessionTimeout = null)
        {
            _sessionTimeout = sessionTimeout ?? TimeSpan.FromMinutes(5);
        }

        /// <summary>수신 패킷 기준으로 세션 업데이트 (없으면 생성)</summary>
        public UdpSession GetOrCreate(IPEndPoint endPoint, int dataLength)
        {
            string key = endPoint.ToString();

            if (!_sessions.TryGetValue(key, out var session))
            {
                session = new UdpSession(endPoint);
                if (_sessions.TryAdd(key, session))
                    SessionCreated?.Invoke(this, session);
                else
                    _sessions.TryGetValue(key, out session);
            }

            session?.Update(dataLength);
            return session;
        }

        public UdpSession GetSession(IPEndPoint endPoint)
        {
            _sessions.TryGetValue(endPoint.ToString(), out var session);
            return session;
        }

        public IEnumerable<UdpSession> GetAllSessions() => _sessions.Values;

        /// <summary>타임아웃된 세션 정리</summary>
        public int CleanupExpired()
        {
            int count = 0;
            var now = DateTime.Now;

            foreach (var kv in _sessions)
            {
                if (now - kv.Value.LastSeen > _sessionTimeout)
                {
                    if (_sessions.TryRemove(kv.Key, out var removed))
                    {
                        count++;
                        SessionExpired?.Invoke(this, removed);
                    }
                }
            }
            return count;
        }

        public bool RemoveSession(IPEndPoint endPoint) =>
            _sessions.TryRemove(endPoint.ToString(), out _);

        public void Clear() => _sessions.Clear();
    }
}
