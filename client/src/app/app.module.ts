import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './components/nav/nav.component';
import { HomepageComponent } from './components/homepage/homepage.component';
import { RegisterComponent } from './components/register/register.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MemberDetailsComponent } from './components/members/member-details/member-details.component';
import { ListsComponent } from './components/lists/lists.component';
import { MessagesComponent } from './components/messages/messages.component';
import { SharedModule } from './modules/shared.module';
import { ErrorsComponent } from './errors/errors/errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomepageComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailsComponent,
    ListsComponent,
    MessagesComponent,
    ErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule
  ],
  providers: [
    //Any interceptor can be added and, by using multi: true,
    //we specify that the program can use multiple interceptors
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
