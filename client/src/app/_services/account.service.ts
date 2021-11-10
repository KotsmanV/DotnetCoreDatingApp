import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { ReplaySubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  //Url for the API call
  baseUrl = 'https://localhost:5001/api/';
  //Buffer object which can hold multiple values that it observes. Here we need to add only one.
  private currentUserSource = new ReplaySubject<User>(1);
  //Observable which will hold the current user.
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    //API is called and use map from rxjs to apply the following function to the response
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        //the user that signs in is added to the local storage as JSON and to the ReplaySubject to be observed
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model:any){
    return this.http.post(this.baseUrl + "account/register", model).pipe(
      map((user:User) =>{
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
