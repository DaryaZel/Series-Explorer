import { DataGrid, type GridRowModel } from '@mui/x-data-grid';
import {
  type ObservationGridRow,
  isObservationCellLocked,
  observationColumns,
} from './ObservationGrid.utils';
import s from './ObservationGrid.module.scss';

interface ObservationGridProps {
  rows: ObservationGridRow[];
  onRowUpdate?: (newRow: GridRowModel<ObservationGridRow>) => ObservationGridRow;
}

export const ObservationGrid = ({ rows, onRowUpdate }: ObservationGridProps) => (
  <DataGrid
    classes={{
      root: s.observationGrid,
      cell: s.gridCell,
      'cell--editing': s.editingCell,
      columnHeader: s.columnHeader,
      columnSeparator: s.columnSeparator,
      filler: s.filler,
      row: s.row,
      scrollbarFiller: s.scrollbarFiller,
      withBorderColor: s.withBorderColor,
    }}
    rows={rows}
    columns={observationColumns}
    disableColumnResize
    disableRowSelectionOnClick
    hideFooter={rows.length <= 25}
    pageSizeOptions={[25, 50, 100]}
    isCellEditable={({ field, row }) => !isObservationCellLocked(row, field)}
    processRowUpdate={onRowUpdate}
  />
);
