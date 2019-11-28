import { Component, OnInit } from "@angular/core";
import { UserService } from "../../../shared/service/userservice.service";
import { ToastrService } from "ngx-toastr";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { PagedResult } from "../../../shared/models/page-result";
import { DatePipe } from "@angular/common";

@Component({
  selector: "app-user",
  templateUrl: "./user.component.html",
  styleUrls: ["./user.component.scss"]
})
export class UserComponent implements OnInit {
  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;

  userForm: FormGroup;
  idUser: number;

  itemsUser: any;
  value: any;
  userErr = false;
  phoneErr = false;
  emailErr = false;
  formCre = false;
  formUp = false;
  listUser = true;
  loading = false;

  gender: any;
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastrService: ToastrService,
    public datepipe: DatePipe
  ) {}
  ngOnInit() {
    this.loading = true;
    this.userForm = this.fb.group({
      UserName: ["", Validators.required],
      Password: ["", Validators.required],
      FullName: ["", Validators.required],
      Gender: ["", Validators.required],
      DateOfBirth: ["", Validators.required],
      Phone: ["", Validators.required],
      Email: ["", Validators.required],
      Address: ["", Validators.required]
    });
    this.getData(this.page, this.pageSize);
  }
  getData(page: number, pageSize: number) {
    this.userService.getAll(page, pageSize).subscribe((data: any) => {
      if (data.data.model.totalCount === 0) {
        this.toastrService.error("group user empty");
      }
      this.itemsUser = data.data.model.items;
      this.pagedResult = data.data.model;
      this.pagedResult.pageNumber = data.data.model.total;
      this.loading = false;
      this.reloadData();
    });
  }
  reloadData() {
    this.gridView = {
      data: this.pagedResult.items.slice(this.skip, this.skip + this.pageSize),
      total: this.pagedResult.total
    };
  }
  pagedResultChange(data: any) {
    this.pagedResult = data;
    this.getData(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getData(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  gotoCre() {
    this.userErr = false;
    this.phoneErr = false;
    this.emailErr = false;
    this.userForm.reset();
    this.listUser = false;
    this.formCre = true;
  }
  OnCreate() {
    this.loading = true;
    this.userService.create(this.userForm.value).subscribe((data: any) => {
      if (data.errorCode !== 0) {
        this.toastrService.error("Thêm người dùng thất bại", "Thất bại");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm người dùng thành công", "Thành công");
        this.getData(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.listUser = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.userService.getById(id).subscribe((data: any) => {
      this.idUser = data.data.id;
      // const date = this.datepipe.transform(data.data.dateOfBirth, "dd-MM-yyyy");
      console.log(data.data.gender === true ? "true" : "false");

      this.userForm.setValue({
        UserName: data.data.username,
        Password: data.data.password,
        FullName: data.data.fullName,
        Gender: data.data.gender === true ? "true" : "false",
        DateOfBirth: data.data.dateOfBirth,
        Phone: data.data.phone,
        Email: data.data.email,
        Address: data.data.address
      });
      this.value = data.data;
      this.loading = false;
    });

    this.formUp = true;
    this.listUser = false;
  }
  OnUpdate() {
    this.loading = true;
    this.userService.edit(this.idUser, this.userForm.value).subscribe(
      (data: any) => {
        console.log(data);

        this.loading = false;
        this.toastrService.success(
          "Cập nhật người dùng thành công",
          "Thành công"
        );
        this.getData(this.page, this.pageSize);
        this.formCre = false;
        this.listUser = true;
        this.formUp = false;
      },
      error => {
        this.loading = false;
        this.toastrService.error("Cập nhật người dùng thất bại", "Thất bại");
      }
    );
  }
  OnDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.userService.delete(id).subscribe((res: any) => {
        if (res.success === false) {
          this.toastrService.error(res.message, "Thất bại");
          this.loading = false;
          this.getData(this.page, this.pageSize);
        } else {
          this.loading = false;
          this.toastrService.success("Xóa người dùng thành công", "Thành công");
          this.getData(this.page, this.pageSize);
        }
      });
    }
  }
  checkUsername($event: any) {
    const value = $event.target.value;
    if (value === "") {
    } else {
      this.userService.checkvalid(value).subscribe((res: any) => {
        if (true === res.success) {
          this.userErr = true;
        } else {
          this.userErr = false;
        }
      });
    }
  }
  checkPhone($event: any) {
    const phone = $event.target.value;
    if (phone === "") {
    } else {
      this.userService.checkvalid(phone).subscribe((res: any) => {
        if (true === res.success) {
          this.phoneErr = true;
        } else {
          this.phoneErr = false;
        }
      });
    }
  }
  checkEmail($event: any) {
    const email = $event.target.value;
    if (email === "") {
    } else {
      this.userService.checkvalid(email).subscribe((res: any) => {
        if (true === res.success) {
          this.emailErr = true;
        } else {
          this.emailErr = false;
        }
      });
    }
  }
  back() {
    this.userErr = false;
    this.phoneErr = false;
    this.emailErr = false;
    this.loading = false;
    this.formCre = false;
    this.listUser = true;
    this.formUp = false;
  }
}
