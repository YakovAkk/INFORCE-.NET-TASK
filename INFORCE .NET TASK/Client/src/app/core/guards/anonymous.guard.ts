import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "src/app/components/Auth/services/auth.service";

@Injectable()
export class AnonymousGuard implements CanActivate {

  constructor(private readonly _authService: AuthService) { 
  }

  canActivate(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
     
      let roles = route.data["roles"]
      return roles.includes(this._authService.user.role)
  }
  
};
