export default interface DemoApiClient {
  get<TResponseData>(resource: string, identifier?: string): Promise<TResponseData|null>;
  post<TRequestData, TResponseData>(resource: string, data: TRequestData, identifier?: string): Promise<TResponseData|null>;
  put<TRequestData, TResponseData>(resource: string, data: TRequestData, identifier?: string): Promise<TResponseData|null>;
  delete<TResponseData>(resource: string, identifier?: string): Promise<TResponseData|null>;
}