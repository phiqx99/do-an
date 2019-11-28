import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { PagedResult } from "../../../shared/models/page-result";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { TopicService } from "../../../shared/service/topic.service";
import { CouncilService } from "../../../shared/service/council.service";
import { UserService } from "../../../shared/service/userservice.service";
import { CategoryService } from "../../../shared/service/category.service";

@Component({
  selector: "app-topic",
  templateUrl: "./topic.component.html",
  styleUrls: ["./topic.component.scss"]
})
export class TopicComponent implements OnInit {
  image: any;
  nameFile: any;
  Base64File: any;
  listSearch: [];
  listSearchUser: [];
  listSearchSchool: [];
  listSearchCategory: [];
  idSelect: any;
  idUserSelect: any;
  idSchoolSelect: any;
  idCategorySelect: any;
  valueSearch: any;

  pageSize = 10;
  skip = 0;
  page = 1;
  gridView: GridDataResult;
  pagedResult: PagedResult<any>;

  form: FormGroup;
  _id: "";

  itemsPreriod: any[];
  itemsEdit: any[];
  nameTopic: any;
  idTopic: any;

  formCre = false;
  formEdit = false;
  listTopic = true;
  listCouncil = false;
  btnAdd = false;
  userErr = false;
  schoolErr = false;
  categoryErr = false;
  loading = false;
  disableAdd = true;

