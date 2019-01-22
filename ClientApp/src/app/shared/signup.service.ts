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

  postTweet(tweet:any){
    return this.http.post(this.url+'api/comment/',tweet);
  }

  receiveTweet(userId:any){
    return this.http.get(this.url+'api/comment/byUser='+userId);
  }

  checkIfFollowed(follow:any){
    return this.http.post(this.url+'api/followuser/check',follow);
  }

  follow(followUser:any){
    return this.http.post(this.url+'api/followuser/post',followUser);
  }
  
}
