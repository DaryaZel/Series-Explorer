import { httpRequest } from '@/services/http/httpRequest';
import type { RequestWithOptions } from '@/types/http';
import type { SeriesTreeResponse } from '@/types/series';

type GetSeriesTreeOptions = Record<never, never>;

export const getSeriesTree: RequestWithOptions<GetSeriesTreeOptions, SeriesTreeResponse> = ({
  signal,
}) =>
  httpRequest.get<SeriesTreeResponse>({
    url: 'api/series/tree',
    signal,
  });
