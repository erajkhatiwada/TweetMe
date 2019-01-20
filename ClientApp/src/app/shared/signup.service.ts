import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

@Injectable()
export class SignupService {
  private url = 'https://localhost:44395/';
  constructor(private http: HttpClient) {
   
  }

  signUp(body:any){
    return this.http.post(this.url+'api/auth/register',body);
  }

  login(body:any){
    return this.http.post(this.url+'api/auth/login',body);
  }

  searchUser(query:any){
    return this.http.get(this.url+'api/usersearch/query='+query);
  }
  
}
