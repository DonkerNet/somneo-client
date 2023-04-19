import DemoApiClient from './demo-client';

export default class HttpDemoApiClient implements DemoApiClient {
  private readonly baseUrl: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public async get<TResponseData>(resource: string, identifier?: string): Promise<TResponseData | null> {
    return this.executeRequest<never, TResponseData>(resource, identifier, 'GET');
  }

  public async put<TRequestData, TResponseData>(resource: string, data: TRequestData, identifier?: string): Promise<TResponseData | null> {
    return this.executeRequest<TRequestData, TResponseData>(resource, identifier, 'PUT', data);
  }

  public async post<TRequestData, TResponseData>(resource: string, data: TRequestData, identifier?: string): Promise<TResponseData | null> {
    return this.executeRequest<TRequestData, TResponseData>(resource, identifier, 'POST', data);
  }

  public delete<TResponseData>(resource: string, identifier?: string): Promise<TResponseData | null> {
    return this.executeRequest<never, TResponseData>(resource, identifier, 'DELETE');
  }

  private async executeRequest<TRequestData, TResponseData>(
    resource: string,
    identifier: string | undefined,
    method: string,
    data?: TRequestData
  ): Promise<TResponseData | null> {
    const url = this.createUrl(resource, identifier);

    const headers: Record<string, string> = {
      'Accept': 'application/json'
    };

    let body: string | undefined;

    if (data) {
      headers['Content-Type'] = 'application/json';
      body = JSON.stringify(data);
    }

    const requestInit: RequestInit = {
      method,
      headers,
      body
    };

    const response = await fetch(url, requestInit);

    if (response.status < 200 || response.status >= 300) {
      throw new Error(`Request returned status code ${response.status}.`);
    }

    let result = null;
    const json = await response.text();
    if (json.length > 0)
      result = JSON.parse(json);

    return result;
  }

  private createUrl(resource: string, identifier?: string) {
    let url = `${this.baseUrl}/${resource}`;
    if (identifier)
      url = `${url}/${identifier}`;
    return url;
  }
}