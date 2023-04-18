import DemoApiClient from './demo-client';

export default class HttpEditorApiClient implements DemoApiClient {
  private readonly baseUrl: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }
}