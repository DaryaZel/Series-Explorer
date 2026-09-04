import {
  useQuery,
  useQueryClient,
  type QueryKey,
  type UseQueryOptions,
  type UseQueryResult,
} from '@tanstack/react-query';
import { useCallback } from 'react';
import type { RequestSignalOptions } from '@/types/http';

type QueryRequest<TRequestOptions, TResponse> = (
  options: TRequestOptions & RequestSignalOptions,
) => Promise<TResponse>;

interface UseRequestQueryParams<TRequestParams, TResponse> {
  queryKey: QueryKey;
  request: QueryRequest<TRequestParams, TResponse>;
  requestParams?: TRequestParams;
  enabled?: boolean;
  queryOptions?: Omit<
    UseQueryOptions<TResponse, Error, TResponse, QueryKey>,
    'queryKey' | 'queryFn' | 'enabled'
  >;
}

type UseRequestQueryResult<TResponse> = UseQueryResult<TResponse, Error> & {
  doRequest: () => Promise<UseQueryResult<TResponse, Error>>;
  resetRequest: () => Promise<void>;
  cancelRequest: () => Promise<void>;
};

export const useRequestQuery = <TRequestParams, TResponse>({
  queryKey,
  request,
  requestParams,
  enabled = true,
  queryOptions,
}: UseRequestQueryParams<TRequestParams, TResponse>): UseRequestQueryResult<TResponse> => {
  const queryClient = useQueryClient();
  const canRunQuery = enabled && Boolean(requestParams);

  const query = useQuery({
    queryKey,
    queryFn: ({ signal }) => {
      if (!requestParams) {
        throw new Error('Request params are required to run this query.');
      }

      return request({ ...requestParams, signal });
    },
    enabled: canRunQuery,
    ...queryOptions,
  });

  const doRequest = useCallback(
    () =>
      query.refetch({
        cancelRefetch: true,
      }),
    [query],
  );

  const resetRequest = useCallback(
    () =>
      queryClient.resetQueries({
        queryKey,
      }),
    [queryClient, queryKey],
  );

  const cancelRequest = useCallback(
    () =>
      queryClient.cancelQueries(
        {
          queryKey,
        },
        {
          silent: true,
        },
      ),
    [queryClient, queryKey],
  );

  return {
    ...query,
    doRequest,
    resetRequest,
    cancelRequest,
  };
};
