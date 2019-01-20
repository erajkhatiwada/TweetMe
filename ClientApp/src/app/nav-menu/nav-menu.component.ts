import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  loggedIn:boolean;
  constructor(){
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


}
