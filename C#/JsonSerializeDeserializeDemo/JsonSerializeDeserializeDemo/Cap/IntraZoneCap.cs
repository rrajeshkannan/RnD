using System;
using Tiger.MetroTransport.Domain.RouteMap;

namespace JsonSerializeDeserializeDemo.Cap
{
    public class IntraZoneCap
    {
        public Int64 Id { get; }

        public CapKind Kind { get; }

        public Zone Zone { get; }

        public decimal Cap { get; }

        public IntraZoneCap(Int64 id, CapKind kind, Zone zone, decimal cap)
            => (Id, Kind, Zone, Cap) = (id, kind, zone, cap);
    }
}