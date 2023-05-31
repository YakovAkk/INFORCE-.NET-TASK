import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { RegistrationModel } from '../../models/registration.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {

  constructor(private readonly _authService : AuthService, private router: Router){  }


  Name = ""
  Password = ""
  ConfirmPassword = ""

  OnClick() : void{
    let errors = ""

    if(!this.Name){
      errors += "Name can't be empty \n"
    }

    if(!this.Password){
      errors += "Password can't be empty \n"
    }

    if(!this.ConfirmPassword){
      errors += "ConfirmPassword can't be empty \n"
    }

    if(errors){
      alert(errors)
      return
    }

    let model = new RegistrationModel (this.Name, this.Password, this.ConfirmPassword)
    this._authService.registration(model).subscribe( response => {
      this.ClearFields()
      alert("you have registered successfully! Now you can log in!")
      this.router.navigate(['/login'])
    }, (err: any) => {
       alert(`Registration failed because ${err.error}`)
       this.ClearFields()
    })
  }

  private ClearFields() :void{
    this.Name = ""
    this.Password = ""
    this.ConfirmPassword = ""
  }
}
