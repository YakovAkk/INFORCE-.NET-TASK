import { Component, OnInit } from '@angular/core';
import { DescriptionService } from '../services/description.service';
import { IDescriptionModel } from '../models/description.model';
import { AuthService } from '../../Auth/services/auth.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  constructor(private readonly _descService: DescriptionService,
    private readonly _authService: AuthService){}

  desc!: IDescriptionModel;
  IsUserAdmin: boolean = false;
  IsEdit : boolean = false

  ngOnInit(){
    this.fetchDescription()

    if(this._authService.user.role == 'Admin')
      this.IsUserAdmin = true
  }

  fetchDescription(){
    this._descService.getDescription().subscribe(resp => {
      this.desc = resp
    }, (err:any) =>{
      console.log(err);
      alert(`Error is ${err.error}`)
    })
  }

  OnClickEdit(){
    this.IsEdit = true
  }

  OnClickSubmit(){
    this._descService.setDescription(this.desc).subscribe(resp => {
      this.IsEdit = false
      this.fetchDescription()
    }, (err:any) => {
      alert(`Error because ${err.error} `)
    })
  }
}
