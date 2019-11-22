import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { PagedResult } from "../../../shared/models/page-result";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { SchoolService } from "../../../shared/service/school.service";
import { TopicService } from "../../../shared/service/topic.service";

@Component({
  selector: "app-school",
  templateUrl: "./school.component.html",
  styleUrls: ["./school.component.scss"]
})
export class SchoolComponent implements OnInit {
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

  itemsSchool: any[];
  itemsEdit: any[];
  nameSchool: any;
  idSchool: any;

  formCre = false;
  formEdit = false;
  listSchool = true;
  listTopic = false;
  btnAdd = false;
  loading = false;

  constructor(
    private schoolService: SchoolService,
    private topicService: TopicService,
    private fb: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.form = this.fb.group({
      NameSchool: ["", Validators.required],
      Phone: ["", Validators.required],
      Email: ["", Validators.required],
      Address: ["", Validators.required],
      Description: [""]
    });
    this.getDataSchool(this.page, this.pageSize);
  }
  getDataSchool(page: number, pageSize: number) {
    this.schoolService.getAll(page, pageSize).subscribe((res: any) => {
      this.itemsSchool = res.data.items;
      this.pagedResult = res.data;
      this.pagedResult.pageNumber = res.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getDataTopicAll(id: number) {
    this.schoolService.getTopicBySchoolId(id).subscribe((data: any) => {
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
    this.getDataSchool(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getDataSchool(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  gotoCre() {
    this.form.reset();
    this.listSchool = false;
    this.formCre = true;
  }
  OnCreate() {
    this.loading = true;
    this.schoolService.create(this.form.value).subscribe((res: any) => {
      if (res.errorCode !== 0) {
        this.toastrService.error(res.message, "Thất bại");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm thể loại thành công.", "Thành công");
        this.getDataSchool(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.listSchool = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.schoolService.getById(id).subscribe((res: any) => {
      this._id = res.data.id;
      this.form.setValue({
        NameSchool: res.data.nameSchool,
        Phone: res.data.phone,
        Email: res.data.email,
        Address: res.data.address,
        Description: res.data.description
      });
      this.loading = false;
    });
    this.loading = false;
    this.listSchool = false;
    this.formCre = false;
    this.formEdit = true;
  }
  OnUpdate() {
    this.loading = true;
    this.schoolService.edit(this._id, this.form.value).subscribe(
      () => {},
      error => {
        this.loading = false;
        this.toastrService.error("Cập nhật giai đoạn thất bại", "Thất bại");
      },
      () => {
        this.loading = false;
        this.toastrService.success(
          "Cập nhật giai đoạn thành công",
          "Thành công"
        );
        this.getDataSchool(this.page, this.pageSize);
        this.formCre = false;
        this.listSchool = true;
        this.listTopic = false;
        this.formEdit = false;
      }
    );
  }
  OnDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.schoolService.delete(id).subscribe(
        () => {
          this.toastrService.success("Xóa giai đoạn thành công", "Thành công");
          this.getDataSchool(this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.listSchool = true;
          this.listTopic = false;
        },
        err => {
          this.toastrService.error("Xóa giai đoạn thất bại", "Thất bại");
          this.getDataSchool(this.page, this.pageSize);
          this.loading = false;
        }
      );
    }
  }
  gotoInfo(id: number) {
    this.loading = true;
    this.idSchool = id;
    this.schoolService.getById(id).subscribe((data: any) => {
      this.nameSchool = data.data.nameSchool;
    });
    this.getDataTopicAll(id);
    this.listTopic = true;
    this.listSchool = false;
    this.loading = false;
  }
  searchChange(e: any) {
    this.valueSearch = e.target.value;
    if (this.valueSearch !== "") {
      this.btnAdd = true;
      this.topicService.search(this.valueSearch).subscribe((res: any) => {
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
  OnAddTopic() {
    this.topicService.search(this.valueSearch).subscribe((res: any) => {
      if (res.data.length > 0) {
        if (res.data[0].nameTopic === this.valueSearch) {
          if (this.idSelect !== null) {
            this.form.value.SchoolId = this.idSchool;
            const temp = this.form.value;
            console.log(temp);

            this.schoolService
              .updateTopicAll(this.idSelect, temp)
              .subscribe((data: any) => {
                if (data.success === false) {
                  this.toastrService.error(
                    "Đề tài đã tồn tại trong giai đoạn này",
                    "Thất bại"
                  );
                  this.getDataTopicAll(this.idSchool);
                } else {
                  this.toastrService.success(
                    "Thêm đề tài vào " + this.nameSchool + " thành công",
                    "Thành công"
                  );
                  this.getDataTopicAll(this.idSchool);
                }
              });
          }
        }
      } else {
        this.toastrService.error("Không tìm thấy đề tài", "Thất bại");
      }
    });
  }
  OnDelTopic(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.form.value.SchoolId = null;
      const temp = this.form.value;
      this.schoolService.updateTopicAll(id, temp).subscribe(
        res => {
          this.toastrService.success(
            "Xóa đề tài khỏi " + this.nameSchool + " thành công",
            "Thành công"
          );
          this.getDataTopicAll(this.idSchool);
          this.loading = false;
          this.formCre = false;
          this.listSchool = false;
          this.listTopic = true;
        },
        err => {
          this.toastrService.error(
            "Xóa đề tài khỏi " + this.nameSchool + " thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getDataTopicAll(this.idSchool);
        }
      );
    }
  }
  backtoListSchool() {
    this.formCre = false;
    this.listSchool = true;
    this.listTopic = false;
    this.formEdit = false;
  }
  listTopicBack() {
    this.getDataSchool(this.page, this.pageSize);
    this.formCre = false;
    this.listSchool = true;
    this.listTopic = false;
    this.formEdit = false;
  }
}
