import { createContext } from 'react';
import DemoApiClient from '../api/demo-client';

interface DemoContextValues {
  api: DemoApiClient
}

const DemoContext = createContext<DemoContextValues>({} as DemoContextValues);

export default DemoContext;