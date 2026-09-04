import axios, { type AxiosRequestConfig, type Method } from 'axios';
import { apiConfig } from '@/config/apiConfig';
import type { BaseRequestParams, GetDeleteParams, PostPatchPutParams } from '@/types/http';

interface DoRequestParams<RequestData = never> extends BaseRequestParams {
  method: Method;
  body?: RequestData;
}

const REQUEST_TIMEOUT_MS = 10_000;

const httpClient = axios.create({
  baseURL: apiConfig.baseUrl,
  timeout: REQUEST_TIMEOUT_MS,
  headers: {
    Accept: 'application/json',
  },
});

export const httpRequest = {
  get<ResponseData>(params: GetDeleteParams): Promise<ResponseData> {
    return doRequest({ method: 'get', ...params });
  },

  post<ResponseData, RequestData = never>(
    params: PostPatchPutParams<RequestData>,
  ): Promise<ResponseData> {
    return doRequest({ method: 'post', ...params });
  },

  patch<ResponseData, RequestData = never>(
    params: PostPatchPutParams<RequestData>,
  ): Promise<ResponseData> {
    return doRequest({ method: 'patch', ...params });
  },

  put<ResponseData, RequestData = never>(
    params: PostPatchPutParams<RequestData>,
  ): Promise<ResponseData> {
    return doRequest({ method: 'put', ...params });
  },

  delete<ResponseData>(params: GetDeleteParams): Promise<ResponseData> {
    return doRequest({ method: 'delete', ...params });
  },
};

const doRequest = async <ResponseData, RequestData = never>({
  method,
  url,
  headers,
  queryParams,
  body,
  signal,
}: DoRequestParams<RequestData>): Promise<ResponseData> => {
  const config: AxiosRequestConfig<RequestData> = {
    method,
    url,
    headers,
    params: queryParams,
    data: body,
    signal,
  };

  const { data } = await httpClient.request<ResponseData, { data: ResponseData }, RequestData>(
    config,
  );

  return data;
};
