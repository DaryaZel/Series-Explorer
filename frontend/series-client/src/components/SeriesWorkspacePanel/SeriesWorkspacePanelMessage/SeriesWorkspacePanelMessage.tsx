import { Typography } from '@mui/material';
import clsx from 'clsx';
import s from './SeriesWorkspacePanelMessage.module.scss';

interface SeriesWorkspacePanelMessageProps {
  children: string;
  tone?: 'default' | 'error';
}

export const SeriesWorkspacePanelMessage = ({
  children,
  tone = 'default',
}: SeriesWorkspacePanelMessageProps) => (
  <Typography
    variant="body2"
    className={clsx(
      s.seriesWorkspacePanelMessage,
      tone === 'error' && s.seriesWorkspacePanelMessageError,
    )}
  >
    {children}
  </Typography>
);
