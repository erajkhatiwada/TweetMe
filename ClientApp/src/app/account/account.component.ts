import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {SignupService} from '../shared/signup.service';
import {UserdetailsService} from '../shared/user/userdetails.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit  {

  //user variables
  id:any;
  username:any;
  userModel:any;
  showLoading:boolean;

  //tweet variables
  tweetModel:any = {};
  userComment:any;
  systemDateInYearMonthDay:any;
  tweets:any;

  //searchuser variables
  searchResults:any;


  constructor(private router: Router, private user: UserdetailsService, private commonApi: SignupService) {
    this.details();
    this.dateAndTime();
    this.getTweets();
   }

  ngOnInit() {

    Observable.fromEvent(document.getElementById("test"), 'keyup')
    // get value
    //.map((evt: any) => evt.target.value)
    // text length must be > 2 chars
    //.filter(res => res.length > 2)
    // emit after 1s of silence
    .debounceTime(500)        
    // emit only if data changes since the last emit       
    .distinctUntilChanged()
    // subscription
    .subscribe(() => {
      this.showLoading = true;
      if(this.userModel == ""){
        this.showLoading = false;
        this.searchResults = [];
      }else{
        this.commonApi.searchUser(this.userModel).subscribe(async res => {
          this.searchResults = res;
          await this.searchResults;
          this.showLoading = false;
          console.log(res);
        },error => {
          console.log(error);
        });
      }
    });
 
  }

  logout(){
    localStorage.removeItem("user");

    this.router.navigate(['/']); 
  }

  async details(){
    this.id = this.user.returnUserId();
    this.username = this.user.returnUsername();

    await this.id;
    await this.username;
  }

  dateAndTime() {
      let x = new Date();
      var newDay;
      var newMonth;
      var year = x.getFullYear();
      var month = x.getMonth() + 1;
      var day = x.getDate();

      if (day < 10) {
        newDay = "0" + day;
      } else {
        newDay = day;
      }
  
      if (month < 10) {
        newMonth = "0" + month;
      } else {
        newMonth = month;
      }
      var merge = year + "" + newMonth + "" + newDay;
      this.systemDateInYearMonthDay = merge;
  }

  tweet(){
    this.tweetModel = {
      userComment : this.userComment,
      userId: this.id,
      dateCreated: this.systemDateInYearMonthDay
    }
    console.log(this.tweetModel);
    if(this.userComment == undefined || this.userComment == ""){
      window.alert("cannot be empty");
    }else{
      this.commonApi.postTweet(this.tweetModel).subscribe( res => {
        console.log(res,"tweet successfully");
        this.userComment = "";
        this.getTweets();
      }, error => {
        console.log(error);
      });
    }
  }

  getTweets(){
    this.commonApi.receiveTweet(this.id).subscribe( res => {
      console.log(res);
      this.tweets = res;
    });
  }

  goToFollowPage(val:any){
    if(val.userId == this.id){
      this.userModel = "";
      this.searchResults = [];
    }else{
      this.router.navigate(['/followPage'],{queryParams: val, skipLocationChange:false});
    }
    
  }


}
