import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/Auth/components/login/login.component';
import { RegistrationComponent } from './components/Auth/components/registration/registration.component';
import { ShortUrlViewComponent } from './components/short-url/components/short-url-view/short-url-view.component';
import { ShortUrlPageComponent } from './components/short-url/components/short-url-page/short-url-page.component';
import { ShortUrlService } from './components/short-url/services/short-url.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NavigationComponent } from './components/navigation/navigation.component';
import { FormsModule } from '@angular/forms';
import { AuthService } from './components/Auth/services/auth.service';
import { JwtInterceptor } from './core/Interceptors/jwt.interceptor';
import { ShortUrlInfoComponent } from './components/short-url/components/short-url-info/short-url-info.component';
import { AnonymousGuard } from './core/guards/anonymous.guard';
import { AboutComponent } from './components/about/components/about.component';
import { DescriptionService } from './components/about/services/description.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AboutComponent,
    RegistrationComponent,
    ShortUrlViewComponent,
    ShortUrlPageComponent,
    NavigationComponent,
    ShortUrlInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule 
  ],
  providers: [ShortUrlService, AuthService, DescriptionService, AnonymousGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
