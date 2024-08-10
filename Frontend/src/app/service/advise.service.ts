import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, MonoTypeOperatorFunction, Observable } from 'rxjs';
import { Advise } from '../core/model/advise.model';
import { environment } from '../environment/environment';

@Injectable()
export class AdviseService {
  private readonly url = environment.apiUrl + '/odata/Advisor';

  constructor(private http: HttpClient) {}

  //---------Get the data------//
  public getAdvisor(
    page: number,
    size: number,
    search: string = '',
    sortField: string = '',
    sortOrder: number = 1
  ): Observable<{ advisor: Advise[]; totalRecords: number }> {
    let params = new HttpParams()
      .set('$count', 'true')
      .set(
        '$orderby',
        sortField
          ? `${sortField} ${sortOrder === 1 ? 'asc' : 'desc'}`
          : 'AdvisorId desc'
      )
      .set('$top', size.toString())
      .set('$skip', (page * size).toString());

    if (search) {
      params = params.set('$filter', `contains(Name,'${search}')`);
    }

    return this.http.get<any>(this.url, { params }).pipe(
      map((response: any) => ({
        advisor: response.value,
        totalRecords: response['@odata.count'] || 0,
      }))
    );
  }

  public update(id: Number, data: any) {
    return this.http.put(`${this.url}/${id}`, data);
  }

  //----------Create Api-------//
  public create(data: any) {
    return this.http.post(this.url, data);
  }

  //---------Delete Api-------------//
  public delete(id: Number) {
    return this.http.delete(this.url + '/' + id);
  }

  public getAdviceById(AdvisorId: number): Observable<Advise> {
    const filter = `AdvisorId eq ${AdvisorId}`;
    const encodedFilter = encodeURIComponent(filter);
    return this.http.get(`${this.url}?$filter=${encodedFilter}`);
  }
}
