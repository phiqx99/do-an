import { Injectable } from "@angular/core";
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from "@angular/router";
import { UserService } from "../../shared/service/userservice.service";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private service: UserService) {}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    this.service.getUserProfile().subscribe((data: any) => {
      if (data.data.length > 0) {
        return true;
      } else {
        this.router.navigate(["login"]);
        return false;
      }
    });
    if (localStorage.getItem("userToken") != null) {
      return true;
    } else {
      this.router.navigate(["login"]);
      return false;
    }
  }
}
