import type { GridColDef, GridRowModel } from '@mui/x-data-grid';
import clsx from 'clsx';
import type { ObservationRow } from '@/types/observations';
import s from './ObservationGrid.module.scss';

const editableFields = ['sales', 'demand', 'supply'] as const;

type EditableObservationField = (typeof editableFields)[number];
type EditableObservationValue = number | string | null;

export type ObservationGridRow = {
  id: string;
  period: string;
  sales: number | null;
  demand: number | null;
  supply: number | null;
  locked: Record<EditableObservationField, boolean>;
};

const renderNullableObservationValue = ({ value }: { value?: number | null }) =>
  String(value ?? 'null');

export const toObservationGridRows = (rows: ObservationRow[]): ObservationGridRow[] =>
  rows.map((row) => ({
    id: row.id,
    period: row.period,
    sales: row.sales.value,
    demand: row.demand.value,
    supply: row.supply.value,
    locked: {
      sales: row.sales.locked,
      demand: row.demand.locked,
      supply: row.supply.locked,
    },
  }));

export const observationColumns: GridColDef<ObservationGridRow>[] = [
  {
    field: 'period',
    headerName: 'Period',
    headerAlign: 'center',
    sortable: false,
    disableColumnMenu: true,
    resizable: false,
    headerClassName: s.dividerColumn,
    cellClassName: s.dividerColumn,
    minWidth: 130,
    flex: 1.2,
  },
  {
    field: 'sales',
    headerName: 'Sales',
    headerAlign: 'center',
    align: 'right',
    sortable: false,
    disableColumnMenu: true,
    resizable: false,
    editable: true,
    renderCell: renderNullableObservationValue,
    headerClassName: s.dividerColumn,
    cellClassName: ({ row }) => clsx(s.dividerColumn, row.locked.sales && s.lockedCell),
    minWidth: 120,
    flex: 1,
  },
  {
    field: 'demand',
    headerName: 'Demand',
    headerAlign: 'center',
    align: 'right',
    sortable: false,
    disableColumnMenu: true,
    resizable: false,
    editable: true,
    renderCell: renderNullableObservationValue,
    headerClassName: s.dividerColumn,
    cellClassName: ({ row }) => clsx(s.dividerColumn, row.locked.demand && s.lockedCell),
    minWidth: 120,
    flex: 1,
  },
  {
    field: 'supply',
    headerName: 'Supply',
    headerAlign: 'center',
    align: 'right',
    sortable: false,
    disableColumnMenu: true,
    resizable: false,
    editable: true,
    renderCell: renderNullableObservationValue,
    cellClassName: ({ row }) => clsx(row.locked.supply && s.lockedCell),
    minWidth: 120,
    flex: 1,
  },
];

export const isEditableObservationField = (field: string): field is EditableObservationField =>
  editableFields.includes(field as EditableObservationField);

export const isObservationCellLocked = (row: ObservationGridRow, field: string) => {
  if (!isEditableObservationField(field)) {
    return false;
  }

  return row.locked[field];
};

export const normalizeObservationGridRow = (
  row: GridRowModel<ObservationGridRow>,
): ObservationGridRow => {
  const observationRow = row as ObservationGridRow;

  return {
    ...observationRow,
    sales: toObservationNumber(observationRow.sales),
    demand: toObservationNumber(observationRow.demand),
    supply: toObservationNumber(observationRow.supply),
  };
};

const toObservationNumber = (value: EditableObservationValue): number | null => {
  if (typeof value === 'number') {
    return Number.isFinite(value) ? value : null;
  }

  if (typeof value === 'string') {
    const trimmedValue = value.trim();

    if (trimmedValue === '') {
      return null;
    }

    const numericValue = Number(trimmedValue);

    return Number.isFinite(numericValue) ? numericValue : null;
  }

  return null;
};
