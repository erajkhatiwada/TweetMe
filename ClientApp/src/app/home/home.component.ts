import { Component } from '@angular/core';
import {SignupService} from '../shared/signup.service';
import {Login} from '../shared/login';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  model:any = {};
  loginUser:Login;

  constructor(private authService: SignupService){}

  login(){
    this.loginUser = {
      UsernameOrEmail: this.model.username,
      Password: this.model.password
    };

    console.log(this.loginUser);
    this.authService.login(this.loginUser).subscribe( res => {
      console.log(res);
      
    },error => {
      console.log(error);
    })
  }
}
