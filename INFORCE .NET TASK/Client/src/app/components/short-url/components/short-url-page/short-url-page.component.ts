import { Component, OnInit } from '@angular/core';
import { ShortUrlService } from '../../services/short-url.service';
import { IShortUrlModel } from '../../models/short-url.model';
import { AuthService } from 'src/app/components/Auth/services/auth.service';
import { UrlCreateModel } from '../../models/url-create.model';

@Component({
  selector: 'app-short-url-page',
  templateUrl: './short-url-page.component.html',
  styleUrls: ['./short-url-page.component.css']
})
export class ShortUrlPageComponent implements OnInit {
  constructor(private readonly _urlService : ShortUrlService,
     public readonly _authService: AuthService){}

    UrlName = ""

  Urls!: IShortUrlModel[];
  loading = false
  ngOnInit(): void {
    this.fetchUrls()
  }

  fetchUrls(){
    this.loading = true
    this._urlService.getAllUrls().subscribe(response => {  
      this.Urls = response;
      this.loading = false
    })
  }

  AddUrlClick(){
    if(!this.UrlName){
      alert("Fill url name!")
      return
    }

    const model = new UrlCreateModel(this.UrlName)
    this._urlService.createUrl(model).subscribe(
      response => {
        this.ClearFields()
        this.fetchUrls()
      }, (err: any) => {
        alert(`Url didn't create because ${err.error}`)
        this.ClearFields()
      }
    )
    
  }

  private ClearFields() :void{
    this.UrlName = ""
  }
}
