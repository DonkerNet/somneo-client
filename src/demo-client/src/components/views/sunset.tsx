import BaseView, { BaseViewProps } from './base';
import TitleBar from '../title-bar';
import { ViewNames } from './factory';

interface SunsetViewState {
  
}

export default class SunsetView extends BaseView<BaseViewProps, SunsetViewState> {
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
        <TitleBar title="Sunset" previousView={ViewNames.OVERVIEW} changeView={this.props.changeView} />
        Work in progress.
      </>
    );
  }
}