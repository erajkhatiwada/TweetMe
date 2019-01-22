import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SignupService} from '../shared/signup.service';
import {UserdetailsService} from '../shared/user/userdetails.service';

@Component({
  selector: 'app-follow-page',
  templateUrl: './follow-page.component.html',
  styleUrls: ['./follow-page.component.css']
})
export class FollowPageComponent implements OnInit {

  //loggedIn user deatils

  loggedUserId:any;

  //user to follow
  followUserDetails:any;
  userId:any;
  username:any;
  firstName:any;
  lastName:any;

  //users to follow tweets
  tweets:any;
  messageIfNoTweets:any;

  showHideFollowButton:any;

  relationshipBetweenTwoUsers:any;
  constructor(private activatedRoute: ActivatedRoute, private commonApi: SignupService, private userDetailsService: UserdetailsService) {
    this.catchDetails();
    this.getLoggedInUserData();
  }

  ngOnInit() {
    this.loadTweetsById();
    this.checkIfFollowed();
  }

  catchDetails(){
    this.activatedRoute.queryParams.subscribe(async res => {
      this.followUserDetails = res;
      await this.followUserDetails;
      this.userId = this.followUserDetails.userId;
      this.username = this.followUserDetails.username;
      this.firstName = this.followUserDetails.firstName;
      this.lastName = this.followUserDetails.lastName;

      await this.userId;
      await this.username;
      await this.firstName;
      await this.lastName;

      console.log("first");
    });
  }

  loadTweetsById(){
    this.commonApi.receiveTweet(this.userId).subscribe( res => {
      this.tweets = res;
      if(this.tweets.length == 0){
        this.messageIfNoTweets = "The user have zero tweets!";
      }
    });
  } 

  async getLoggedInUserData(){
    this.loggedUserId = this.userDetailsService.returnUserId();
    await this.loggedUserId;

    console.log(this.loggedUserId, "second");
  }

  checkIfFollowed(){
    this.relationshipBetweenTwoUsers = {
      "userId": this.loggedUserId,
      "followedUserId": this.userId
    };
    console.log(this.relationshipBetweenTwoUsers,"third");
    this.commonApi.checkIfFollowed(this.relationshipBetweenTwoUsers).subscribe(res => {
      let temp = res;
      console.log(temp);
      if(temp == true){
        this.showHideFollowButton = true;
      }else{
        this.showHideFollowButton = false;
      }
    });
  }

  followRequest(){
    this.commonApi.follow(this.relationshipBetweenTwoUsers).subscribe(res => {
      console.log("Followed");
      this.checkIfFollowed();
    });
  }

}
