import { Injectable } from '@angular/core';
import { GridService } from '../core/services/grid.service';
import { RestService } from '../core/services/rest.service';

@Injectable({
  providedIn: 'root',
})
export class ListService extends GridService {
  constructor(restService: RestService) {
    super(restService, '/odata/advisor');

    this.setOrderBy('AdvisorId desc');
  }
}
