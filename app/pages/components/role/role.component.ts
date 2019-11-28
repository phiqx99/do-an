import { Component, OnInit } from "@angular/core";
import { RoleService } from "../../../shared/service/role.service";
import { ToastrService } from "ngx-toastr";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
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
  listSearch: [];
  idSelect: any;
  valueSearch: any;

  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;

  roleForm: FormGroup;

  itemsRole: any;
  idRole: any;
  nameRole: any;
  itemsGroup: any;
  temp: any;

  formCre = false;
  formUp = false;
  listRole = true;
  listGroup = false;
  btnAdd = false;
  loading = false;

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
  searchChange(value: any) {
    this.valueSearch = value.target.value;
    if (this.valueSearch !== "") {
      this.btnAdd = true;
      this.groupService.search(this.valueSearch).subscribe((res: any) => {
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
  getData(page: number, pageSize: number) {
    this.roleService.getall(page, pageSize).subscribe((data: any) => {
      if (data.data.totalCount === 0) {
        this.toastrService.error("group user empty");
      }
      this.itemsRole = data.data.items;
      this.pagedResult = data.data;
      this.pagedResult.pageNumber = data.data.total;
      this.loading = false;
      this.reloadData();
    });
  }
  getdataPermission(id: number) {
    this.permissionService.getallbyid(id).subscribe((data: any) => {
      this.itemsGroup = data.data.items;
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
    this.roleForm.reset();
    this.listRole = false;
    this.formCre = true;
  }
  OnCreate() {
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
        this.listRole = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.roleService.getbyid(id).subscribe((data: any) => {
      this.idRole = data.data.id;
      this.roleForm.setValue({
        NameRole: data.data.nameRole,
        Description: data.data.description
      });
      this.loading = false;
    });

    this.formUp = true;
    this.listRole = false;
  }
  OnUpdate() {
    this.loading = true;
    this.roleService.edit(this.idRole, this.roleForm.value).subscribe(
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
        this.listRole = true;
        this.formUp = false;
      }
    );
  }
  OnDelRole(id: number) {
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
  gotoInfo(id: number) {
    this.idRole = id;
    this.loading = true;
    this.roleService.getbyid(id).subscribe((data2: any) => {
      this.nameRole = data2.data.nameRole;
      this.getdataPermission(id);
      this.listGroup = true;
      this.listRole = false;
      this.loading = false;
    });
  }
  OnAddGroup() {
    this.groupService.search(this.valueSearch).subscribe((res: any) => {
      if (res.data.length > 0) {
        if (res.data[0].nameGroup === this.valueSearch) {
          if (this.idSelect !== null) {
            this.roleForm.value.RoleId = this.idRole;
            this.roleForm.value.GroupId = this.idSelect;
            this.temp = this.roleForm.value;
            console.log(this.temp);
            this.permissionService.create(this.temp).subscribe((data: any) => {
              if (data.errorCode === 11) {
                this.toastrService.error(
                  "Nhóm đã tồn tại trong vai trò",
                  "Thất bại"
                );
                this.getdataPermission(this.idRole);
              } else {
                this.toastrService.success(
                  "Thêm nhóm vào vai trò thành công",
                  "Thành công"
                );
                this.getdataPermission(this.idRole);
              }
            });
            this.valueSearch = "";
          }
        }
      } else {
        this.toastrService.error("Không tìm thấy nhóm", "Thất bại");
      }
    });
  }
  OnDelGroup(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.permissionService.delete(id).subscribe(
        res => {
          this.toastrService.success(
            "Xóa nhóm khỏi vai trò thành công",
            "Thành công"
          );
          this.getdataPermission(this.idRole);
          this.loading = false;
          this.formCre = false;
          this.listRole = false;
          this.listGroup = true;
        },
        err => {
          this.toastrService.error(
            "Xóa nhóm khỏi vai trò thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getdataPermission(id);
        }
      );
    }
  }
  back() {
    this.loading = false;
    this.formCre = false;
    this.listRole = true;
    this.formUp = false;
  }
  listGroupBack() {
    this.formCre = false;
    this.formUp = false;
    this.listRole = true;
    this.listGroup = false;
  }
}
