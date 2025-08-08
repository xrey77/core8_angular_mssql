import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {  Observable, of, throwError } from 'rxjs';
import { retry, catchError} from 'rxjs/operators';
import { Mfa } from './interface/mfa';

@Injectable({
  providedIn: 'root'
})
export class MfaService {

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

  public sendMfaValidationRequest(mfadata: Mfa): Observable<any>
  {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
        // 'Authorization': 'jwt-token'
      })
    };
    return this.httpclient.post<Mfa>("https://localhost:7280/validateotp", mfadata, options)
    .pipe(
      catchError(this.handleError('mfadata', mfadata))
    );        
  }    
}
