import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { LoginModel } from '../../models/login.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private readonly _authService : AuthService, private router: Router){  }

  Name = ""
  Password = ""

  OnClick() : void{
    let errors = ""

    if(!this.Name){
      errors += "Name can't be empty \n"
    }

    if(!this.Password){
      errors += "Password can't be empty \n"
    }

    if(errors){
      alert(errors)
      return
    }

    let model = new LoginModel (this.Name, this.Password)
    this._authService.login(model).subscribe( response => {
      this.ClearFields()
      alert("you have logged in successfully!")
      this.router.navigate(['/short-url'])
    }, (err: any) => {
       alert(`Login failed because ${err.error}`)
       this.ClearFields()
    })
  }

  private ClearFields() :void{
    this.Name = ""
    this.Password = ""
  }
}
