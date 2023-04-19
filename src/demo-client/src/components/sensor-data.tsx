import { Card, LinearProgress, List, ListItem, ListItemIcon, ListItemText, Tooltip } from '@mui/material';
import { useContext, useEffect, useState } from 'react';
import DemoContext from '../contexts/demo';
import SensorDataModel from '../models/sensor-data';
import { Thermostat, VolumeMute, WaterDrop, WbSunny } from '@mui/icons-material';

export default function SensorData() {
  const { api } = useContext(DemoContext);
  const [time, setTime] = useState(new Date());
  const [refreshProgress, setRefreshProgress] = useState(0);
  const [sensorData, setSensorData] = useState<SensorDataModel | null>(null);

  const refreshSensorData = () => api.get<SensorDataModel>("sensors").then(setSensorData);

  // Run a timer for every second
  useEffect(() => {
    const interval = setInterval(() => {
      setTime(new Date());
    }, 100);

    return () => clearInterval(interval);
  }, []);

  // Update refresh progress whenever the timer updates
  useEffect(() => {
    setRefreshProgress(refreshProgress === 100 ? -11 : refreshProgress + 1);
  }, [time]);

  // Refresh sensor data when progress resets
  useEffect(() => {
    if (refreshProgress === 0) {
      refreshSensorData();
    }
  }, [refreshProgress]);

  return (
    <Card variant="outlined">
      <List dense={true}>
        <ListItem>
          <ListItemIcon>
            <Tooltip title="Temperature">
              <Thermostat />
            </Tooltip>
          </ListItemIcon>
          <ListItemText
            primary={`${sensorData?.currentTemperature ?? '-'} °C`}
            secondary={`Average: ${sensorData?.averageTemperature ?? '-'} °C`} />
        </ListItem>
        <ListItem>
          <ListItemIcon>
            <Tooltip title="Light">
              <WbSunny />
            </Tooltip>
          </ListItemIcon>
          <ListItemText
            primary={`${sensorData?.currentLight ?? '-'} lux`}
            secondary={`Average: ${sensorData?.averageLight ?? '-'} lux`} />
        </ListItem>
        <ListItem>
          <ListItemIcon>
            <Tooltip title="Sound">
              <VolumeMute />
            </Tooltip>
          </ListItemIcon>
          <ListItemText
            primary={`${sensorData?.currentSound ?? '-'} dB`}
            secondary={`Average: ${sensorData?.averageSound ?? '-'} dB`} />
        </ListItem>
        <ListItem>
          <ListItemIcon>
            <Tooltip title="Humidity">
              <WaterDrop />
            </Tooltip>
          </ListItemIcon>
          <ListItemText
            primary={`${sensorData?.currentHumidity ?? '-'} %`}
            secondary={`Average: ${sensorData?.averageHumidity ?? '-'} %`} />
        </ListItem>
      </List>
      <LinearProgress variant="determinate" value={refreshProgress} />
    </Card>
  );
}