import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShortUrlPageComponent } from './components/short-url/components/short-url-page/short-url-page.component';
import { ShortUrlInfoComponent } from './components/short-url/components/short-url-info/short-url-info.component';
import { AnonymousGuard } from './core/guards/anonymous.guard';
import { AboutComponent } from './components/about/components/about.component';
import { RegistrationComponent } from './components/Auth/components/registration/registration.component';
import { LoginComponent } from './components/Auth/components/login/login.component';

const routes: Routes = [
  {path:'', redirectTo: 'short-url', pathMatch:'full'},
  {path:'about', component: AboutComponent},
  {path:'registration', component: RegistrationComponent},
  {path:'short-url', component: ShortUrlPageComponent},
  {path:'short-url/url/:id', component: ShortUrlInfoComponent, canActivate: [AnonymousGuard],
    data:{
      roles: ["Admin", "User"] 
    }},
  {path:'login', component: LoginComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
