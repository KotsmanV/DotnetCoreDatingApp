import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dotnet/Angular Dating App';
  users: any;

  //The constructor is injected with the Account Service
  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.setCurrentUser();
  }

  //Gets the current user from the local storage and uses the account service to make the login persistent
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }




}
