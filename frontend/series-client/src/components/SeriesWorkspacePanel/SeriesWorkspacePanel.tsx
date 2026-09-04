import { useState } from 'react';
import { useRequestQuery } from '@/hooks/useRequestQuery';
import { queryKeys } from '@/services/queryKeys';
import { getSeriesTree } from '@/services/series/seriesRequests';
import type { SelectedSeries } from '@/types/series';
import { toTreeItemId } from './SeriesWorkspacePanel.utils';
import { SeriesWorkspacePanelHeader } from './SeriesWorkspacePanelHeader/SeriesWorkspacePanelHeader';
import { SeriesWorkspacePanelMessage } from './SeriesWorkspacePanelMessage/SeriesWorkspacePanelMessage';
import { SeriesTree } from './SeriesTree/SeriesTree';
import s from './SeriesWorkspacePanel.module.scss';

interface SeriesWorkspacePanelProps {
  selectedSeries: SelectedSeries | null;
  onSeriesSelect: (series: SelectedSeries) => void;
}

export const SeriesWorkspacePanel = ({
  selectedSeries,
  onSeriesSelect,
}: SeriesWorkspacePanelProps) => {
  const [expandedItems, setExpandedItems] = useState<string[]>([]);

  const seriesTreeQuery = useRequestQuery({
    queryKey: queryKeys.series.tree,
    request: getSeriesTree,
    requestParams: {},
  });

  const { data, isLoading: isInitialLoading, isFetching, isError, doRequest } = seriesTreeQuery;
  const nodes = data?.nodes ?? [];
  const isLoading = !isError && isInitialLoading;
  const selectedItemId = selectedSeries ? toTreeItemId(selectedSeries.id) : null;

  return (
    <div className={s.seriesWorkspacePanel}>
      <SeriesWorkspacePanelHeader
        isRefreshDisabled={isInitialLoading || isFetching}
        onRefresh={() => {
          void doRequest();
        }}
      />

      <div className={s.seriesWorkspacePanelBody}>
        {isLoading && <SeriesWorkspacePanelMessage>Loading series...</SeriesWorkspacePanelMessage>}

        {isError && (
          <SeriesWorkspacePanelMessage tone="error">
            Could not load data. Please try again.
          </SeriesWorkspacePanelMessage>
        )}

        {!isLoading && !isError && !nodes.length && (
          <SeriesWorkspacePanelMessage>No series available.</SeriesWorkspacePanelMessage>
        )}

        {!isLoading && !isError && nodes.length > 0 && (
          <SeriesTree
            nodes={nodes}
            expandedItems={expandedItems}
            selectedItemId={selectedItemId}
            onExpandedItemsChange={setExpandedItems}
            onSeriesSelect={onSeriesSelect}
          />
        )}
      </div>
    </div>
  );
};
