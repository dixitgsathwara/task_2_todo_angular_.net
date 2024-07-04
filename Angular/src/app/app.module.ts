import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';  // Import HttpClientModule
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule } from '@angular/forms';
import { UserComponent } from './MyComponent/user/user.component';
import { AddUserComponent } from './MyComponent/add-user/add-user.component';
import { AboutComponent } from './MyComponent/about/about.component';
import { UserListComponent } from './MyComponent/user-list/user-list.component';
import { ListUserComponent } from './list-user/list-user.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    UserComponent,
    AddUserComponent,
    AboutComponent,
    UserListComponent,
    ListUserComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
