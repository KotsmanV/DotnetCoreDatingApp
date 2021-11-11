import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-errors',
  templateUrl: './errors.component.html',
  styleUrls: ['./errors.component.css']
})
export class ErrorsComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  get404error(){
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe(response =>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }

  get400error(){
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe(response =>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
  get500error(){
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe(response =>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
  get401error(){
    this.http.get(this.baseUrl + 'buggy/auth').subscribe(response =>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
  get400validationError(){
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe(response =>{
      console.log(response);
    }, error =>{
      console.log(error);
    })
  }
}
