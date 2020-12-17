using System;
using Tiger.MetroTransport.Domain.RouteMap;

namespace JsonSerializeDeserializeDemo.Cap
{
    public class InterZoneCap
    {
        public Int64 Id { get; }

        public CapKind Kind { get; }

        public Zone From { get; }

        public Zone To { get; }

        public decimal Cap { get; }

        public InterZoneCap(Int64 id, CapKind kind, Zone from, Zone to, decimal cap)
            => (Id, Kind, From, To, Cap) = (id, kind, from, to, cap);
    }
}