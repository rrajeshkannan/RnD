using System;
//using System.Text.Json.Serialization;
using Tiger.MetroTransport.Domain.RouteMap;

namespace Tiger.Card.FareCalculation.Domain.TariffChart
{
    public class InterZoneFare : Fare
    {
        //[JsonInclude]
        //[JsonPropertyName("ZoneFrom")]
        public Zone From { get; }

        //[JsonInclude]
        //[JsonPropertyName("ZoneTo")]
        public Zone To { get; }

        public InterZoneFare(Int64 id, Zone from, Zone to, decimal peakHours, decimal offPeakHours) : base(id, peakHours, offPeakHours)
            => (From, To) = (from, to);

        #region Model identities

        public override string ToString() => $"[{Id}]<Zone:{From.Name}-{To.Name},Peak:{PeakHours},Off-Peak:{OffPeakHours}>";

        #endregion
    }
}