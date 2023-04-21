import BaseView, { BaseViewProps } from './base';
import TitleBar from '../title-bar';
import { ViewNames } from './factory';

interface AlarmsViewState {
  
}

export default class AlarmsView extends BaseView<BaseViewProps, AlarmsViewState> {
  constructor(props: BaseViewProps) {
    super(props);
    this.state = { details: null };
  }

  componentDidMount() {
    const { api } = this.context;
    
  }

  render() {
    return (
      <>
        <TitleBar title="Alarms" previousView={ViewNames.OVERVIEW} changeView={this.props.changeView} />
        Work in progress.
      </>
    );
  }
}