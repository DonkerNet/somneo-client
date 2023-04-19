import { ArrowBackIos } from '@mui/icons-material';
import { Button, Grid, Typography } from '@mui/material';

export default function TitleBar(props: { title: string, previousView?: string, changeView?: (view: string) => void }) {
  const { title, previousView, changeView } = props;
  return (
    <Grid container sx={{ mb: 3 }} spacing={1}>
      {
        previousView && (
          <Grid item xs="auto">
            <Button variant="text" sx={{ height: "100%", pr: 0 }} onClick={() => changeView && changeView(previousView)}>
              <ArrowBackIos />
            </Button>
          </Grid>
        )
      }
      <Grid item>
        <Typography variant="h4" component="div">
          {title}
        </Typography>
      </Grid>
    </Grid>
  );
}