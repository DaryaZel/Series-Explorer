import type { SelectedSeries, SeriesTreeNode } from '@/types/series';

export const toTreeItemId = (seriesId: number) => String(seriesId);

export const findSeriesByTreeItemId = (
  nodes: SeriesTreeNode[],
  itemId: string | null,
): SelectedSeries | null => {
  if (!itemId) {
    return null;
  }

  for (const node of nodes) {
    if (toTreeItemId(node.id) === itemId) {
      return {
        id: node.id,
        label: node.label,
      };
    }

    const childMatch = findSeriesByTreeItemId(node.children, itemId);

    if (childMatch) {
      return childMatch;
    }
  }

  return null;
};
