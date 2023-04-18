import { Component } from 'react';
import DemoApiClient from './api/demo-client';
import HttpDemoApiClient from './api/http-demo-client';
import DemoContext from './contexts/demo';

export interface AppProps {
  apiBaseUrl: string
}

export default class App extends Component<AppProps> {
  private readonly api: DemoApiClient;

  constructor(props: AppProps) {
    super(props);
    this.api = new HttpDemoApiClient(props.apiBaseUrl);
  }

  render() {
    return (
      <DemoContext.Provider value={{ api: this.api }}>
        Demo client succesfully loaded!
      </DemoContext.Provider>
    );
  }
}