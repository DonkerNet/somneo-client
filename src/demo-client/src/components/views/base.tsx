import { Component, ContextType } from 'react';
import DemoContext from '../../contexts/demo';

export interface BaseViewProps {
  changeView: (name: string) => void;
}

export default abstract class BaseView<
  TProps extends BaseViewProps,
  TState
> extends Component<TProps, TState> {
  static contextType = DemoContext;
  declare context: ContextType<typeof DemoContext>;

  constructor(props: TProps) {
    super(props);
  }
}