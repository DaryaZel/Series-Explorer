import { Inventory2Outlined } from '@mui/icons-material';
import { Button, Typography } from '@mui/material';
import s from './ObservationEmptyState.module.scss';

interface ObservationEmptyStateProps {
  title: string;
  description: string;
  actionLabel?: string;
  onAction?: () => void;
}

export const ObservationEmptyState = ({
  title,
  description,
  actionLabel,
  onAction,
}: ObservationEmptyStateProps) => (
  <div className={s.root}>
    <div className={s.content}>
      <Inventory2Outlined className={s.icon} />
      <Typography variant="h6" component="p" className={s.title}>
        {title}
      </Typography>
      <Typography variant="body2" className={s.description}>
        {description}
      </Typography>
      {actionLabel && onAction && (
        <Button
          variant="outlined"
          color="inherit"
          size="small"
          className={s.action}
          onClick={onAction}
        >
          {actionLabel}
        </Button>
      )}
    </div>
  </div>
);
