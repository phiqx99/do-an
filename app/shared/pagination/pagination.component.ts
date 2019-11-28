import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { BehaviorSubject } from 'rxjs';
import { PagedResult } from '../models/page-result';
import { Page } from '../models/page';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit, OnChanges {
  @Input() pagedResult: PagedResult<any>;
  @Output() pagedResultChange = new EventEmitter<any>();
  @Input() defaultPageSize = 0;
  @Input() hide = true;
  @Output() pageSizeChange = new EventEmitter<number>();
  numberPre: number;
  maxSize = 9;
  pages: Page[];
  hidden = true;
  constructor() {
  }

  ngOnInit() {
  this.hidden = true;
  }

  ngOnChanges() {
    this.pages = this.createPageArray(
      this.pagedResult && +this.pagedResult.pageNo,
      this.pagedResult && +this.pagedResult.pageSize,
      this.pagedResult && +this.pagedResult.totalCount,
      this.maxSize
      );
  }

  changeDefaultSize(size: number) {
    this.pageSizeChange.emit(size);
  }

  getPage(page: number | number): void {
    this.pagedResult.total = Math.ceil(Number(this.pagedResult.totalCount));
    this.pagedResult.pageNo = page;
    this.pagedResultChange.emit(this.pagedResult);
    this.ngOnChanges();
  }

  getCurrentPage() {
    this.getPage(this.pagedResult.pageNo);
  }

  onItemsPerPageChange(pageSize: number | number) {
    this.pagedResult.pageSize = pageSize;
    this.pagedResult.pageNo = 1;
    this.pagedResultChange.emit(this.pagedResult);
  }

  private createPageArray(currentPage: number, itemsPerPage: number, totalItems: number, paginationRange: number): Page[] {
    // paginationRange could be a string if passed from attribute, so cast to number.
    paginationRange = +paginationRange;
    const pages = [];
    const totalPages = Math.ceil(totalItems / itemsPerPage);
    const halfWay = Math.ceil(paginationRange / 2);

    const isStart = currentPage <= halfWay;
    const isEnd = totalPages - halfWay < currentPage;
    const isMiddle = !isStart && !isEnd;

    const ellipsesNeeded = paginationRange < totalPages;
    let i = 1;

    while (i <= totalPages && i <= paginationRange) {
      let label;
      const pageNumber = this.calculatePageNumber(i, currentPage, paginationRange, totalPages);
      const openingEllipsesNeeded = (i === 2 && (isMiddle || isEnd));
      const closingEllipsesNeeded = (i === paginationRange - 1 && (isMiddle || isStart));
      if (ellipsesNeeded && (openingEllipsesNeeded || closingEllipsesNeeded)) {
        label = '...';
      } else {
        label = pageNumber;
      }
      pages.push({
        label: label,
        value: pageNumber
      });
      i++;
    }
    return pages;

  }

  /**
     * Given the position in the sequence of pagination links [i],
     * figure out what page number corresponds to that position.
     */
  private calculatePageNumber(i: number, currentPage: number, paginationRange: number, totalPages: number) {
    const halfWay = Math.ceil(paginationRange / 2);
    if (i === paginationRange) {
      return totalPages;
    } else if (i === 1) {
      return i;
    } else if (paginationRange < totalPages) {
      if (totalPages - halfWay < currentPage) {
        return totalPages - paginationRange + i;
      } else if (halfWay < currentPage) {
        return currentPage - halfWay + i;
      } else {
        return i;
      }
    } else {
      return i;
    }
  }

}
