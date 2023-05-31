import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ShortUrlService } from '../../services/short-url.service';
import { IShortUrlModel } from '../../models/short-url.model';

@Component({
  selector: 'app-short-url-info',
  templateUrl: './short-url-info.component.html',
  styleUrls: ['./short-url-info.component.css']
})
export class ShortUrlInfoComponent implements OnInit {

  constructor(private readonly router: ActivatedRoute, 
    private readonly _urlService:ShortUrlService,
    private readonly _router: Router){
  }

  model: IShortUrlModel | any;

  ngOnInit(){
    let id = this.router.snapshot.paramMap.get('id')

    if(!id)
      return

    this._urlService.getUrlById(id).subscribe(resp => {
      this.model = resp
    },(err:any) => {
      alert(`Error occurred because ${err.error}`)
    })
  }

  OnClickView(){
    window.open(this.model.longUrl);
  }

  OnClickBackToList(){
    this._router.navigate(["/short-url"])
  }
}
