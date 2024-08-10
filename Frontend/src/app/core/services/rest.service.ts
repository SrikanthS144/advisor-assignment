import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RestService {
  baseUrl = ""; // Base URL for API

  constructor(
    private readonly http: HttpClient,
  ) {
    this.baseUrl = `https://localhost:44353`;
  }

  public patch<T>(relativeUrl: string, data: any = null): Observable<T> {
    return this.http
      .patch<T>(`${this.baseUrl}${relativeUrl}`, data)
      .pipe(catchError(this.handleError<T>('error')));
  }

  public get<T>(
    relativeUrl: string,
    params: { [param: string]: string | string[] } = {}
  ): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}${relativeUrl}`, { params });
  }

  public post<T>(relativeUrl: string, data: any = null): Observable<T> {
    return this.http
      .post<T>(`${this.baseUrl}${relativeUrl}`, data)
      .pipe(catchError(this.handleError<T>('error')));
  }

  public put<T>(relativeUrl: string, data: any = null): Observable<T> {
    return this.http
      .put<T>(`${this.baseUrl}${relativeUrl}`, data)
      .pipe(catchError(this.handleError<T>('error')));
  }

  public delete<T>(relativeUrl: string): Observable<T> {
    return this.http
      .delete<T>(`${this.baseUrl}${relativeUrl}`)
      .pipe(catchError(this.handleError<T>('error')));
  }

  public uploadFile<T>(relativeUrl: string, data: any = null) {
    const headers = new HttpHeaders().append(
      'Content-Disposition',
      'multipart/form-data'
    );

    return this.http
      .post<T>(`${this.baseUrl}${relativeUrl}`, data, { headers })
      .pipe(catchError(this.handleError<T>('error')));
  }

  public downloadFile<T>(relativeUrl: string, data: any = null): Observable<T> {
    const headers = new HttpHeaders().append('Accept', 'application/pdf');

    return this.http
      .post<T>(`${this.baseUrl}${relativeUrl}`, data, {
        headers: headers,
        responseType: 'blob' as 'json',
      })
      .pipe(catchError(this.handleError<T>('error')));
  }

  public postWithErrors<T>(
    relativeUrl: string,
    data: any = null
  ): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}${relativeUrl}`, data);
  }


  protected handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      //Display validation errors on screen
      if (error.status == 400) {
        for (const errorPropName in error.error) {
          error.error[errorPropName].forEach((element: string) => {
          });
        }
      } else if (error.error && error.error.value && operation == 'error') {
      }
      const resp = result || error.error;
      return throwError(resp as T);
    };
  }
}
