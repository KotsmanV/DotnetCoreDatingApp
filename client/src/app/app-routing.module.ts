import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './components/homepage/homepage.component';
import { ListsComponent } from './components/lists/lists.component';
import { MemberDetailsComponent } from './components/members/member-details/member-details.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MessagesComponent } from './components/messages/messages.component';
import { ErrorsComponent } from './errors/errors/errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component:HomepageComponent},
  {
    path:'',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children:[
      { path: 'members', component:MemberListComponent},
      { path: 'members/:id', component:MemberDetailsComponent},
      { path: 'lists', component:ListsComponent},
      { path: 'messages', component:MessagesComponent},
    ]
  },
  { path: 'errors', component:ErrorsComponent},
  { path: 'not-found', component:NotFoundComponent},
  { path: 'server-error', component:ServerErrorComponent},
  { path: '**', component:NotFoundComponent, pathMatch:'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
