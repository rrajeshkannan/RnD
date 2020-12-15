using System;

namespace Tiger.Card.FareCalculation.Domain.TariffChart
{
    public abstract class Fare
    {
        public Int64 Id { get; }

        public decimal PeakHours { get; }

        public decimal OffPeakHours { get; }

        public Fare(Int64 id, decimal peakHours, decimal offPeakHours) 
            => (Id, PeakHours, OffPeakHours) = (id, peakHours, offPeakHours);

        #region Model identities

        public override int GetHashCode() => (int)Id;

        public override string ToString() => $"[{Id}]<Peak:{PeakHours},Off-Peak:{OffPeakHours}>";

        #endregion
    }
}