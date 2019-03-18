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
  errorMessage:any;
  logginInMessage:any;
  constructor(private authService: SignupService, private router:Router){
  }


  login(){
    this.logginInMessage = "Authorizing... Please Wait!"
    this.loginUser = {
      UsernameOrEmail: this.model.username,
      Password: this.model.password
    };

    console.log(this.loginUser);
    this.authService.login(this.loginUser).subscribe( res => {
      console.log(res);

      /* JWT TEST */
      var x = res;
      let jwt = JSON.parse(JSON.stringify(x)).token;

      let jwtData = jwt.split('.')[1];
      let decodedJwtJsonData = window.atob(jwtData);
      let decodedJwtData = JSON.parse(decodedJwtJsonData);

      let isAdmin = decodedJwtData.nameid;

      //console.log('jwtData: ' + jwtData)
      //console.log('decodedJwtJsonData: ' + decodedJwtJsonData)
      //console.log('decodedJwtData: ' + decodedJwtData)
      //console.log('Is admin: ' + isAdmin)
      /* JWT END */

      this.logginInMessage = "Logged In";
      localStorage.setItem("user",JSON.stringify(res));
      setTimeout(() => {
        this.router.navigate(['/account']);
        window.location.reload();
      }, 500);
      
    },error => {
      console.log(error);
      this.logginInMessage = "";
      this.errorMessage = "Invalid username or password";

      setTimeout(() => {
        this.errorMessage = "";
      }, 3000);
    })
  }

 
}
