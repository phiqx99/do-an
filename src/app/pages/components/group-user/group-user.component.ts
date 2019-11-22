import { Component, OnInit } from '@angular/core';
import { GroupUserService } from '../../../shared/service/group-user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';
import { PagedResult } from '../../../shared/models/page-result';

@Component({
  selector: 'app-group-user',
  templateUrl: './group-user.component.html',
  styleUrls: ['./group-user.component.scss']
})
export class GroupUserComponent implements OnInit {

//   pageSize = 10;
//   skip = 0;
//   page = 1;
//   gridView: GridDataResult;
//   pagedResult: PagedResult<any>;
//   loading = false;

//   group_userForm: FormGroup;
//   _id: '';
//   UserId: '';
//   GroupId: '';
//   Active: '';
//   CreatedAt: '';
//   UpdateAt: '';
//   CreatedUser: '';
//   UpdateUser: '';
//   idUserUpdate: '';

//   items: any;
//   value: any;
//   lstNameGroup: any;
//   lstNameUser: any;

//   // tslint:disable-next-line:no-inferrable-types
//   formCre: boolean = false;
//   // tslint:disable-next-line:no-inferrable-types
//   formUp: boolean = false;
//   // tslint:disable-next-line:no-inferrable-types
//   list: boolean = false;

//   // tslint:disable-next-line:max-line-length
  constructor(private fb: FormBuilder, private groupuserService: GroupUserService,
 private router: Router, private toastrService: ToastrService) {
  }

  ngOnInit() {
  }
}
//     this.loading = true;
//     this.group_userForm = this.fb.group({
//       GroupId: ['', Validators.required],
//       UserId: ['', Validators.required],
//       Active: ['', Validators.required],
//       CreatedAt: ['', Validators.required],
//       UpdateAt: ['', Validators.required],
//       CreatedUser: ['', Validators.required],
//       UpdateUser: ['', Validators.required]
//     });
//       this.getData(this.page, this.pageSize);
//   }
//   getData(page, pageSize) {
//     this.groupuserService.getall(page, pageSize).subscribe((data: any) => {
//       if (data.data.totalCount === 0) {
//         this.toastrService.error('group user empty');
//       }
//       this.items = data.data.items;
//       this.pagedResult = data.data;
//       this.pagedResult.pageNumber = data.data.total;
//       this.loading = false;
//       this.reloadData();
//     });
//   }
//   reloadData() {
//     this.gridView = {
//       data: this.pagedResult.items.slice(this.skip, this.skip + this.pageSize),
//       total: this.pagedResult.total
//     };
//   }
//   pagedResultChange(data) {
//     this.pagedResult = data;
//     this.getData(data.pageNo, data.pageSize);
//     this.pageSize = data.pageSize;
//     this.reloadData();
//   }

//   pageSizeChange(size: number) {

//     this.pageSize = size;
//     this.pagedResult.pageSize = size;
//     this.getData(1, this.pageSize);
//     this.reloadData();
//   }
//   pageChange(event: PageChangeEvent): void {
//     this.skip = event.skip;
//   }
//   OnSubmit() {
//     this.loading = true;
//     this.groupuserService.create(this.group_userForm.value)
//       .subscribe((data: any) => {
//         if (data.errorCode !== 0) {
//           this.toastrService.error(data.message, 'False');
//           this.loading = false;
//         } else {
//           this.toastrService.success('Insert success.', 'Success');
//           this.getData(this.page, this.pageSize);

//           this.loading = false;
//           this.formCre = false;
//           this.list = false;
//         }
//       });
//   }
//   OnUpdate() {
//     this.loading = true;
//     // this.group_userForm.value.userId = this.idUserUpdate;
//     console.log('  this.group_userForm.value', this.group_userForm.value);
//     this.groupuserService.edit(this._id, this.group_userForm.value)
//       .subscribe((data: any) => {
//         if (data.errorCode !== 0) {
//           this.loading = false;
//           this.toastrService.error(data.message, 'False');
//         } else {
//           this.loading = false;
//           this.toastrService.success('Update success.', 'Success');
//           this.getData(this.page, this.pageSize);
//           this.formCre = false;
//           this.list = false;
//           this.formUp = false;
//         }
//       });
//   }
//   gotoEdit(id) {
//     this.loading = true;
//     this.groupuserService.getbyid(id)
//       .subscribe((groupuser: any) => {
//         // get list groupname
//         this.groupuserService.getGroup().subscribe((group: any) => {
//           this.lstNameGroup = group.data;
//           // get list username
//           this.groupuserService.getUser().subscribe((user: any) => {
//             this.lstNameUser = user.data;
//             this._id = groupuser.data.id;
//             this.group_userForm.setValue({
//               GroupId: groupuser.data.groupId,
//               UserId: groupuser.data.userId,
//               Active: groupuser.data.active,
//               CreatedAt: groupuser.data.createdAt,
//               UpdateAt: groupuser.data.updateAt,
//               CreatedUser: groupuser.data.createdUser,
//               UpdateUser: groupuser.data.updateUser
//             });
//             this.value = groupuser.data;
//             this.loading = false;
//           });

//           this.formUp = true;
//           this.list = true;
//         });
//       });
//   }
//   gotoDel(id: number) {
//     this.loading = true;
//     this.groupuserService.delete(id)
//       .subscribe(res => {
//         this.loading = false;
//         this.toastrService.success('Delete success.', 'Success');
//         this.getData(this.page, this.pageSize);
//       }, (err) => {
//         console.log(err);
//         this.toastrService.error('Delete false.', 'False');
//         this.loading = false;
//         this.getData(this.page, this.pageSize);
//       });
//   }
//   gotoCre() {
//     // get list groupname
//     this.groupuserService.getGroup().subscribe((group: any) => {
//       this.lstNameGroup = group.data;
//       // get list username
//       this.groupuserService.getUser().subscribe((user: any) => {
//         this.lstNameUser = user.data;
//         this.group_userForm.reset();
//         this.list = true;
//         this.formCre = true;
//       });
//     });
//   }
//   back() {
//     this.formCre = false;
//     this.list = false;
//     this.formUp = false;
//   }
// }
