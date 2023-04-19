import { BaseViewProps } from './base';
import DetailsView from './details';
import OverviewView from './overview';

export enum ViewNames {
  OVERVIEW = "overview",
  DETAILS = "details"
};

export function createView(name: string, props: BaseViewProps) {
  switch (name) {
    case ViewNames.OVERVIEW:
      return <OverviewView {...props} />;
      case ViewNames.DETAILS:
        return <DetailsView {...props} />;
  }
}