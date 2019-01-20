import { Injectable } from '@angular/core';

@Injectable()
export class UserdetailsService {
  user:any;
  constructor() {
    this.getStorageData();
   }

  getStorageData(): void{
    var x = localStorage.getItem("user");

    this.user = JSON.parse(x);
  }

  returnUserId(){
    return this.user.id;
  }

  returnUsername(){
    return this.user.username;
  }
}
