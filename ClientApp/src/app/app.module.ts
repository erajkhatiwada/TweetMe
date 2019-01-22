import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AccountComponent } from './account/account.component';

import {SignupService} from './shared/signup.service';
import {UserdetailsService} from './shared/user/userdetails.service';
import {AuthGuard} from './shared/auth/auth.guard';
import {AuthLoginSignupGuard} from './shared/auth/auth-login-signup.guard';
import { FollowPageComponent } from './follow-page/follow-page.component';
import { Page1Component } from './page1/page1.component';
import { Page2Component } from './page2/page2.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AccountComponent,
    FollowPageComponent,
    Page1Component,
    Page2Component
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate:[AuthLoginSignupGuard] },
      { path: 'signup', component: CounterComponent, canActivate:[AuthLoginSignupGuard] },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'account', component: AccountComponent, canActivate:[AuthGuard] },
      { path: 'followPage', component: FollowPageComponent },
    ])
  ],
  providers: [SignupService,AuthGuard,UserdetailsService,AuthLoginSignupGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
