import BaseView, { BaseViewProps } from './base';
import TitleBar from '../title-bar';
import { ViewNames } from './factory';

interface FMRadioViewState {
  
}

export default class FMRadioView extends BaseView<BaseViewProps, FMRadioViewState> {
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
        <TitleBar title="FM-radio" previousView={ViewNames.OVERVIEW} changeView={this.props.changeView} />
        Work in progress.
      </>
    );
  }
}