  constructor(
    private topicService: TopicService,
    private councilService: CouncilService,
    private userService: UserService,
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.form = this.fb.group({
      NameTopic: ["", Validators.required],
      CategoryId: ["", Validators.required],
      UserId: ["", Validators.required],
      SchoolId: ["", Validators.required],
      NameFile: [""],
      Base64File: [""],
      Description: ["", Validators.required]
    });
    this.getDataTopic(this.page, this.pageSize);
  }
  getDataTopic(page: number, pageSize: number) {
    this.topicService.getAll(page, pageSize).subscribe((res: any) => {
      console.log(res);

      this.itemsPreriod = res.data.items;
      this.pagedResult = res.data;
      this.pagedResult.pageNumber = res.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getDataCouncilAll(id: number) {
    this.topicService.getCouncilByTopicId(id).subscribe((data: any) => {
      this.disableAdd = data.data.items.length > 0 === false;
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
    this.getDataTopic(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getDataTopic(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  gotoCre() {
    this.form.reset();
    this.listTopic = false;
    this.formCre = true;
  }
  changeListener($event): void {
    this.nameFile = $event.target.value;
    this.next($event.target);
  }
  next(inputValue: any): void {
    const file: File = inputValue.files[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onload = () => {
      this.Base64File = myReader.result;
      console.log(this.Base64File);
    };
  }
  OnCreate() {
    this.loading = true;
    this.form.patchValue({
      Base64File: this.Base64File,
      UserId: this.idUserSelect,
      SchoolId: this.idSchoolSelect,
      CategoryId: this.idCategorySelect
    });
    console.log(this.form.value);
    this.topicService.create(this.form.value).subscribe((res: any) => {
      if (res.errorCode !== 0) {
        console.log(res);

        this.toastrService.error(res.message, "Thất bại");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm đề tài thành công.", "Thành công");
        this.getDataTopic(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.listTopic = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.form.reset();
    this.topicService.getEditById(id).subscribe((res: any) => {
      console.log(res);
      this._id = res.data.id;
      const data = res.data[0];
      this.form.setValue({
        Base64File: data.base64File,
        NameTopic: data.nameTopic,
        CategoryId: data.nameCategory,
        UserId: data.nameUser,
        SchoolId: data.nameSchool,
        NameFile: data.nameFile,
        Description: data.description
      });
      this.Base64File = data.base64File;
      this.nameFile = data.nameFile;
      this.loading = false;
    });
    this.loading = false;
    this.listTopic = false;
    this.formCre = false;
    this.formEdit = true;
  }
  downloadFile() {
    // window.location.href = this.Base64File;
    window.location.href = "https://localhost:44346/files/" + this.nameFile;
  }
  OnUpdate() {
    this.loading = true;
    console.log(this.form.value);

    this.topicService.edit(this._id, this.form.value).subscribe(
      (res: any) => {},
      error => {
        this.loading = false;
        this.toastrService.error("Cập nhật đề tài thất bại", "Thất bại");
      },
      () => {
        this.loading = false;
        this.toastrService.success("Cập nhật đề tài thành công", "Thành công");
        this.getDataTopic(this.page, this.pageSize);
        this.formCre = false;
        this.listTopic = true;
        this.listCouncil = false;
        this.formEdit = false;
      }
    );
  }
  OnDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.topicService.delete(id).subscribe(
        res => {
          this.toastrService.success("Xóa đề tài thành công", "Thành công");
          this.getDataTopic(this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.listTopic = true;
          this.listCouncil = false;
        },
        err => {
          this.toastrService.error("Xóa đề tài thất bại", "Thất bại");
          this.getDataTopic(this.page, this.pageSize);
          this.loading = false;
        }
      );
    }
  }
  gotoInfo(id: number) {
    this.loading = true;
    this.idTopic = id;
    this.topicService.getById(id).subscribe((data: any) => {
      this.nameTopic = data.data.nameTopic;
    });
    this.getDataCouncilAll(id);
    this.listCouncil = true;
    this.listTopic = false;
    this.loading = false;
  }
  searchChange(e: any) {
    this.valueSearch = e.target.value;
    if (this.valueSearch !== "") {
      this.btnAdd = true;
      this.councilService.search(this.valueSearch).subscribe((res: any) => {
        this.listSearch = res.data;
        console.log(this.listSearch);

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
  searchUser(e: any) {
    const value = e.target.value;
    if (value !== "") {
      this.userErr = false;
      this.userService.search(value).subscribe((res: any) => {
        this.listSearchUser = res.data;
        if (this.listSearchUser.length > 0) {
          this.idUserSelect = res.data[0].id;
          console.log(this.idUserSelect);
          this.userErr = false;
        } else {
          this.idUserSelect = null;
          this.userErr = true;
        }
      });
    }
  }
  searchSchool(e: any) {
    const value = e.target.value;
    if (value !== "") {
      this.schoolErr = false;
      this.topicService.searchSchool(value).subscribe((res: any) => {
        this.listSearchSchool = res.data;
        console.log(this.listSearchSchool);
        this.schoolErr = false;

        if (this.listSearchSchool.length > 0) {
          this.idSchoolSelect = res.data[0].id;
          console.log(this.idSchoolSelect);
        } else {
          this.idSchoolSelect = null;
          this.schoolErr = true;
        }
      });
    }
  }
  searchCategory(e: any) {
    const value = e.target.value;
    if (value !== "") {
      this.categoryErr = false;
      this.categoryService.search(value).subscribe((res: any) => {
        this.listSearchCategory = res.data;
        console.log(this.listSearchCategory);
        this.categoryErr = false;

        if (this.listSearchCategory.length > 0) {
          this.idCategorySelect = res.data[0].id;
          console.log(this.idCategorySelect);
        } else {
          this.idCategorySelect = null;
          this.categoryErr = true;
        }
      });
    }
  }
  OnAddCouncil() {
    console.log(this.valueSearch);

    this.councilService.search(this.valueSearch).subscribe((res: any) => {
      console.log(res);

      if (res.data.length > 0) {
        if (res.data[0].nameCouncil === this.valueSearch) {
          if (this.idSelect !== null) {
            this.form.value.CouncilId = this.idSelect;
            this.form.value.TopicId = this.idTopic;
            const temp = this.form.value;
            console.log(temp);

            this.topicService
              .createTopicCouncil(temp)
              .subscribe((data: any) => {
                if (data.success === false) {
                  this.toastrService.error("Hội đồng đã tồn tại", "Thất bại");
                  this.getDataCouncilAll(this.idTopic);
                } else {
                  this.toastrService.success(
                    "Thêm hội đồng thành công",
                    "Thành công"
                  );
                  this.getDataCouncilAll(this.idTopic);
                }
              });
            this.valueSearch = "";
          }
        }
      } else {
        this.toastrService.error("Không tìm thấy hội đồng", "Thất bại");
      }
    });
  }
  OnDelCouncil(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.topicService.deleteTopicCouncil(id).subscribe(
        res => {
          this.toastrService.success(
            "Xóa hội đồng khỏi " + this.nameTopic + " thành công",
            "Thành công"
          );
          this.getDataCouncilAll(this.idTopic);
          this.loading = false;
          this.formCre = false;
          this.listTopic = false;
          this.listCouncil = true;
        },
        err => {
          this.toastrService.error(
            "Xóa hội đồng khỏi " + this.nameTopic + " thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getDataCouncilAll(this.idTopic);
        }
      );
    }
  }
  backtoListTopic() {
    this.formCre = false;
    this.listTopic = true;
    this.listCouncil = false;
    this.formEdit = false;
  }
  listCouncilBack() {
    this.getDataTopic(this.page, this.pageSize);
    this.formCre = false;
    this.listTopic = true;
    this.listCouncil = false;
    this.formEdit = false;
    this.disableAdd = true;
  }
}
