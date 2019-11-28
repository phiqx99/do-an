export class PagedResult<T> {
    pageNo: number | number = 0;
    pageSize: number | number = 10;
    total: number;
    totalCount: number;
    pageNumber: number;
    items: [];
}
