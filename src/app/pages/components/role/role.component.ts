import { Component, OnInit } from "@angular/core";
import { RoleService } from "../../../shared/service/role.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import {
  FormControl,
  FormGroupDirective,
  FormBuilder,
  FormGroup,
  NgForm,
  Validators
} from "@angular/forms";
import { NgxPaginationModule } from "ngx-pagination";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { PagedResult } from "../../../shared/models/page-result";
import { PermissionService } from "../../../shared/service/permission.service";
import { GroupService } from "../../../shared/service/group.service";

@Component({
  selector: "app-role",
  templateUrl: "./role.component.html",
  styleUrls: ["./role.component.scss"]
})
export class RoleComponent implements OnInit {
  listG: [];
  idSelect: any;
  filterName: any;
  nameSearch: any;

  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;
  loading = false;

  roleForm: FormGroup;
  _id: "";

  items: any;
  value: any;
  idR: any;
  nameR: any;
  itemsInfo: any;
  temp: any;

  formCre = false;
  formUp = false;
  list = true;
  nameRole = false;
  btnAdd = false;

  constructor(
    private fb: FormBuilder,
    private permissionService: PermissionService,
    private roleService: RoleService,
    private groupService: GroupService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.roleForm = this.fb.group({
      NameRole: ["", Validators.required],
      Description: [""]
    });
    this.getData(this.page, this.pageSize);
  }
  searchChange(e: any) {
    this.nameSearch = e.target.value;
    if (this.nameSearch !== "") {
      this.btnAdd = true;
      this.groupService.search(this.nameSearch).subscribe((res: any) => {
        this.listG = res.data;
        if (this.listG.length > 0) {
          this.idSelect = res.data[0].id;
        } else {
          this.idSelect = null;
        }
      });
    } else {
      this.btnAdd = false;
    }
  }
  getData(page: number, pageSize: number) {
    this.roleService.getall(page, pageSize).subscribe((data: any) => {
      if (data.data.totalCount === 0) {
        this.toastrService.error("group user empty");
      }
      this.items = data.data.items;
      this.pagedResult = data.data;
      this.pagedResult.pageNumber = data.data.total;
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
  OnSubmit() {
    this.loading = true;
    this.roleService.create(this.roleForm.value).subscribe((data: any) => {
      if (data.errorCode !== 0) {
        this.toastrService.error("Thêm mới vai trò thất bại", "Thất bại");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm mới vai trò thành công", "Thành công");
        this.getData(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.list = true;
      }
    });
  }
  OnUpdate() {
    this.loading = true;
    this.roleService.edit(this._id, this.roleForm.value).subscribe(
      () => {},
      error => {
        this.loading = false;
        this.toastrService.error("Cập nhật vài trò thất bại", "Thất bại");
      },
      () => {
        this.loading = false;
        this.toastrService.success("Cập nhật vai trò thành công", "Thành công");
        this.getData(this.page, this.pageSize);
        this.formCre = false;
        this.list = true;
        this.formUp = false;
      }
    );
  }
  addUser() {
    this.groupService.search(this.nameSearch).subscribe((res: any) => {
      if (res.data.length > 0) {
        if (res.data[0].nameGroup === this.nameSearch) {
          if (this.idSelect !== null) {
            this.roleForm.value.RoleId = this.idR;
            this.roleForm.value.GroupId = this.idSelect;
            this.temp = this.roleForm.value;
            console.log(this.temp);
            this.permissionService.create(this.temp).subscribe((data: any) => {
              if (data.errorCode === 11) {
                this.toastrService.error(
                  "Nhóm đã tồn tại trong vai trò",
                  "Thất bại"
                );
                this.getdataPermission(this.idR);
              } else {
                this.toastrService.success(
                  "Thêm nhóm vào vai trò thành công",
                  "Thành công"
                );
                this.getdataPermission(this.idR);
              }
            });
            this.filterName = "";
          }
        }
      } else {
        this.toastrService.error("Không tìm thấy nhóm", "Thất bại");
      }
    });
  }
  gotoInfo(id: number) {
    this.idR = id;
    this.loading = true;
    this.roleService.getbyid(id).subscribe((data2: any) => {
      this.nameR = data2.data.nameRole;
      this.getdataPermission(id);
      this.nameRole = true;
      this.list = false;
      this.loading = false;
    });
  }
  getdataPermission(id: number) {
    this.permissionService.getallbyid(id).subscribe((data: any) => {
      this.itemsInfo = data.data.items;
      this.pagedResult = data.data;
      this.pagedResult.pageNumber = data.data.total;
      this.reloadData();
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.roleService.getbyid(id).subscribe((data: any) => {
      this._id = data.data.id;
      this.roleForm.setValue({
        NameRole: data.data.nameRole,
        Description: data.data.description
      });
      this.value = data.data;
      this.loading = false;
    });

    this.formUp = true;
    this.list = false;
  }
  gotoDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.roleService.delete(id).subscribe((res: any) => {
        console.log(res);

        if (res.success === false) {
          this.toastrService.error(res.message, "Thất bại");
          this.loading = false;
          this.getData(this.page, this.pageSize);
        } else {
          this.toastrService.success("Xóa vai trò thành công", "Thành công");
          this.getData(this.page, this.pageSize);
          this.loading = false;
        }
      });
    }
  }
  gotoDelG(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.permissionService.delete(id).subscribe(
        res => {
          this.toastrService.success(
            "Xóa người dùng khỏi nhóm thành công",
            "Thành công"
          );
          this.getdataPermission(this.idR);
          this.loading = false;
          this.formCre = false;
          this.list = false;
          this.nameRole = true;
        },
        err => {
          this.toastrService.error(
            "Xóa người dùng khỏi nhóm thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getdataPermission(id);
        }
      );
    }
  }
  gotoCre() {
    this.roleForm.reset();
    this.list = false;
    this.formCre = true;
  }
  back() {
    this.loading = false;
    this.formCre = false;
    this.list = true;
    this.formUp = false;
  }
  nameRoleB() {
    this.formCre = false;
    this.formUp = false;
    this.list = true;
    this.nameRole = false;
  }
}
