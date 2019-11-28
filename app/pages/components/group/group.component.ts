import { Component, OnInit } from "@angular/core";
import { GroupService } from "../../../shared/service/group.service";
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
  listSearch: [];
  idSelect: any;
  valueSearch: any;

  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;

  groupForm: FormGroup;

  UserId: number;
  GroupId: number;

  itemsGroup: any[];
  itemsUser: any[];
  value: any;
  nameGroup: any;
  idG: any;
  temp: any;

  formCre = false;
  formEdit = false;
  listGroup = true;
  listUser = false;
  btnAdd = false;
  loading = false;

  constructor(
    private userService: UserService,
    private groupuserService: GroupUserService,
    private fb: FormBuilder,
    private groupService: GroupService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    console.log(localStorage.getItem("userToken"));

    this.groupForm = this.fb.group({
      NameGroup: ["", Validators.required],
      Description: [""]
    });
    this.loading = true;
    this.getdataGroup(this.page, this.pageSize);
  }
  searchChange(value: any) {
    this.valueSearch = value.target.value;
    if (this.valueSearch !== "") {
      this.btnAdd = true;
      this.userService.search(this.valueSearch).subscribe((res: any) => {
        this.listSearch = res.data;
        if (this.listSearch.length > 0) {
          this.idSelect = res.data[0].id;
        } else {
          this.idSelect = null;
        }
      });
    } else {
      this.btnAdd = false;
    }
  }
  getdataGroup(page: number, pageSize: number) {
    this.groupService.getall(page, pageSize).subscribe((data: any) => {
      console.log(data);
      this.itemsGroup = data.data.items;
      this.pagedResult = data.data;
      this.pagedResult.pageNumber = data.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getdataGroupUser(id: number, page: number, pageSize: number) {
    this.groupService.getallbyid(id, page, pageSize).subscribe((data: any) => {
      console.log(data);

      this.itemsUser = data.data.items;
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
  pagedResultChange(data: any) {
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
  gotoCre() {
    this.groupForm.reset();
    this.listGroup = false;
    this.formCre = true;
  }
  OnCreate() {
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
        this.listGroup = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.groupService.getbyid(id).subscribe((data: any) => {
      this.GroupId = data.data.id;
      this.groupForm.setValue({
        NameGroup: data.data.nameGroup,
        Description: data.data.description
      });
      this.value = data.data;
      this.loading = false;
    });
    this.listGroup = false;
    this.formCre = false;
    this.formEdit = true;
  }
  OnUpdate() {
    this.loading = true;
    this.groupService.edit(this.GroupId, this.groupForm.value).subscribe(
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
        this.listGroup = true;
        this.listUser = false;
        this.formEdit = false;
      }
    );
  }
  OnDelGroup(id: number) {
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
          this.listGroup = true;
          this.listUser = false;
        }
      });
    }
  }
  gotoInfo(id: number) {
    this.idG = id;
    this.loading = true;
    this.groupService.getbyid(id).subscribe((data2: any) => {
      console.log(data2);

      this.nameGroup = data2.data.nameGroup;
      this.getdataGroupUser(id, this.page, this.pageSize);
      this.listUser = true;
      this.listGroup = false;
      this.loading = false;
    });
  }
  OnAddUser() {
    this.userService.search(this.valueSearch).subscribe((res: any) => {
      if (res.data.length > 0) {
        if (res.data[0].username === this.valueSearch) {
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
                this.getdataGroupUser(this.idG, this.page, this.pageSize);
              } else {
                this.toastrService.success(
                  "Thêm người dùng vào nhóm thành công",
                  "Thành công"
                );
                this.getdataGroupUser(this.idG, this.page, this.pageSize);
              }
            });
            this.valueSearch = "";
          }
        }
      } else {
        this.toastrService.error("Không tìm thấy người dùng", "Thất bại");
      }
    });
  }
  OnDelUser(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.groupuserService.delete(id).subscribe(
        res => {
          this.toastrService.success(
            "Xóa người dùng khỏi nhóm thành công",
            "Thành công"
          );
          this.getdataGroupUser(this.idG, this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.listGroup = false;
          this.listUser = true;
        },
        err => {
          this.toastrService.error(
            "Xóa người dùng khỏi nhóm thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getdataGroupUser(id, this.page, this.pageSize);
        }
      );
    }
  }
  back() {
    this.loading = false;
    this.formCre = false;
    this.listGroup = true;
    this.listUser = false;
    this.formEdit = false;
  }
  listUserBack() {
    this.getdataGroup(this.page, this.pageSize);
    this.formCre = false;
    this.listGroup = true;
    this.listUser = false;
  }
}
