import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {  Observable, throwError } from 'rxjs';
import { retry, catchError} from 'rxjs/operators';
import { Profile } from './interface/profile';


interface ActivateMfa {
  twofactorenabled: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private httpclient: HttpClient
  ) { }

  handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      return throwError(() => new Error(error.error));
    } else {
      return throwError(() => new Error(error.error));
    }
  }  

  public getUserbyId(id: string, token: any): Observable<any> {

    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': `Bearer ${token}`
      })
    };    

    //let params1 = this.httpParams.set("userId",id);
    return this.httpclient.get("https://localhost:7280/api/getbyid/" + id, options)
    .pipe(catchError(this.handleError));      
  }

  public ActivateMFA(id: string, enabled: ActivateMfa, token: any) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': `Bearer ${token}`
      })
    };    
    return this.httpclient.put("https://localhost:7280/api/enablemfa/" + id, enabled, options)
    .pipe(catchError(this.handleError));
  }

  public UploadPicture(profilepic: any, token: any): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${token}`
      })
    };    
    return this.httpclient.post<any>("https://localhost:7280/api/uploadpicture", profilepic, options)
    .pipe(catchError(this.handleError));
  }

  public sendProfileRequest(id: string, userdtls: Profile, token: any): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': `Bearer ${token}`
      })
    };    
    return this.httpclient.patch<Profile>("https://localhost:7280/api/updateprofile/" + id, userdtls, options)
    .pipe(catchError(this.handleError));
  }  

  public sendNewpasswordRequest(id: string, userdtls: Profile, token: any): Observable<any> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': `Bearer ${token}`
      })
    };    
    return this.httpclient.patch<Profile>("https://localhost:7280/api/updatepassword/" + id, userdtls, options)
    .pipe(catchError(this.handleError));
  }  

}
