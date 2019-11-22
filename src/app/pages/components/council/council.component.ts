import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { PagedResult } from "../../../shared/models/page-result";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { CouncilService } from "../../../shared/service/council.service";
import { UserService } from "../../../shared/service/userservice.service";
import { GroupUserService } from "../../../shared/service/group-user.service";

@Component({
  selector: "app-council",
  templateUrl: "./council.component.html",
  styleUrls: ["./council.component.scss"]
})
export class CouncilComponent implements OnInit {
  listSearch: [];
  idSelect: any;
  valueSearch: any;

  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;

  form: FormGroup;
  _id: "";

  itemsCouncil: any[];
  itemsEdit: any[];
  nameCouncil: any;
  idCouncil: any;

  formCre = false;
  formEdit = false;
  listCouncil = true;
  listUser = false;
  btnAdd = false;
  loading = false;

  constructor(
    private councilService: CouncilService,
    private userService: UserService,
    private fb: FormBuilder,
    private groupUserService: GroupUserService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.form = this.fb.group({
      NameCouncil: ["", Validators.required],
      Description: [""]
    });
    this.getDataCouncil(this.page, this.pageSize);
  }
  getDataCouncil(page: number, pageSize: number) {
    this.councilService.getAll(page, pageSize).subscribe((res: any) => {
      this.itemsCouncil = res.data.items;
      this.pagedResult = res.data;
      this.pagedResult.pageNumber = res.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getDataUser(id: number) {
    this.councilService.getUserByCouncilId(id).subscribe((data: any) => {
      console.log(data);

      this.itemsEdit = data.data.items;
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
    this.getDataCouncil(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getDataCouncil(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  gotoCre() {
    this.form.reset();
    this.listCouncil = false;
    this.formCre = true;
  }
  OnCreate() {
    this.loading = true;
    this.councilService.create(this.form.value).subscribe((res: any) => {
      if (res.errorCode !== 0) {
        this.toastrService.error(res.message, "False");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm thể loại thành công.", "Success");
        this.getDataCouncil(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.listCouncil = true;
      }
    });
  }
  gotoEdit(id: any) {
    this.loading = true;
    this.councilService.getById(id).subscribe((res: any) => {
      this._id = res.data.id;
      this.form.setValue({
        NameCouncil: res.data.nameCouncil,
        Description: res.data.description
      });
      this.loading = false;
    });
    this.loading = false;
    this.listCouncil = false;
    this.formCre = false;
    this.formEdit = true;
  }
  OnUpdate() {
    this.loading = true;
    this.councilService.edit(this._id, this.form.value).subscribe(
      (res: any) => {},
      error => {
        this.loading = false;
        this.toastrService.error("Update false.", "False");
      },
      () => {
        this.loading = false;
        this.toastrService.success("Update success.", "Success");
        this.getDataCouncil(this.page, this.pageSize);
        this.formCre = false;
        this.listCouncil = true;
        this.listUser = false;
        this.formEdit = false;
      }
    );
  }
  OnDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.councilService.delete(id).subscribe(
        res => {
          this.toastrService.success("Xóa giai đoạn thành công", "Thành công");
          this.getDataCouncil(this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.listCouncil = true;
          this.listUser = false;
        },
        err => {
          this.toastrService.error("Xóa giai đoạn thất bại", "Thất bại");
          this.getDataCouncil(this.page, this.pageSize);
          this.loading = false;
        }
      );
    }
  }
  gotoInfo(id: any) {
    this.loading = true;
    this.idCouncil = id;
    this.councilService.getById(id).subscribe((data: any) => {
      this.nameCouncil = data.data.nameCouncil;
    });
    this.getDataUser(id);
    this.listUser = true;
    this.listCouncil = false;
    this.loading = false;
  }
  searchChange(e: any) {
    this.valueSearch = e.target.value;
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
  OnAddUser() {
    this.userService.search(this.valueSearch).subscribe((res: any) => {
      if (res.data.length > 0) {
        if (res.data[0].username === this.valueSearch) {
          if (this.idSelect !== null) {
            this.form.value.CouncilId = this.idCouncil;
            this.form.value.UserId = this.idSelect;
            const temp = this.form.value;
            this.groupUserService.create(temp).subscribe((data: any) => {
              if (data.success === false) {
                this.toastrService.error(
                  "Người dùng đã tồn tại trong hội đồng",
                  "Thất bại"
                );
                this.getDataUser(this.idCouncil);
              } else {
                this.toastrService.success(
                  "Thêm người dùng vào hội đồng thành công",
                  "Thành công"
                );
                this.getDataUser(this.idCouncil);
              }
            });
            // this.filterName = '';
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
      this.councilService.delete(id).subscribe(
        res => {
          this.toastrService.success(
            "Xóa người dùng khỏi " + this.nameCouncil + " thành công",
            "Thành công"
          );
          this.getDataUser(this.idCouncil);
          this.loading = false;
          this.formCre = false;
          this.listCouncil = false;
          this.listUser = true;
        },
        err => {
          this.toastrService.error(
            "Xóa người dùng khỏi " + this.nameCouncil + " thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getDataUser(this.idCouncil);
        }
      );
    }
  }
  backtoListCouncil() {
    this.formCre = false;
    this.listCouncil = true;
    this.listUser = false;
    this.formEdit = false;
  }
  listUserBack() {
    this.getDataCouncil(this.page, this.pageSize);
    this.formCre = false;
    this.listCouncil = true;
    this.listUser = false;
    this.formEdit = false;
  }
}
