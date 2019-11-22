import { Component, OnInit } from "@angular/core";
import { GroupService } from "../../../shared/service/group.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { PagedResult } from "../../../shared/models/page-result";
import { GroupUserService } from "../../../shared/service/group-user.service";
import { UserService } from "../../../shared/service/userservice.service";

@Component({
  selector: "app-group",
  templateUrl: "./group.component.html",
  styleUrls: ["./group.component.scss"]
})
export class GroupComponent implements OnInit {
  listU: [];
  idSelect: any;
  filterName: any;
  nameSearch: any;

  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;
  loading = false;

  groupForm: FormGroup;
  _id: "";

  UserId: "";
  GroupId: "";

  items: any[];
  itemsE: any[];
  itemsA: any[];
  value: any;
  nameG: any;
  idG: any;
  temp: any;

  formCre = false;
  formEdit = false;
  list = true;
  nameGroup = false;
  btnAdd = false;

  constructor(
    private userService: UserService,
    private groupuserService: GroupUserService,
    private fb: FormBuilder,
    private groupService: GroupService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.groupForm = this.fb.group({
      NameGroup: ["", Validators.required],
      Description: [""]
    });
    this.loading = true;
    this.getdataGroup(this.page, this.pageSize);
  }
  searchChange(e) {
    this.nameSearch = e.target.value;
    if (this.nameSearch !== "") {
      this.btnAdd = true;
      this.userService.search(this.nameSearch).subscribe((res: any) => {
        this.listU = res.data;
        if (this.listU.length > 0) {
          this.idSelect = res.data[0].id;
        } else {
          this.idSelect = null;
        }
      });
    } else {
      this.btnAdd = false;
    }
  }
  getdataGroup(page, pageSize) {
    this.groupService.getall(page, pageSize).subscribe((data: any) => {
      console.log(data);
      this.items = data.data.items;
      this.pagedResult = data.data;
      this.pagedResult.pageNumber = data.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getdataGroupUser(id) {
    this.groupService.getallbyid(id).subscribe((data: any) => {
      console.log(data);

      this.itemsE = data.data.items;
      this.pagedResult = data.data;
      this.pagedResult.pageNumber = data.data.total;
      this.reloadData();
    });
  }
  reloadData() {
    this.gridView = {
      data: this.pagedResult.items.slice(this.skip, this.skip + this.pageSize),
      total: this.pagedResult.total
    };
  }
  pagedResultChange(data) {
    this.pagedResult = data;
    this.getdataGroup(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getdataGroup(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  OnSubmit() {
    this.loading = true;
    this.groupService.create(this.groupForm.value).subscribe((data: any) => {
      if (data.errorCode !== 0) {
        this.toastrService.error("Thêm nhóm mới thất bại", "Thất bại");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm nhóm mới thành công", "Thành công");
        this.getdataGroup(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.list = true;
      }
    });
  }
  OnUpdate() {
    this.loading = true;
    this.groupService.edit(this._id, this.groupForm.value).subscribe(
      () => {},
      error => {
        this.loading = false;
        this.toastrService.error("Cập nhật nhóm thất bại", "Thất bại");
      },
      () => {
        this.loading = false;
        this.toastrService.success("Cập nhật nhóm thành công", "Thành công");
        this.getdataGroup(this.page, this.pageSize);
        this.formCre = false;
        this.list = true;
        this.nameGroup = false;
        this.formEdit = false;
      }
    );
  }
  addUser() {
    this.userService.search(this.nameSearch).subscribe((res: any) => {
      if (res.data.length > 0) {
        if (res.data[0].username === this.nameSearch) {
          if (this.idSelect !== null) {
            this.groupForm.value.GroupId = this.idG;
            this.groupForm.value.UserId = this.idSelect;
            this.temp = this.groupForm.value;
            this.groupuserService.create(this.temp).subscribe((data: any) => {
              if (data.errorCode === 11) {
                this.toastrService.error(
                  "Người dùng đã tồn tại trong nhóm",
                  "Thất bại"
                );
                this.getdataGroupUser(this.idG);
              } else {
                this.toastrService.success(
                  "Thêm người dùng vào nhóm thành công",
                  "Thành công"
                );
                this.getdataGroupUser(this.idG);
              }
            });
            this.filterName = "";
          }
        }
      } else {
        this.toastrService.error("Không tìm thấy người dùng", "Thất bại");
      }
    });
  }
  gotoInfo(id) {
    this.idG = id;
    this.loading = true;
    this.groupService.getbyid(id).subscribe((data2: any) => {
      console.log(data2);

      this.nameG = data2.data.nameGroup;
      this.getdataGroupUser(id);
      this.nameGroup = true;
      this.list = false;
      this.loading = false;
    });
  }
  gotoDelG(id) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.groupService.delete(id).subscribe((res: any) => {
        if (res.success === false) {
          this.toastrService.error(res.message, "Thất bại");
          this.loading = false;
          this.getdataGroup(this.page, this.pageSize);
        } else {
          this.toastrService.success("Xóa nhóm thành công", "Thành công");
          this.getdataGroup(this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.list = true;
          this.nameGroup = false;
        }
      });
    }
  }
  gotoDelU(id) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.groupuserService.delete(id).subscribe(
        res => {
          this.toastrService.success(
            "Xóa người dùng khỏi nhóm thành công",
            "Thành công"
          );
          this.getdataGroupUser(this.idG);
          this.loading = false;
          this.formCre = false;
          this.list = false;
          this.nameGroup = true;
        },
        err => {
          this.toastrService.error(
            "Xóa người dùng khỏi nhóm thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getdataGroupUser(id);
        }
      );
    }
  }
  gotoCre() {
    this.groupForm.reset();
    this.list = false;
    this.formCre = true;
  }
  gotoEdit(id) {
    this.loading = true;
    this.groupService.getbyid(id).subscribe((data: any) => {
      this._id = data.data.id;
      this.groupForm.setValue({
        NameGroup: data.data.nameGroup,
        Description: data.data.description
      });
      this.value = data.data;
      this.loading = false;
    });
    this.list = false;
    this.formCre = false;
    this.formEdit = true;
  }
  back() {
    this.loading = false;
    this.formCre = false;
    this.list = true;
    this.nameGroup = false;
    this.formEdit = false;
  }
  nameGroupB() {
    this.getdataGroup(this.page, this.pageSize);
    this.formCre = false;
    this.list = true;
    this.nameGroup = false;
  }
}
