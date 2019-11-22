import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { UserService } from "../../shared/service/userservice.service";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: UserService) {}

  canActivate() {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(["/login"]);
      console.log(localStorage.getItem("userToken"));
      return false;
    } else {
      // this.router.navigateByUrl("components");
      return true;
    }
  }
}
