import { SimpleTreeView } from '@mui/x-tree-view/SimpleTreeView';
import type { SelectedSeries, SeriesTreeNode } from '@/types/series';
import { findSeriesByTreeItemId } from '../SeriesWorkspacePanel.utils';
import { SeriesTreeItem } from '../SeriesTreeItem/SeriesTreeItem';
import s from './SeriesTree.module.scss';

interface SeriesTreeProps {
  nodes: SeriesTreeNode[];
  expandedItems: string[];
  selectedItemId: string | null;
  onExpandedItemsChange: (itemIds: string[]) => void;
  onSeriesSelect: (series: SelectedSeries) => void;
}

export const SeriesTree = ({
  nodes,
  expandedItems,
  selectedItemId,
  onExpandedItemsChange,
  onSeriesSelect,
}: SeriesTreeProps) => (
  <SimpleTreeView
    className={s.seriesTree}
    expandedItems={expandedItems}
    selectedItems={selectedItemId}
    expansionTrigger="iconContainer"
    onExpandedItemsChange={(_, itemIds) => {
      onExpandedItemsChange(itemIds);
    }}
    onSelectedItemsChange={(_, itemId) => {
      const selectedSeries = findSeriesByTreeItemId(nodes, itemId);

      if (selectedSeries) {
        onSeriesSelect(selectedSeries);
      }
    }}
  >
    {nodes.map((node) => (
      <SeriesTreeItem node={node} selectedItemId={selectedItemId} key={node.id} />
    ))}
  </SimpleTreeView>
);
