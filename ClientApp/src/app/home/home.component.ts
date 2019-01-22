import { Component, Input} from '@angular/core';
import {SignupService} from '../shared/signup.service';
import {Login} from '../shared/login';
import {Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent  {
  // public message = "eraj";
  // public name =  "";

  model:any = {};
  loginUser:Login;
  
  constructor(private authService: SignupService, private router:Router){
  }


  login(){
    this.loginUser = {
      UsernameOrEmail: this.model.username,
      Password: this.model.password
    };

    console.log(this.loginUser);
    this.authService.login(this.loginUser).subscribe( res => {
      console.log(res);

      localStorage.setItem("user",JSON.stringify(res));
      setTimeout(() => {
        this.router.navigate(['/account']);
        window.location.reload();
      }, 3000);
      
    },error => {
      console.log(error);
    })
  }

 
}
