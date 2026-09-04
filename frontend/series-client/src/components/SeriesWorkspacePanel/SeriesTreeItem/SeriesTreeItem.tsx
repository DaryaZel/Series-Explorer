import { Tooltip, Typography, useMediaQuery } from '@mui/material';
import { TreeItem } from '@mui/x-tree-view/TreeItem';
import clsx from 'clsx';
import type { SeriesTreeNode } from '@/types/series';
import { toTreeItemId } from '../SeriesWorkspacePanel.utils';
import s from './SeriesTreeItem.module.scss';

interface SeriesTreeItemProps {
  node: SeriesTreeNode;
  selectedItemId: string | null;
}

export const SeriesTreeItem = ({ node, selectedItemId }: SeriesTreeItemProps) => {
  const isMobile = useMediaQuery('(width < 768px)');
  const itemId = toTreeItemId(node.id);

  return (
    <TreeItem
      classes={{
        content: clsx(
          s.seriesTreeItemContent,
          itemId === selectedItemId && s.seriesTreeItemContentSelected,
        ),
        label: s.seriesTreeItemLabel,
      }}
      itemId={itemId}
      label={
        <Tooltip title={node.label} placement={isMobile ? 'top' : 'right'} enterDelay={500}>
          <Typography component="span" className={s.seriesTreeItemText} noWrap>
            {node.label}
          </Typography>
        </Tooltip>
      }
    >
      {node.children.map((child) => (
        <SeriesTreeItem node={child} selectedItemId={selectedItemId} key={child.id} />
      ))}
    </TreeItem>
  );
};
