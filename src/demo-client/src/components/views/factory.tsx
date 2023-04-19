import { BaseViewProps } from './base';
import OverviewView from './overview';

export enum ViewNames {
  OVERVIEW = "overview"
};

export function createView(name: string, props: BaseViewProps) {
  switch (name) {
    case ViewNames.OVERVIEW:
      return <OverviewView {...props} />;
  }
}