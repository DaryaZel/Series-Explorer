import { CircularProgress, Typography } from '@mui/material';
import { useMemo, useState } from 'react';
import { useRequestQuery } from '@/hooks/useRequestQuery';
import { getObservations } from '@/services/observations/observationsRequests';
import { queryKeys } from '@/services/queryKeys';
import type { SelectedSeries } from '@/types/series';
import { ObservationGrid } from './ObservationGrid/ObservationGrid';
import {
  normalizeObservationGridRow,
  toObservationGridRows,
} from './ObservationGrid/ObservationGrid.utils';
import { ObservationEmptyState } from './ObservationEmptyState/ObservationEmptyState';
import s from './ObservationWorkspacePanel.module.scss';
import { YearSelect } from './YearSelect/YearSelect';

interface ObservationWorkspacePanelProps {
  selectedSeries: SelectedSeries | null;
}

interface SelectedYearState {
  seriesId: number;
  year: number;
}

export const ObservationWorkspacePanel = ({ selectedSeries }: ObservationWorkspacePanelProps) => {
  const selectedSeriesId = selectedSeries?.id;
  const [selectedYearState, setSelectedYearState] = useState<SelectedYearState | null>(null);
  const selectedYear =
    selectedYearState && selectedYearState.seriesId === selectedSeriesId
      ? selectedYearState.year
      : null;

  const observationsQuery = useRequestQuery({
    queryKey: queryKeys.observations.list({ seriesId: selectedSeriesId, year: selectedYear }),
    request: getObservations,
    requestParams: selectedSeriesId
      ? {
          seriesId: selectedSeriesId,
          year: selectedYear,
        }
      : undefined,
    enabled: Boolean(selectedSeriesId),
    queryOptions: {
      placeholderData: (previousData) =>
        previousData?.seriesId === selectedSeriesId ? previousData : undefined,
    },
  });

  const { data, isLoading: isInitialLoading, isFetching, isError, doRequest } = observationsQuery;
  const isLoading = !isError && isInitialLoading;
  const observationRows = data?.rows;
  const gridRows = useMemo(() => toObservationGridRows(observationRows ?? []), [observationRows]);

  const handleYearChange = (year: number) => {
    if (selectedSeriesId) {
      setSelectedYearState({ seriesId: selectedSeriesId, year });
    }
  };

  const handleRetry = () => {
    void doRequest();
  };

  return (
    <div className={s.root}>
      <div className={s.header}>
        <div className={s.heading}>
          <Typography variant="h6" component="h2">
            Observations
          </Typography>
          <Typography variant="body2" className={s.subtitle}>
            {selectedSeries?.label ?? 'Select a series to view observations.'}
          </Typography>
        </div>

        <YearSelect
          className={s.yearSelect}
          years={data?.availableYears ?? []}
          selectedYear={selectedYear ?? data?.selectedYear ?? null}
          disabled={!selectedSeriesId || isInitialLoading || isFetching}
          onYearChange={handleYearChange}
        />
      </div>

      <div className={s.body}>
        <div className={s.gridShell}>
          {(!selectedSeriesId || (!isLoading && !isError && !gridRows.length)) && (
            <div className={s.gridBody}>
              <ObservationEmptyState
                title="No observation data"
                description="Select a series or choose another year with observation data."
              />
            </div>
          )}

          {selectedSeriesId && isLoading && (
            <div className={s.loadingBody}>
              <CircularProgress
                className={s.loadingSpinner}
                color="inherit"
                size={32}
                aria-label="Loading observations"
              />
            </div>
          )}

          {selectedSeriesId && isError && (
            <div className={s.gridBody}>
              <ObservationEmptyState
                title="Could not load data"
                description="Please try again."
                actionLabel="Try again"
                onAction={handleRetry}
              />
            </div>
          )}

          {selectedSeriesId && !isLoading && !isError && gridRows.length > 0 && (
            <ObservationGrid rows={gridRows} onRowUpdate={normalizeObservationGridRow} />
          )}
        </div>
      </div>
    </div>
  );
};
