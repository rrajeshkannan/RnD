using System;

namespace Tiger.MetroTransport.Domain.RouteMap
{
    public class Zone
    {
        public Int64 Id { get; }

        public string Name { get; private set; }

        public Zone(Int64 id, string name) => (Id, Name) = (id, name);

        public override int GetHashCode() => (int)Id;

        public override string ToString() => $"[{Id}]<Name:{Name}>";
    }
}