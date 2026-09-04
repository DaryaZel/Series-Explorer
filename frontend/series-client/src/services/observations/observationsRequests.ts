import { httpRequest } from '@/services/http/httpRequest';
import type { RequestWithOptions } from '@/types/http';
import type { ObservationsResponse } from '@/types/observations';

export interface GetObservationsOptions {
  seriesId: number;
  year?: number | null;
}

export const getObservations: RequestWithOptions<GetObservationsOptions, ObservationsResponse> = ({
  seriesId,
  year,
  signal,
}) =>
  httpRequest.get<ObservationsResponse>({
    url: 'api/observations',
    queryParams: {
      seriesId,
      year,
    },
    signal,
  });
