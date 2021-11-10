import { ThrowStmt } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Output exports the variable to the parent component. This is usually done by emitting events.
  //EventEmitter should come from '@angular/core'
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe(response =>{
      console.log(response);
      this.cancel();
    }, error => {
      console.log(error)
    })
  }

  cancel() {
    //this value is emitted
    this.cancelRegister.emit(false);
  }
}
