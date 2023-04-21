import { Card, CardContent, Grid, Stack, Switch, Typography } from '@mui/material';
import { useContext, useEffect, useState } from 'react';
import DemoContext from '../contexts/demo';
import { Bedtime } from '@mui/icons-material';
import BedtimeSessionModel from '../models/bedtime-session';
import moment from 'moment';

export default function BedtimeSession() {
  const { api } = useContext(DemoContext);
  const [time, setTime] = useState(new Date());
  const [enabled, setEnabled] = useState<boolean | null>(null);
  const [started, setStarted] = useState<moment.Moment | null>(null);
  const [ended, setEnded] = useState<moment.Moment | null>(null);

  const refreshBedtimeSession = () => {
    api.get<BedtimeSessionModel>('bedtime').then(newBedtimeSession => {
      if (newBedtimeSession) {
        setEnabled(newBedtimeSession.enabled);
        setStarted(moment(newBedtimeSession.started).local());
        setEnded(moment(newBedtimeSession.ended).local());
      }
    })
  };

  const onEnabledChanged = (newEnabled: boolean) => {
    if (newEnabled !== enabled) {
      api.put<any, BedtimeSessionModel>('bedtime/enabled', { enabled: newEnabled });
      setEnabled(newEnabled);
      if (newEnabled) {
        setStarted(moment().local());
      } else {
        setEnded(moment().local());
      }
    }
  };

  // Run a timer for every 3 seconds
  useEffect(() => {
    const interval = setInterval(() => {
      setTime(new Date());
    }, 3000);

    return () => clearInterval(interval);
  }, []);

  // Update the bed time session whenever the timer updates
  useEffect(() => {
    refreshBedtimeSession();
  }, [time]);

  return (
    <Card variant="outlined">
      <CardContent sx={{ position: "relative" }}>
        <Stack direction="row" alignItems="center" gap={1} mb={2}>
          <Bedtime />
          <Typography variant="h5" component="div">Bedtime</Typography>
        </Stack>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={3}>Enabled</Grid>
          <Grid item xs={9}>
            {
              (enabled !== null && (
                <Switch
                  checked={enabled}
                  onChange={(_, checked) => onEnabledChanged(checked)}
                  sx={{ ml: "-12px" }}
                />
              )) || '-'
            }
          </Grid>
          {
            enabled === true && (
              <>
                <Grid item xs={3}>Started</Grid>
                <Grid item xs={9}>{started?.format('LLLL') || '-'}</Grid>
              </>
            )
          }
          {
            enabled === false && (
              <Grid item xs={12}>
                <Grid container alignItems="start">
                  <Grid item xs={3}>Last session</Grid>
                  <Grid item xs={9}>
                    From {started?.format('LLLL') || '-'}
                    <br />
                    to {ended?.format('LLLL') || '-'}
                  </Grid>
                </Grid>
              </Grid>
            )
          }
        </Grid>
      </CardContent>
    </Card>
  );
}