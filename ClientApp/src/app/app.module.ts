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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AccountComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'signup', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'account', component: AccountComponent, canActivate:[AuthGuard] },
    ])
  ],
  providers: [SignupService,AuthGuard,UserdetailsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
