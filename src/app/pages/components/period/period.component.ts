import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { PagedResult } from "../../../shared/models/page-result";
import { GridDataResult, PageChangeEvent } from "@progress/kendo-angular-grid";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { PeriodService } from "../../../shared/service/period.service";
import { TopicService } from "../../../shared/service/topic.service";

@Component({
  selector: "app-period",
  templateUrl: "./period.component.html",
  styleUrls: ["./period.component.scss"]
})
export class PeriodComponent implements OnInit {
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

  itemsPeriod: any[];
  itemsEdit: any[];
  namePeriod: any;
  idPeriod: any;

  formCre = false;
  formEdit = false;
  listPeriod = true;
  listTopic = false;
  btnAdd = false;
  loading = false;

  constructor(
    private periodService: PeriodService,
    private topicService: TopicService,
    private fb: FormBuilder,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.form = this.fb.group({
      Caption: ["", Validators.required],
      StartDay: ["", Validators.required],
      EndDay: ["", Validators.required],
      Description: [""]
    });
    this.getDataPeriod(this.page, this.pageSize);
  }
  getDataPeriod(page: number, pageSize: number) {
    this.periodService.getAll(page, pageSize).subscribe((res: any) => {
      this.itemsPeriod = res.data.items;
      this.pagedResult = res.data;
      this.pagedResult.pageNumber = res.data.total;
      this.reloadData();
      this.loading = false;
    });
  }
  getDataTopic(id: number) {
    this.periodService.getTopicByPeriodId(id).subscribe((data: any) => {
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
    this.getDataPeriod(data.pageNo, data.pageSize);
    this.pageSize = data.pageSize;
    this.reloadData();
  }
  pageSizeChange(size: number) {
    this.pageSize = size;
    this.pagedResult.pageSize = size;
    this.getDataPeriod(1, this.pageSize);
    this.reloadData();
  }
  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
  }
  gotoCre() {
    this.form.reset();
    this.listPeriod = false;
    this.formCre = true;
  }
  OnCreate() {
    this.loading = true;
    this.periodService.create(this.form.value).subscribe((res: any) => {
      if (res.errorCode !== 0) {
        this.toastrService.error(res.message, "Thất bại");
        this.loading = false;
      } else {
        this.toastrService.success("Thêm thể loại thành công.", "Thành công");
        this.getDataPeriod(this.page, this.pageSize);

        this.loading = false;
        this.formCre = false;
        this.listPeriod = true;
      }
    });
  }
  gotoEdit(id: number) {
    this.loading = true;
    this.periodService.getById(id).subscribe((res: any) => {
      this._id = res.data.id;
      this.form.setValue({
        Caption: res.data.caption,
        StartDay: res.data.startDay,
        EndDay: res.data.endDay,
        Description: res.data.description
      });
      this.loading = false;
    });
    this.loading = false;
    this.listPeriod = false;
    this.formCre = false;
    this.formEdit = true;
  }
  OnUpdate() {
    this.loading = true;
    this.periodService.edit(this._id, this.form.value).subscribe(
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
        this.getDataPeriod(this.page, this.pageSize);
        this.formCre = false;
        this.listPeriod = true;
        this.listTopic = false;
        this.formEdit = false;
      }
    );
  }
  OnDel(id: number) {
    if (confirm("Xác nhận xóa !")) {
      this.loading = true;
      this.periodService.delete(id).subscribe(
        () => {
          this.toastrService.success("Xóa giai đoạn thành công", "Thành công");
          this.getDataPeriod(this.page, this.pageSize);
          this.loading = false;
          this.formCre = false;
          this.listPeriod = true;
          this.listTopic = false;
        },
        err => {
          this.toastrService.error("Xóa giai đoạn thất bại", "Thất bại");
          this.getDataPeriod(this.page, this.pageSize);
          this.loading = false;
        }
      );
    }
  }
  gotoInfo(id: number) {
    this.loading = true;
    this.idPeriod = id;
    this.periodService.getById(id).subscribe((data: any) => {
      this.namePeriod = data.data.caption;
    });
    this.getDataTopic(id);
    this.listTopic = true;
    this.listPeriod = false;
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
            this.form.value.PeriodId = this.idPeriod;
            const temp = this.form.value;
            console.log(temp);

            this.periodService
              .updateTopic(this.idSelect, temp)
              .subscribe((data: any) => {
                if (data.success === false) {
                  this.toastrService.error(
                    "Đề tài đã tồn tại trong giai đoạn này",
                    "Thất bại"
                  );
                  this.getDataTopic(this.idPeriod);
                } else {
                  this.toastrService.success(
                    "Thêm đề tài vào " + this.namePeriod + " thành công",
                    "Thành công"
                  );
                  this.getDataTopic(this.idPeriod);
                }
              });
              this.valueSearch = "";
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
      this.form.value.PeriodId = null;
      const temp = this.form.value;
      this.periodService.updateTopic(id, temp).subscribe(
        res => {
          this.toastrService.success(
            "Xóa đề tài khỏi " + this.namePeriod + " thành công",
            "Thành công"
          );
          this.getDataTopic(this.idPeriod);
          this.loading = false;
          this.formCre = false;
          this.listPeriod = false;
          this.listTopic = true;
        },
        err => {
          this.toastrService.error(
            "Xóa đề tài khỏi " + this.namePeriod + " thất bại",
            "Thất bại"
          );
          this.loading = false;
          this.getDataTopic(this.idPeriod);
        }
      );
    }
  }
  OnPassTopic(id: number) {
    if (confirm("Xác nhận duyệt đề tài này !")) {
      this.loading = true;
      console.log("this.idPeriod", this.idPeriod);
      console.log(id);
      this.form.value.PeriodId = this.idPeriod;
      const temp = this.form.value;
      console.log(temp);
      this.periodService.passTopic(id, temp).subscribe(
        () => {
          this.loading = false;
          this.toastrService.success("Đề tài đã được duyệt", "Thành công");
          this.getDataPeriod(this.page, this.pageSize);
          this.formCre = false;
          this.listPeriod = true;
          this.listTopic = false;
          this.formEdit = false;
        },
        error => {
          this.loading = false;
          this.toastrService.error("", "Thất bại");
        }
      );
    }
  }
  backtoListPeriod() {
    this.formCre = false;
    this.listPeriod = true;
    this.listTopic = false;
    this.formEdit = false;
  }
  listTopicBack() {
    this.getDataPeriod(this.page, this.pageSize);
    this.formCre = false;
    this.listPeriod = true;
    this.listTopic = false;
    this.formEdit = false;
  }
}
