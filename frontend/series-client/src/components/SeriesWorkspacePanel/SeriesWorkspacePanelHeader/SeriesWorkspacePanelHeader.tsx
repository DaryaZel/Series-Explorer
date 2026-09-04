import { Refresh } from '@mui/icons-material';
import { IconButton, Tooltip, Typography } from '@mui/material';
import s from './SeriesWorkspacePanelHeader.module.scss';

interface SeriesWorkspacePanelHeaderProps {
  isRefreshDisabled: boolean;
  onRefresh: () => void;
}

export const SeriesWorkspacePanelHeader = ({
  isRefreshDisabled,
  onRefresh,
}: SeriesWorkspacePanelHeaderProps) => (
  <div className={s.seriesWorkspacePanelHeader}>
    <Typography variant="h6" component="h2">
      Series
    </Typography>

    <Tooltip title="Refresh series">
      <span>
        <IconButton
          aria-label="Refresh series"
          className={s.seriesWorkspacePanelHeaderRefreshButton}
          size="small"
          onClick={onRefresh}
          disabled={isRefreshDisabled}
        >
          <Refresh fontSize="small" />
        </IconButton>
      </span>
    </Tooltip>
  </div>
);
