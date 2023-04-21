import BaseView, { BaseViewProps } from './base';
import TitleBar from '../title-bar';
import { ViewNames } from './factory';

interface RelaxBreatheViewState {
  
}

export default class RelaxBreatheView extends BaseView<BaseViewProps, RelaxBreatheViewState> {
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
        <TitleBar title="RelaxBreathe" previousView={ViewNames.OVERVIEW} changeView={this.props.changeView} />
        Work in progress.
      </>
    );
  }
}