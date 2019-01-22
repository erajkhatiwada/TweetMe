import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import {Router} from '@angular/router';
@Injectable()
export class AuthLoginSignupGuard implements CanActivate {
  constructor(private router : Router){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      if (localStorage.getItem('user') == null)
      return true;
      this.router.navigate(['/account']);
      return false;
  }
}
