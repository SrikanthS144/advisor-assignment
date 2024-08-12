import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { RestService } from './rest.service';
import { State } from '../model/advise.model';

@Injectable({
  providedIn: 'root',
})
export abstract class GridService {
  public state: State = {};
  public data$: BehaviorSubject<any> = new BehaviorSubject([]);
  protected baseUrl: string = '';
  protected orderBy: string = '';
  public currentFilter: any = '';
  public gridState: any = {};
  public defaultFilter!: any;
  public defaultExpand!: any;
  private apiUrl: string = '';
  private gridCurrentState: any;

  constructor(private restService: RestService, apiUrl: string) {
    this.apiUrl = apiUrl;
  }

  public setOrderBy(orderBy: string): void {
    this.orderBy = orderBy;
  }

  public getData(queryParams: any = {}): Observable<any> {
    const params = { ...queryParams, $orderby: this.orderBy };
    return this.restService.get(this.apiUrl, { params });
  }

  public read() {
    const query = this.generateODataQuery(this.gridState);

    this.restService
      .get(this.apiUrl + (query ? '?' + query : ''), {})
      .pipe(
        map((x: any) => {
          this.data$.next(x.value ?? x);
          this.state.totalCount = x['@odata.count'] ?? x.length;
          return x;
        })
      )
      .subscribe();
  }

  public onFilterChange(event: any) {
    const eventString = JSON.stringify(event);
    const gridStateString = JSON.stringify(this.gridState);
    const defaultFilterString = JSON.stringify(this.defaultFilter);
    const updatedGridState = JSON.parse(JSON.stringify(event));
    if (eventString === gridStateString) {
      return;
    }
    this.gridState = updatedGridState;
    this.read();
  }

  public stateInitialization(state?: State) {
    this.state = state ?? {
      skip: 0,
      top: 10,
      filter: [],
      defaultGridFilter: [],
      gridFilter: [],
    };
  }

  private generateODataQuery(gridState?: any): string {
    this.gridCurrentState = gridState;
    gridState = gridState ?? this.gridCurrentState;
    const filterQueries: string[] = [];
    const queryParts: string[] = [];
    let filterData: any = {};
    if (Object.entries(gridState)?.length) {
      const entries = Object.entries(gridState?.filters);
      const filtered = entries?.filter((x: any) =>
        x[1]?.find((y: any) => y?.value !== null)
      );
      gridState.filters = {};
      if (filtered.length) {
        filtered.forEach((x: any) => {
          gridState.filters[x?.[0]] = x?.[1];
        });
      }
    }
    filterData = { ...this.defaultFilter, ...gridState.filters };
    const sortQueries: string[] = [];
    gridState.multiSortMeta?.forEach((sortMeta: any) => {
      const { field, order } = sortMeta;
      sortQueries.push(`${field} ${order === 1 ? 'asc' : 'desc'}`);
    });

    if (sortQueries.length > 0) {
      queryParts.push(`$orderby=${sortQueries.join(',')}`);
    }

    for (const field in filterData) {
      filterData[field].forEach((filterItem: any) => {
        const { value, matchMode } = filterItem;
        if (value !== null) {
          const filterValue = typeof value === 'string' ? `'${value}'` : value;
          switch (matchMode) {
            case 'startsWith':
              filterQueries.push(`startswith(tolower(${field}), ${filterValue.toLowerCase()})`);
              break;
            case 'endsWith':
              filterQueries.push(`endswith(tolower(${field}), ${filterValue.toLowerCase()})`);
              break;
            case 'contains':
              filterQueries.push(`contains(tolower(${field}), ${filterValue.toLowerCase()})`);
              break;
            case 'notContains':
              filterQueries.push(`not(contains(tolower(${field}), ${filterValue.toLowerCase()}))`);
              break;
            case 'equals':
              filterQueries.push(`tolower(${field}) eq ${filterValue.toLowerCase()}`);
              break;
            case 'notEquals':
              filterQueries.push(`tolower(${field})   ne ${filterValue.toLowerCase()}`);
              break;
            // Add other cases as needed
          }
        }
      });
    }

    if (filterQueries.length > 0) {
      queryParts.push(
        `$filter=${filterQueries.join(
          ' ' + (gridState.operators ?? 'or') + ' '
        )}`
      );
    }
    if (this.orderBy) {
      queryParts.push(`$orderby=${this.orderBy}`);
    }
    queryParts.push(`$skip=${gridState.first ?? 0}`);
    queryParts.push(`$top=${gridState.rows ?? 10}`);
    queryParts.push('$count=true');
    if (this.defaultExpand) {
      queryParts.push(`$expand=${this.defaultExpand}`);
    }

    return queryParts.join('&');
  }
}
