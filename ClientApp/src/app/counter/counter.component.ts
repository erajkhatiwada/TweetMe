import { Component, Input, EventEmitter, Output } from '@angular/core';
import {SignUp} from '../shared/signup';
import {SignupService} from '../shared/signup.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css']
})
export class CounterComponent {

  /*@Input() public myData;
  @Output() public childEvent = new EventEmitter(); */

  model:any = {};
  signUp:SignUp;
  constructor(private SignUpService: SignupService, private router: Router){}

  //message = "hello world";
  register(){
    this.signUp = {
      username : this.model.username,
      firstName: this.model.firstname,
      lastName: this.model.lastname,
      email : this.model.email,
      password: this.model.password
    }

    this.SignUpService.signUp(this.signUp).subscribe(() => {
      console.log('Registered');

      setTimeout(() => {
        this.router.navigate(["/"]);
      }, 1000);
    },error => {
      console.log(error.error);
    },()=>{
      console.log("completed");
    });
  }

  // fireEvent(){
  //   this.childEvent.emit('Hey Codeevolution');
  //   console.log('clicked');
  // }

}
