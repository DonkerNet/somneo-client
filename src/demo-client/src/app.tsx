import { Component } from 'react';
import DemoApiClient from './api/demo-client';
import HttpDemoApiClient from './api/http-demo-client';
import DemoContext from './contexts/demo';
import { ViewNames, createView } from './components/views/factory';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { Box, Container, Typography } from '@mui/material';

export interface AppProps {
  apiBaseUrl: string
}

interface AppState {
  selectedView: string
}

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});


export default class App extends Component<AppProps, AppState> {
  private readonly api: DemoApiClient;

  constructor(props: AppProps) {
    super(props);
    this.api = new HttpDemoApiClient(props.apiBaseUrl);
    this.state = { selectedView: ViewNames.OVERVIEW };
  }

  changeView = (name: string) => {
    this.setState({
      ...this.state,
      selectedView: name
    });
  }

  render() {
    const view = createView(this.state.selectedView, { changeView: this.changeView });

    return (
      <DemoContext.Provider value={{ api: this.api }}>
        <ThemeProvider theme={darkTheme}>
          <CssBaseline />
          <Container maxWidth="sm">
            <Box mt={2} mb={2}>
              {view}
            </Box>
          </Container>
        </ThemeProvider>
      </DemoContext.Provider>
    );
  }
}