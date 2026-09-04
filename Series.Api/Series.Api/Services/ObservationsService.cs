using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Series.Api.Data;
using Series.Api.Dtos;
using Series.Api.Models;

namespace Series.Api.Services
{
    public class ObservationsService : IObservationsService
    {
        private const string SalesOpinion = "QY_SALES";
        private const string DemandOpinion = "QY_DEMAND";
        private const string SupplyOpinion = "QY_SUPPLY";

        private readonly SeriesDbContext _context;

        public ObservationsService(SeriesDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public ObservationsResponseDto GetObservations(int seriesId, int? year)
        {
            var availableYears = GetAvailableYears(seriesId);
            var selectedYear = year.HasValue ? year.Value : (int?)availableYears.LastOrDefault();

            var response = new ObservationsResponseDto
            {
                SeriesId = seriesId,
                AvailableYears = availableYears,
                SelectedYear = selectedYear == 0 ? null : selectedYear,
                Rows = new List<ObservationRowDto>()
            };

            if (!response.SelectedYear.HasValue)
            {
                return response;
            }

            var observations = GetObservationsForYear(seriesId, response.SelectedYear.Value);
            var lockedPeriods = GetLockedPeriodsForYear(seriesId, response.SelectedYear.Value);
            var lockedLookup = CreateLockedLookup(lockedPeriods);

            response.Rows = observations
                .Select(observation => ToDto(observation, lockedLookup))
                .ToList();

            return response;
        }

        private List<int> GetAvailableYears(int seriesId)
        {
            return _context.Observations
                .AsNoTracking()
                .Where(observation => observation.SeriesId == seriesId)
                .Select(observation => observation.Period.Substring(0, 4))
                .Distinct()
                .ToList()
                .Select(int.Parse)
                .OrderBy(year => year)
                .ToList();
        }

        private List<ObservationRecord> GetObservationsForYear(int seriesId, int year)
        {
            string yearPrefix = GetYearPrefix(year);

            return _context.Observations
                .AsNoTracking()
                .Where(observation =>
                    observation.SeriesId == seriesId &&
                    observation.Period.StartsWith(yearPrefix))
                .OrderBy(observation => observation.Period)
                .ToList();
        }

        private List<LockedPeriodRecord> GetLockedPeriodsForYear(int seriesId, int year)
        {
            string yearPrefix = GetYearPrefix(year);

            return _context.LockedPeriods
                .AsNoTracking()
                .Where(lockedPeriod =>
                    lockedPeriod.SeriesId == seriesId &&
                    lockedPeriod.Period.StartsWith(yearPrefix))
                .OrderBy(lockedPeriod => lockedPeriod.Period)
                .ThenBy(lockedPeriod => lockedPeriod.Opinion)
                .ToList();
        }

        private static ObservationRowDto ToDto(
            ObservationRecord observation,
            ISet<string> lockedLookup)
        {
            return new ObservationRowDto
            {
                Id = observation.Period,
                Period = observation.Period,
                Sales = ToCellDto(observation.Sales, observation.Period, SalesOpinion, lockedLookup),
                Demand = ToCellDto(observation.Demand, observation.Period, DemandOpinion, lockedLookup),
                Supply = ToCellDto(observation.Supply, observation.Period, SupplyOpinion, lockedLookup)
            };
        }

        private static ObservationCellDto ToCellDto(
            double? value,
            string period,
            string opinion,
            ISet<string> lockedLookup)
        {
            return new ObservationCellDto
            {
                Value = value,
                Locked = lockedLookup.Contains(GetLockedKey(period, opinion))
            };
        }

        private static ISet<string> CreateLockedLookup(IEnumerable<LockedPeriodRecord> lockedPeriods)
        {
            return new HashSet<string>(
                lockedPeriods.Select(item => GetLockedKey(item.Period, item.Opinion)));
        }

        private static string GetLockedKey(string period, string opinion)
        {
            return period + "|" + opinion;
        }

        private static string GetYearPrefix(int year)
        {
            return year.ToString("0000");
        }
    }
}
