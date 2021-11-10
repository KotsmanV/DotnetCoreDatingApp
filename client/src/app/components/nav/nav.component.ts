import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { User } from '../../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any = {};

  //The account service is checked inside the template
  //and checks whether there is a user in local storage or not.
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {}

  //Uses the Account Service to login. The function is called from the form submit.
  login(){
    this.accountService.login(this.model).subscribe(response =>{
      console.log(response);
    }, error => {
      console.log(error)
    })
  }

  logout() {
    this.accountService.logout();
  }
}
