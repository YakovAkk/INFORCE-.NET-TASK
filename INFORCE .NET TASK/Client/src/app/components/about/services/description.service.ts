import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/enviroments/enviroment";
import { IDescriptionModel } from "../models/description.model";

@Injectable()
export class DescriptionService{
    constructor(private http : HttpClient){}

    getDescription() : Observable<IDescriptionModel>{
        return this.http.get<IDescriptionModel>(`${environment.apiUrl}/Description`);
    }

    setDescription(desc: IDescriptionModel) : Observable<object>{
        return this.http.post(`${environment.apiUrl}/Description`, desc);
    }
}