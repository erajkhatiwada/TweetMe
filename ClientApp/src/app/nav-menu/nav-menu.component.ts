import { Component} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent  {

  isExpanded = false;

  public loggedIn:boolean;
  constructor(private router: Router){
    this.getUser();
  }

  getUser(){
    if(localStorage.getItem("user") != null){
      this.loggedIn = true;
    }else{
      this.loggedIn = false;
    }
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  
  logout(){

    var x = window.confirm("Are you sure you want to logout");
    if(x){
      localStorage.removeItem("user");
      this.isExpanded = false;
      this.getUser();
      this.router.navigate(['/']); 
    }
    
  }

  alert(){
    window.alert("hey");
  }



}
