import { Box, Card, CardContent, Grid, LinearProgress, Slider, Stack, Switch, Typography } from '@mui/material';
import { useContext, useEffect, useState } from 'react';
import DemoContext from '../contexts/demo';
import { Brightness5 } from '@mui/icons-material';
import LightStateModel from 'src/models/light-state';

export default function LightControls() {
  const { api } = useContext(DemoContext);
  const [time, setTime] = useState(new Date());
  const [processingChanges, setProcessingChanges] = useState<boolean>(false);
  const [enabled, setEnabled] = useState<boolean | null>(null);
  const [intensity, setIntensity] = useState<number | null>(null);

  const refreshLightState = () => {
    if (!processingChanges) {
      api.get<LightStateModel>("light").then(newLightState => {
        if (!processingChanges && newLightState) {
          setEnabled(newLightState.lightEnabled);
          setIntensity(newLightState.lightLevel);
        }
      })
    }
  };

  const onEnabledChanged = (newEnabled: boolean) => {
    if (newEnabled !== enabled) {
      setProcessingChanges(true);

      api.put('light/enabled', { enabled: newEnabled })
        .then(() => {
          setEnabled(newEnabled);
          setTimeout(() => setProcessingChanges(false), 5000);
        });
    }
  };

  const onIntensityChanged = (newIntensity: number) => {
    if (newIntensity !== intensity) {
      api.put('light/intensity', { intensity: newIntensity });
      setIntensity(newIntensity);
    }
  };

  // Run a timer for every 3 seconds
  useEffect(() => {
    const interval = setInterval(() => {
      setTime(new Date());
    }, 3000);

    return () => clearInterval(interval);
  }, []);

  // Update the light state whenever the timer updates
  useEffect(() => {
    refreshLightState();
  }, [time]);

  return (
    <Card variant="outlined">
      <CardContent sx={{ position: "relative" }}>
        <Stack direction="row" alignItems="center" gap={1} mb={2}>
          <Brightness5 />
          <Typography variant="h5" component="div">Light</Typography>
        </Stack>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={3}>Enabled</Grid>
          <Grid item xs={9}>
            {
              (enabled !== null && (
                <Switch
                  checked={enabled}
                  onChange={(_, checked) => onEnabledChanged(checked)}
                  disabled={processingChanges}
                  sx={{ ml: "-12px" }}
                />
              )) || '-'
            }
          </Grid>
          {
            enabled === true && (
              <>
                <Grid item xs={3}>Intensity</Grid>
                <Grid item xs={9} pr={2}>
                  {
                    (intensity !== null && (
                      <Slider
                        value={intensity}
                        onChange={(_, value) => onIntensityChanged(value as number)}
                        valueLabelDisplay="auto"
                        step={1}
                        marks
                        min={1}
                        max={25}
                        disabled={processingChanges}
                      />
                    ) || '-')
                  }
                </Grid>
              </>
            )
          }
        </Grid>
      </CardContent>
      {
        processingChanges && <LinearProgress />
      }
    </Card>
  );
}