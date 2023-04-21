import { BaseViewProps } from './base';
import AlarmsView from './alarms';
import DetailsView from './details';
import FMRadioView from './fm-radio';
import OverviewView from './overview';
import RelaxBreatheView from './relax-breathe';
import SunsetView from './sunset';

export enum ViewNames {
  OVERVIEW = "overview",
  DETAILS = "details",
  ALARMS = "alarms",
  RELAX_BREATHE = "relax-breathe",
  SUNSET = "sunset",
  FM_RADIO = "fm-radio"
};

export function createView(name: string, props: BaseViewProps) {
  switch (name) {
    case ViewNames.OVERVIEW:
      return <OverviewView {...props} />;
    case ViewNames.DETAILS:
      return <DetailsView {...props} />;
    case ViewNames.ALARMS:
      return <AlarmsView {...props} />;
    case ViewNames.RELAX_BREATHE:
      return <RelaxBreatheView {...props} />;
    case ViewNames.SUNSET:
      return <SunsetView {...props} />;
    case ViewNames.FM_RADIO:
      return <FMRadioView {...props} />;
  }
}