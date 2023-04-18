import ReactDOM from 'react-dom/client';
import App, { AppProps } from './app';

const container = document.getElementById('demo-client')!;
const root = ReactDOM.createRoot(container);
const config: AppProps = JSON.parse(container.getAttribute('data-config')!);
root.render(<App {...config} />);