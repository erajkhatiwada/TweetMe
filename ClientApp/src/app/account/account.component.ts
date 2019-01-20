import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {SignupService} from '../shared/signup.service';
import {UserdetailsService} from '../shared/user/userdetails.service';
import {Observable} from 'rxjs';
import { delay } from 'rxjs/operator/delay';
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  id:any;
  username:any;
  userModel:any;

  showLoading:boolean;
  constructor(private router: Router, private user: UserdetailsService, private commonApi: SignupService) {
    this.details();
   }

  ngOnInit() {

    Observable.fromEvent(document.getElementById("test"), 'keyup')
    // get value
    //.map((evt: any) => evt.target.value)
    // text length must be > 2 chars
    //.filter(res => res.length > 2)
    // emit after 1s of silence
    .debounceTime(1000)        
    // emit only if data changes since the last emit       
    .distinctUntilChanged()
    // subscription
    .subscribe(() => {
      this.showLoading = true;
      this.commonApi.searchUser(this.userModel).subscribe(res => {
        this.showLoading = false;
        console.log(res);
      },error => {
        console.log(error);
      });
    });
 

  }

  logout(){
    localStorage.removeItem("user");

    this.router.navigate(['/']); 
  }

  details(){
    this.id = this.user.returnUserId();
    this.username = this.user.returnUsername();
  }

}
