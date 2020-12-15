using System;
//using System.Text.Json.Serialization;
using Tiger.MetroTransport.Domain.RouteMap;

namespace Tiger.Card.FareCalculation.Domain.TariffChart
{
    public class IntraZoneFare : Fare
    {
        //[JsonInclude]
        public Zone Zone { get; }

        public IntraZoneFare(Int64 id, Zone zone, decimal peakHours, decimal offPeakHours) : base(id, peakHours, offPeakHours)
            => Zone = zone;

        #region Model identities

        public override string ToString() => $"[{Id}]<Zone:{Zone.Name},Peak:{PeakHours},Off-Peak:{OffPeakHours}>";

        #endregion
    }
}