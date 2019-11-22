import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class CategoryService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/theme/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/theme/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/theme/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/theme/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/theme/getbyid?id=" + id);
  }
  getTopicByCategoryId(id: number) {
    return this.http.get(
      this.url + "/api/themeTopic/GetTopicByCateId?id=" + id
    );
  }
  createThemeTopic(form: Object) {
    return this.http.post(this.url + "/api/themetopic/created", form);
  }
  deleteThemeTopic(id: any) {
    return this.http.delete(this.url + "/api/themetopic/delete?id=" + id);
  }
  search(textsearch: string) {
    return this.http.get(
      this.url + "/api/theme/search?searchstring=" + textsearch
    );
  }
}
