import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
//Toastr is a notification service. We should also need add the css style in the angular.json.
//Whenever we need to use the Toastr, it should be injected in the element/module constructor
//and then used to display any notification we need
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    })
  ],
  exports:[
    BsDropdownModule,
    ToastrModule
  ]
  
})
export class SharedModule { }
