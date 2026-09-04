import type { RawAxiosRequestHeaders } from 'axios';

export type QueryParamValue = string | number | boolean | null | undefined;
export type QueryParams = Record<string, QueryParamValue>;

export interface RequestSignalOptions {
  signal?: AbortSignal;
}

export type RequestWithOptions<TOptions, TResponse> = (
  options: TOptions & RequestSignalOptions,
) => Promise<TResponse>;

export type RequestWithoutOptions<TResponse> = (
  options?: RequestSignalOptions,
) => Promise<TResponse>;

export interface BaseRequestParams extends RequestSignalOptions {
  url: string;
  headers?: RawAxiosRequestHeaders;
  queryParams?: QueryParams;
}

export type GetDeleteParams = BaseRequestParams;

export interface PostPatchPutParams<RequestData = never> extends BaseRequestParams {
  body?: RequestData;
}
