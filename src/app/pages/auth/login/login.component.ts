import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { UserService } from "../../../shared/service/userservice.service";
import { ToastrService } from "ngx-toastr";
import { NgForm, FormGroup, FormBuilder, Validators } from "@angular/forms";
import { KeycloakService } from "../loginKeycloak/keycloak.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  ssosub: any;
  loading = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: UserService,
    private toastr: ToastrService,
    private keycloakService: KeycloakService
  ) {}

  ngOnInit() {
    this.loginForm = this.fb.group({
      UserName: ["", Validators.required],
      Password: ["", Validators.required],
      SSOSub: [""]
    });

    this.loading = true;
    const loca = window.location.href;
    const a = loca.search("code=");
    const b = loca.search("state=");
    const c = loca.search("session_state=");
    const code = loca.slice(a + 5);
    const state = loca.slice(b + 6, c - 1);
    if (loca.length > 100) {
      this.getTokenData(code, state);
    }
    document.querySelector("body").setAttribute("themebg-pattern", "theme1");
    // this.service.getUserProfile().subscribe((data: any) => {
    //   if (data.data.length > 0) {
    //     this.router.navigateByUrl("components");
    //     this.loading = false;
    //   }
    // });
    this.loading = false;
  }
  getTokenData(code, state) {
    this.loading = true;
    this.keycloakService.gettoken(code, state).subscribe((res: any) => {
      this.ssosub = res.data;

      localStorage.setItem("userToken", res.data.token);
      console.log(localStorage.getItem("userToken"));
      if (localStorage.getItem("userToken") !== "undefined") {
        this.toastr.success("Đăng nhập thành công !");
        this.router.navigateByUrl("components/user");
      } else {
        this.toastr.error(res.message);
      }
      // this.service.getUserProfile().subscribe(
      //   (data: any) => {
      //     if (data.data.length > 0) {
      //       this.toastr.success("Đăng nhập thành công !");
      //       this.router.navigateByUrl("components/user");
      //     }
      //   },
      //   err => {
      //     this.toastr.error(res.message);
      //   }
      // );
    });
    this.loading = false;
  }
  OnSubmit() {
    this.loading = true;
    this.loginForm.patchValue({
      SSOSub: this.ssosub
    });
    this.service.login(this.loginForm.value).subscribe(
      (res: any) => {
        console.log(res);

        this.loading = false;
        localStorage.setItem("userToken", res.data.token);
        this.router.navigateByUrl("components/user");
        this.toastr.success("Đăng nhập thành công !");
      },
      error => {
        this.loading = false;
        if (error.status === 401) {
          this.toastr.error(
            "Tài khoản hoặc mật khẩu không đúng",
            "Đăng nhập thất bại"
          );
        } else {
          console.log(error);
        }
      }
    );
  }

  login_keycloak(): void {
    this.keycloakService.login();
  }
  // logout_keycloak() {
  //   this.keycloakService.logout();
  // }
}
