import { ArrowBackIos } from '@mui/icons-material';
import { Box, Button, Divider, Grid, Link, Typography } from '@mui/material';

export default function TitleBar(props: { title: string, previousView?: string, changeView?: (view: string) => void }) {
  const { title, previousView, changeView } = props;
  return (
    <Grid container sx={{ mb: 3 }}>
      {
        previousView && (
          <Grid item xs={2}>
            <Button variant="text" sx={{ height: "100%" }} onClick={() => changeView && changeView(previousView)}>
              <ArrowBackIos />
            </Button>
          </Grid>
        )
      }
      <Grid item xs="auto">
        <Typography variant="h4" component="div">
          {title}
        </Typography>
      </Grid>
    </Grid>
  );
}