import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class CategoryService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/category/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/category/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/category/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/category/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/category/getbyid?id=" + id);
  }
  getTopicByCategoryId(id: number) {
    return this.http.get(
      this.url + "/api/category/GetTopicByCategoryId?id=" + id
    );
  }
  createCategory(form: Object) {
    return this.http.post(this.url + "/api/category/created", form);
  }
  deleteCategory(id: any) {
    return this.http.delete(this.url + "/api/category/delete?id=" + id);
  }
  search(textsearch: string) {
    return this.http.get(
      this.url + "/api/category/search?searchstring=" + textsearch
    );
  }
}
