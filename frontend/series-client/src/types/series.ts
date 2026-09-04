export interface SeriesTreeNode {
  id: number;
  label: string;
  children: SeriesTreeNode[];
}

export interface SeriesTreeResponse {
  nodes: SeriesTreeNode[];
}

export interface SelectedSeries {
  id: number;
  label: string;
}
