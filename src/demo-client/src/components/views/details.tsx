import { Card, CardContent, Grid, Typography } from '@mui/material';
import BaseView, { BaseViewProps } from './base';
import DetailsModel from 'src/models/details';
import TitleBar from '../title-bar';
import { ViewNames } from './factory';

interface DetailsViewState {
  details: DetailsModel | null
}

export default class DetailsView extends BaseView<BaseViewProps, DetailsViewState> {
  constructor(props: BaseViewProps) {
    super(props);
    this.state = { details: null };
  }

  componentDidMount() {
    const { api } = this.context;
    api.get<DetailsModel>('details')
      .then(newDetails => this.setState({ ...this.state, details: newDetails }));
  }

  render() {
    return (
      <>
        <TitleBar title="Device details" previousView={ViewNames.OVERVIEW} changeView={this.props.changeView} />
        <Grid container justifyContent="center" spacing={2}>
          <Grid item xs={12}>
            <Card variant="outlined">
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  General
                </Typography>
                <Grid container spacing={1}>
                  <Grid item xs={3}>
                    Assigned name
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.device.assignedName ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    Type number
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.device.typeNumber ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    Serial number
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.device.serial ?? '-'}
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card variant="outlined">
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  Wifi
                </Typography>
                <Grid container spacing={1}>
                  <Grid item xs={3}>
                    SSID
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.wifi.ssid ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    Protection
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.wifi.protection ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    IP address
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.wifi.ipAddress ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    MAC address
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.wifi.macAddress ?? '-'}
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card variant="outlined">
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  Firmware
                </Typography>
                <Grid container spacing={1}>
                  <Grid item xs={3}>
                    Name
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.firmware.name ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    Version
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.firmware.version ?? '-'}
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card variant="outlined">
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  Locale
                </Typography>
                <Grid container spacing={1}>
                  <Grid item xs={3}>
                    Country
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.locale.country ?? '-'}
                  </Grid>
                  <Grid item xs={3}>
                    Timezone
                  </Grid>
                  <Grid item xs={9}>
                    : {this.state.details?.locale.timezone ?? '-'}
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      </>
    );
  }
}