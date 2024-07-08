import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './MyComponent/user/user.component';
import { AboutComponent } from './MyComponent/about/about.component';
import {ListUserComponent} from './list-user/list-user.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { ContactComponent } from './contact/contact.component';
// import { ContactComponent } from './contact/contact.component';
// contac
ContactComponent
const routes: Routes = [
  { path: 'home', component: UserComponent ,canActivate: [AuthGuard] },
  { path: 'about', component: AboutComponent ,canActivate: [AuthGuard]},
  { path: 'list-user', component: ListUserComponent ,canActivate: [AuthGuard]},
  { path: 'contact', component: ContactComponent ,canActivate: [AuthGuard]},
  { path: '', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
