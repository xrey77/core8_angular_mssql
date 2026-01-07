import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {  Observable, catchError, of, throwError } from 'rxjs';

export interface SearchResponse {
  products: any[];
  message: string;
}

@Injectable({
  providedIn: 'root'
})

export class ProductsService {
  private charturl = "https://localhost:7280/chart";
  constructor(
    private httpclient: HttpClient
  ) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
  
      return of(result as T);
    };
  }
  log(arg0: string) {
    throw new Error('Method not implemented.');
  }

  public sendSearchRequest(search: any): Observable<any>
  {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
        // 'Authorization': 'jwt-token'
      })
    };
    return this.httpclient.post<SearchResponse>("https://localhost:7280/api/searchproducts", search, options)
    .pipe(
      catchError(this.handleError())
    );        

  }

  public sendProductRequest(page: any): Observable<any>
  {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
        // 'Authorization': 'jwt-token'
      })
    };
    return this.httpclient.get("https://localhost:7280/api/listproducts/" + page, options)
    .pipe(
      catchError(this.handleError())
    );        
  }

  public showPdfReport(): Observable<Blob> {
    return this.httpclient.get('https://localhost:7280/products/report', { 
      responseType: 'blob' 
    });
  }

  public showSalesGraph(): Observable<Blob> {
    return this.httpclient.get(this.charturl, { responseType: 'blob' });
  }
}
