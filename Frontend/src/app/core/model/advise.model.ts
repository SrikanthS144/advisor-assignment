export interface Advise {
  AdvisorId?: number;
  Name?: string;
  SIN?: Number;
  Address?: string;
  Phone?: number;
}
export interface State {
  skip?: number;
  take?: number;
  sort?: string | Array<string>;
  filter?: Array<string> | any;
  gridFilter?: Array<string>;
  defaultGridFilter?:Array<string>;
  group?: string | Array<string>;
  orderby?: Array<any>;
  expand?: string | Array<string>;
  top?: number;
  count?: boolean;
  totalCount?: number;
}
