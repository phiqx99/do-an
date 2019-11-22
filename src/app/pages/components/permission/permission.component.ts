import { Component, OnInit } from '@angular/core';
import { PermissionService } from '../../../shared/service/permission.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { PagedResult } from '../../../shared/models/page-result';
import { GroupService } from '../../../shared/service/group.service';

@Component({
  selector: 'app-permission',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.scss']
})
export class PermissionComponent implements OnInit {

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

  permissionForm: FormGroup;
  _id: '';
  GroupId: '';
  RoleId: '';
  idUserUpdate: '';
  items: any;
  value: any;
  lstNameGroup: any;
  lstNameRole: any;

  formCre = false;
  formUp = false;
  list = false;
  btnAdd = false;


  // tslint:disable-next-line:max-line-length
  constructor(private fb: FormBuilder, private groupService: GroupService, private permissionService: PermissionService, private router: Router, private toastrService: ToastrService) {
  }

  ngOnInit() {
    this.loading = true;
    this.permissionForm = this.fb.group({
      GroupId: ['', Validators.required],
      RoleId: ['', Validators.required],
    });
    this.getData(this.page, this.pageSize);

  }
  searchChange(e) {
    this.nameSearch = e.target.value;
    if (this.nameSearch !== '') {
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
  getData(page, pageSize) {
    this.permissionService.getall(page, pageSize).subscribe((data: any) => {
      if (data.data.totalCount === 0) {
        this.toastrService.error('group user empty');
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
  pagedResultChange(data) {
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
    this.permissionService.create(this.permissionForm.value)
      .subscribe((data: any) => {
        if (data.errorCode !== 0) {
          this.toastrService.error('Thêm quyền mới thất bại', 'Thất bại');
          this.loading = false;
        } else {
          this.toastrService.success('Thêm quyền mới thành công', 'Thành công');
          this.getData(this.page, this.pageSize);

          this.loading = false;
          this.formCre = false;
          this.list = false;
        }
      });
  }
  OnUpdate() {
    this.loading = true;
    // this.group_userForm.value.userId = this.idUserUpdate;
    this.permissionService.edit(this._id, this.permissionForm.value)
      .subscribe((data: any) => {
        if (data.errorCode !== 0) {
          this.loading = false;
          this.toastrService.error('Cập nhật quyền thất bại', 'Thất bại');
        } else {
          this.loading = false;
          this.toastrService.success('Cập nhật quyền thành công', 'Thành công');
          this.getData(this.page, this.pageSize);
          this.formCre = false;
          this.list = false;
          this.formUp = false;
        }
      });
      this.filterName = '';
  }
  gotoEdit(id) {
    this.loading = true;
    this.permissionService.getbyid(id)
      .subscribe((permission: any) => {
        // get list groupname
        this.permissionService.getGroup().subscribe((group: any) => {
          this.lstNameGroup = group.data.items;
          // get list username
          this.permissionService.getRole().subscribe((role: any) => {
            this.lstNameRole = role.data.items;
            this._id = permission.data.id;
            this.permissionForm.setValue({
              GroupId: permission.data.groupId,
              RoleId: permission.data.roleId,
            });
            this.value = permission.data;
            this.loading = false;
          });

          this.formUp = true;
          this.list = true;
        });
      });
  }
  gotoDel(id: number) {
    if (confirm('Xác nhận xóa !')) {
      this.loading = true;
      this.permissionService.delete(id)
        .subscribe(res => {
          this.loading = false;
          this.toastrService.success('Xóa quyền thành công', 'Thành công');
          this.getData(this.page, this.pageSize);
        }, (err) => {
          this.toastrService.error('Xóa quyền thất bại', 'Thất bại');
          this.loading = false;
          this.getData(this.page, this.pageSize);
        });
    }
  }
  gotoCre() {
    this.permissionService.getGroup().subscribe((group: any) => {
      this.lstNameGroup = group.data.items;
      this.permissionService.getRole().subscribe((user: any) => {
        this.lstNameRole = user.data.items;
        this.permissionForm.reset();
        this.list = true;
        this.formCre = true;
      });
    });
  }
  back() {
    this.formCre = false;
    this.list = false;
    this.formUp = false;
  }
}
