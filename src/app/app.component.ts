import { Component, OnInit } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import { UserService } from "./shared/service/userservice.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {
  title = "Wellcome ! Dashboard Admin";
  loading = false;
  constructor(private router: Router, private service: UserService) {}

  ngOnInit() {
    // this.router.events.subscribe(evt => {
    //   if (!(evt instanceof NavigationEnd)) {
    //     return;
    //   }
    //   window.scrollTo(0, 0);
    // });
    // const loca = window.location.href;
    // if (localStorage.getItem("userToken")) {
    //   this.loading = true;
    //   this.service.getUserProfile().subscribe(
    //     (data: any) => {
    //       // if (data.data.length === 0) {
    //       //   this.router.navigateByUrl("com");
    //       // }
    //       this.loading = false;
    //     },
    //     () => {
    //       this.router.navigateByUrl("login");
    //       this.loading = false;
    //     }
    //   );
    // }
  }
}
