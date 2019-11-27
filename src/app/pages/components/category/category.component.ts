import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { PagedResult } from "../../../shared/models/page-result";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { CategoryService } from "../../../shared/service/category.service";
import { TopicService } from "../../../shared/service/topic.service";

@Component({
  selector: "app-category",
  templateUrl: "./category.component.html",
  styleUrls: ["./category.component.scss"]
})
export class CategoryComponent implements OnInit {
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

  itemsCategory: any[];
  itemsEdit: any[];
  nameCategory: any;
  idCategory: any;

  formCre = false;
  formEdit = false;
  listCategory = true;
  listTopic = false;
  btnAdd = false;
  loading = false;

  constructor(
    private categoryService: CategoryService,
    private topicService: TopicService,
    private fb: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.form = this.fb.group({
      NameCategory: ["", Validators.required],
      Description: [""]
    });
    this.getDataCategory(this.page, this.pageSize);
  }
  getDataCategory(page: number, pageSize: number) {
    this.categoryService.getAll(page, pageSize).subscribe((res: any) => {
      this.itemsCategory = res.data.items;
      this.pagedResult = res.data;
      this.pagedResult.pageNumber = res.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getDataTopic(id: number) {
    this.categoryService.getTopicByCategoryId(id).subscribe((data: any) => {
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
    this.getDataCategory(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getDataCategory(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  gotoCre() {
    this.form.reset();
    this.listCategory = false;
    this.formCre = true;
  }
  OnCreate() {
    this.loading = true;
    this.categoryService.create(this.form.value).subscribe((res: any) => {
      if (res.errorCode !== 0) {
        this.toastrService.error(res.message, "False");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm đề tài thành công.", "Success");
        this.getDataCategory(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.listCategory = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.categoryService.getById(id).subscribe((res: any) => {
      this._id = res.data.id;
      this.form.setValue({
        NameCategory: res.data.nameCategory,
        Description: res.data.description
      });
      this.loading = false;
    });
    this.loading = false;
    this.listCategory = false;
    this.formCre = false;
    this.formEdit = true;
  }
  OnUpdate() {
    this.loading = true;
    this.categoryService.edit(this._id, this.form.value).subscribe(
      () => {},
      error => {
        this.loading = false;
        this.toastrService.error("Update false.", "False");
      },
      () => {
        this.loading = false;
        this.toastrService.success("Update success.", "Success");
        this.getDataCategory(this.page, this.pageSize);
        this.formCre = false;
        this.listCategory = true;
        this.listTopic = false;
        this.formEdit = false;
      }
    );
  }
  OnDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.categoryService.delete(id).subscribe(
        () => {
          this.toastrService.success("Xóa giai đoạn thành công", "Thành công");
          this.getDataCategory(this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.listCategory = true;
          this.listTopic = false;
        },
        err => {
          this.toastrService.error("Xóa giai đoạn thất bại", "Thất bại");
          this.getDataCategory(this.page, this.pageSize);
          this.loading = false;
        }
      );
    }
  }
  gotoInfo(id: number) {
    this.loading = true;
    this.idCategory = id;
    this.categoryService.getById(id).subscribe((data: any) => {
      this.nameCategory = data.data.nameCategory;
    });
    this.getDataTopic(id);
    this.listTopic = true;
    this.listCategory = false;
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
            this.form.value.TopicId = this.idSelect;
            this.form.value.CategoryId = this.idCategory;
            const temp = this.form.value;
            this.categoryService
              .createCategory(temp)
              .subscribe((data: any) => {
                console.log(data);
                if (data.success === false) {
                  this.toastrService.error(
                    "Đề tài đã tồn tại trong thể loại " + this.nameCategory,
                    "Thất bại"
                  );
                  this.getDataTopic(this.idCategory);
                } else {
                  this.toastrService.success(
                    "Đã thêm đề tài vào thể loại " + this.nameCategory,
                    "Thành công"
                  );
                  this.getDataTopic(this.idCategory);
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
      this.categoryService.deleteCategory(id).subscribe(
        res => {
          console.log(res);

          this.toastrService.success(
            "Đã xóa đề tài khỏi thể loại " + this.nameCategory,
            "Thành công"
          );
          this.getDataTopic(this.idCategory);
          this.loading = false;
          this.formCre = false;
          this.listCategory = false;
          this.listTopic = true;
        },
        err => {
          this.toastrService.error("Xóa đề tài thất bại", "Thất bại");
          this.loading = false;
          this.getDataTopic(this.idCategory);
        }
      );
    }
  }
  backtoListCategory() {
    this.formCre = false;
    this.listCategory = true;
    this.listTopic = false;
    this.formEdit = false;
  }
  listTopicBack() {
    this.getDataCategory(this.page, this.pageSize);
    this.formCre = false;
    this.listCategory = true;
    this.listTopic = false;
    this.formEdit = false;
  }
}
