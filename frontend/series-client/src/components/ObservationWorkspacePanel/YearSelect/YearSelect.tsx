import { FormControl, InputLabel, MenuItem, Select, type SelectChangeEvent } from '@mui/material';

interface YearSelectProps {
  years: number[];
  selectedYear: number | null;
  disabled: boolean;
  className?: string;
  onYearChange: (year: number) => void;
}

export const YearSelect = ({
  years,
  selectedYear,
  disabled,
  className,
  onYearChange,
}: YearSelectProps) => {
  const handleYearChange = (event: SelectChangeEvent<string>) => {
    onYearChange(Number(event.target.value));
  };

  return (
    <FormControl size="small" className={className} disabled={disabled || !years.length}>
      <InputLabel id="year-select-label">Year</InputLabel>
      <Select
        labelId="year-select-label"
        label="Year"
        value={String(selectedYear ?? '')}
        onChange={handleYearChange}
      >
        {years.map((year) => (
          <MenuItem value={String(year)} key={year}>
            {year}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};
