import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ITokenModel } from "../models/token.model";
import { ILoginModel } from "../models/login.model";
import { environment } from "src/enviroments/enviroment";
import { Observable } from "rxjs/internal/Observable";
import { IRegistrationModel } from "../models/registration.model";
import { IUserModel } from "../../short-url/models/user.model";
import { BehaviorSubject, tap } from "rxjs";
import { UserModel } from "../models/user.model";

@Injectable()
export class AuthService{
    constructor(private http : HttpClient){
        this._isLoggedIn.next(!!this.token)
    }

    private readonly ACCESS_TOKEN_NAME = 'AccessToken'
    private readonly USER_ROLE_KEY = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    private readonly USER_NAME_KEY = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    private readonly USER_ID_KEY = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
    private _isLoggedIn = new BehaviorSubject<boolean>(false)
    IsLoggedIn = this._isLoggedIn.asObservable()

    get token() : string{
        const token = localStorage.getItem(this.ACCESS_TOKEN_NAME)
        if(token)
            return token 
        else
            return ''
    }

    get user() :UserModel{
        if(!this.token)
            return new UserModel("","","")
        
        let data = this.getData(this.token)
        return new UserModel(
            data[this.USER_ID_KEY],
            data[this.USER_ROLE_KEY],
            data[this.USER_NAME_KEY]
        )
    }

    private getData(token : string){
        return JSON.parse(atob(token.split('.')[1]))
    }

    public login(loginModel: ILoginModel) : Observable<ITokenModel>{
        return this.http.post<ITokenModel>(`${environment.apiUrl}/Auth/login`, loginModel).pipe(
            tap(resp => {
                localStorage.setItem(this.ACCESS_TOKEN_NAME, resp.accessToken) 
                
                this._isLoggedIn.next(true)
            })
        );
    }

    public registration(registrationModel: IRegistrationModel) : Observable<IUserModel>{
        return this.http.post<IUserModel>(`${environment.apiUrl}/Auth/registration`, registrationModel);
    }
}