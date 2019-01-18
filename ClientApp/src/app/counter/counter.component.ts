import { Component } from '@angular/core';
import {SignUp} from '../shared/signup';
import {SignupService} from '../shared/signup.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  
  model:any = {};
  signUp:SignUp;
  constructor(private SignUpService: SignupService, private router: Router){}

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

}
