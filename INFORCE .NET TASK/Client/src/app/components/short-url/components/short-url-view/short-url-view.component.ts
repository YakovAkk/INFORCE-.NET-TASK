import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IShortUrlModel } from '../../models/short-url.model';
import { ShortUrlService } from '../../services/short-url.service';
import { AuthService } from 'src/app/components/Auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-short-url-view',
  templateUrl: './short-url-view.component.html',
  styleUrls: ['./short-url-view.component.css']
})
export class ShortUrlViewComponent{
  constructor(private readonly _urlService: ShortUrlService, 
    public readonly _authService: AuthService,
    private readonly _router: Router){

  }

  @Input() 
  model: IShortUrlModel | any;

  @Output()
  onFetchUrls = new EventEmitter() 

  OnClickInfo(model: IShortUrlModel) :void{
    this._router.navigate([`/short-url/url/${model.id}`])
  }

  OnClickDelete(model: IShortUrlModel) :void{
    this._urlService.deleteUrl(model.urlCode).subscribe(resp => {
      this.onFetchUrls.emit()
    }, (err:any) => {
      console.log(err);
      alert(`Deletion unsuccessful because! ${err.error} `)
    })
  }
}
