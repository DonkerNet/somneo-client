import { Card, CardContent, Grid, Slider, Stack, Switch, Typography } from '@mui/material';
import { useContext, useEffect, useState } from 'react';
import DemoContext from '../contexts/demo';
import { Brightness6 } from '@mui/icons-material';
import DisplayStateModel from 'src/models/display-state';

export default function DisplaySettings() {
  const { api } = useContext(DemoContext);
  const [time, setTime] = useState(new Date());
  const [permanent, setPermanent] = useState<boolean | null>(null);
  const [brightness, setBrightness] = useState<number | null>(null);

  const refreshDisplayState = () => api.get<DisplayStateModel>("display").then(newDisplayState => {
    if (newDisplayState) {
      setPermanent(newDisplayState.permanent);
      setBrightness(newDisplayState.brightness);
    }
  });

  const onPermanentChanged = (newPermanent: boolean) => {
    if (newPermanent !== permanent) {
      api.put('display/permanent', { permanent: newPermanent });
      setPermanent(newPermanent);
    }
  };

  const onBrightnessChanged = (newBrightness: number) => {
    if (newBrightness !== brightness) {
      api.put('display/brightness', { brightness: newBrightness });
      setBrightness(newBrightness);
    }
  };

  // Run a timer for every 3 seconds
  useEffect(() => {
    const interval = setInterval(() => {
      setTime(new Date());
    }, 3000);

    return () => clearInterval(interval);
  }, []);

  // Update the display state whenever the timer updates
  useEffect(() => {
    refreshDisplayState();
  }, [time]);

  return (
    <Card variant="outlined">
      <CardContent>
        <Stack direction="row" alignItems="center" gap={1} mb={2}>
          <Brightness6 />
          <Typography variant="h5" component="div">Display</Typography>
        </Stack>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={3}>Always on</Grid>
          <Grid item xs={9}>
            {
              (permanent !== null && (
                <Switch
                  checked={permanent}
                  onChange={(_, checked) => onPermanentChanged(checked)}
                />
              )) || '-'
            }
          </Grid>
          <Grid item xs={3}>Brightness</Grid>
          <Grid item xs={9} pr={2}>
            {
              (brightness !== null && (
                <Slider
                  value={brightness}
                  onChange={(_, value) => onBrightnessChanged(value as number)}
                  valueLabelDisplay="auto"
                  step={1}
                  marks
                  min={1}
                  max={6}
                />
              ) || '-')
            }
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
}