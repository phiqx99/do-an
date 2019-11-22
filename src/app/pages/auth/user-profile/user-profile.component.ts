import { Component, OnInit } from "@angular/core";
import { UserService } from "../../../shared/service/userservice.service";
import { animate, style, transition, trigger } from "@angular/animations";
import {
  FormControl,
  FormGroupDirective,
  FormBuilder,
  FormGroup,
  NgForm,
  Validators
} from "@angular/forms";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-user-profile",
  templateUrl: "./user-profile.component.html",
  styleUrls: ["./user-profile.component.scss"],
  animations: [
    trigger("fadeInOutTranslate", [
      transition(":enter", [
        style({ opacity: 0 }),
        animate("400ms ease-in-out", style({ opacity: 1 }))
      ]),
      transition(":leave", [
        style({ transform: "translate(0)" }),
        animate("400ms ease-in-out", style({ opacity: 0 }))
      ])
    ])
  ]
})
export class UserProfileComponent implements OnInit {
  info: any;
  loading = true;
  editProfile = true;
  editProfileIcon = "icofont-edit";

  groupForm: FormGroup;
  id: any;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.groupForm = this.formBuilder.group({
      FullName: ["", Validators.required],
      Gender: ["", Validators.required],
      DateOfBirth: ["", Validators.required],
      Phone: ["", Validators.required],
      Email: ["", Validators.required],
      Address: ["", Validators.required]
    });
    this.getUserProfile();
  }

  toggleEditProfile() {
    this.editProfileIcon =
      this.editProfileIcon === "icofont-close"
        ? "icofont-edit"
        : "icofont-close";
    this.editProfile = !this.editProfile;
  }
  getUserProfile() {
    this.userService.getUserProfile().subscribe((res: any) => {
      this.info = res.data[0];
      this.id = res.data[0].id;
      this.groupForm.setValue({
        FullName: res.data[0].fullName,
        Gender: res.data[0].gender === true ? "true" : "false",
        DateOfBirth: res.data[0].dateOfBirth,
        Phone: res.data[0].phone,
        Email: res.data[0].email,
        Address: res.data[0].address
      });
      this.loading = false;
    });
  }
  Update() {
    this.userService
      .edit(this.id, this.groupForm.value)
      .subscribe((res: any) => {
        if (res.success === false) {
          this.toastrService.error(res.message, "Thất bại");
        } else {
          this.toastrService.success(
            "Cập nhật thông tin thành công",
            "Thành công"
          );
          this.getUserProfile();
          this.toggleEditProfile();
        }
      });
  }
}
