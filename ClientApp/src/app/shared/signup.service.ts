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

  receiveTweetFromUserSearch(userId: any) {
    return this.http.get(this.url + 'api/comment/byUserSearch=' + userId);
  }

  receiveTweetFromUserSearchFollowed(userId: any) {
    return this.http.get(this.url + 'api/comment/byUserSearchFollowed=' + userId);
  }

  checkIfFollowed(follow:any){
    return this.http.post(this.url+'api/followuser/check',follow);
  }

  follow(followUser:any){
    return this.http.post(this.url+'api/followuser/post',followUser);
  }

  tweetsFromAllFollowers(userId:any){
    return this.http.get(this.url+'api/followuser/allTweets='+userId);
  }

  unfollow(userId:number, unfollowUserId:number){
      return this.http.post(this.url+'api/FollowUser/unfollow/currentUser='+userId+'/unfollowUser='+unfollowUserId,null);
  }
  
}
