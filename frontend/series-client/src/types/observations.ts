export interface ObservationCell {
  value: number | null;
  locked: boolean;
}

export interface ObservationRow {
  id: string;
  period: string;
  sales: ObservationCell;
  demand: ObservationCell;
  supply: ObservationCell;
}

export interface ObservationsResponse {
  seriesId: number;
  availableYears: number[];
  selectedYear: number | null;
  rows: ObservationRow[];
}
