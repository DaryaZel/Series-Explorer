import { Typography } from '@mui/material';
import { useState } from 'react';
import { ObservationWorkspacePanel } from '@/components/ObservationWorkspacePanel';
import { SeriesWorkspacePanel } from '@/components/SeriesWorkspacePanel';
import { useOnlineStatus } from '@/hooks/useOnlineStatus';
import type { SelectedSeries } from '@/types/series';
import s from './MainPage.module.scss';

export const MainPage = () => {
  const [selectedSeries, setSelectedSeries] = useState<SelectedSeries | null>(null);
  const isOnline = useOnlineStatus();

  return (
    <div className={s.root}>
      <div className={s.header}>
        <Typography variant="h5" component="h1">
          Series Observations
        </Typography>
      </div>

      {!isOnline && (
        <div className={s.networkStatus} role="alert">
          <Typography variant="body2">
            No network connection. Check your connection and try again.
          </Typography>
        </div>
      )}

      <div className={s.content}>
        <SeriesWorkspacePanel selectedSeries={selectedSeries} onSeriesSelect={setSelectedSeries} />
        <ObservationWorkspacePanel selectedSeries={selectedSeries} />
      </div>
    </div>
  );
};
