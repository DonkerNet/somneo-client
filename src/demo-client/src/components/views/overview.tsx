import { Button, Grid, Typography } from '@mui/material';
import BaseView, { BaseViewProps } from './base';
import { Air, Notifications, Radio, WbTwilight } from '@mui/icons-material';
import SensorData from '../sensor-data';
import BedtimeSession from '../bedtime-session';
import LightControls from '../light-controls';
import DisplaySettings from '../display-settings';

export default class OverviewView extends BaseView<BaseViewProps, {}> {
  constructor(props: BaseViewProps) {
    super(props);
  }

  render() {
    return (
      <Grid container justifyContent="center" spacing={2}>
        <Grid item xs={5}>
          <Button variant="text" fullWidth sx={{ display: "block", height: "100%" }}>
            <img src="/img/somneo.png" width="100%" />
          </Button>
        </Grid>
        <Grid item xs={7}>
          <SensorData />
        </Grid>
        <Grid item xs={12}>
          <Button variant="contained" fullWidth sx={{ display: "block" }}>
            <Notifications />
            <Typography>Alarms</Typography>
          </Button>
        </Grid>
        <Grid item xs={6}>
          <Button variant="contained" fullWidth sx={{ display: "block" }}>
            <Air />
            <Typography>RelaxBreathe</Typography>
          </Button>
        </Grid>
        <Grid item xs={6}>
          <Button variant="contained" fullWidth sx={{ display: "block" }}>
            <WbTwilight />
            <Typography>Sunset</Typography>
          </Button>
        </Grid>
        <Grid item xs={12}>
          <BedtimeSession />
        </Grid>
        <Grid item xs={12}>
          <LightControls />
        </Grid>
        <Grid item xs={12}>
          <Button variant="contained" fullWidth sx={{ display: "block" }}>
            <Radio />
            <Typography>FM-radio</Typography>
          </Button>
        </Grid>
        <Grid item xs={12}>
          <DisplaySettings />
        </Grid>
      </Grid>
    );
  }
}