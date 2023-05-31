import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IShortUrlModel } from "../models/short-url.model";
import { Observable, delay } from "rxjs";
import { environment } from "src/enviroments/enviroment";
import { UrlCreateModel } from "../models/url-create.model";

@Injectable()
export class ShortUrlService{
    constructor(private http : HttpClient){}

    getAllUrls() : Observable<IShortUrlModel[]>{
        return this.http.get<IShortUrlModel[]>(`${environment.apiUrl}/UrlShorter`);
    }

    createUrl(urlCreateModel: UrlCreateModel) : Observable<object>{
        return this.http.post(`${environment.apiUrl}/UrlShorter`, urlCreateModel);
    }

    deleteUrl(url: string) : Observable<object>{
        return this.http.delete(`${environment.apiUrl}/UrlShorter/for-user/${url}`);
    }

    getUrlById(id:string) : Observable<IShortUrlModel>{
        return this.http.get<IShortUrlModel>(`${environment.apiUrl}/UrlShorter/${id}`);
    }

}