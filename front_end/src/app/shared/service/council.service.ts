import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class CouncilService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/council/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/council/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/council/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/council/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/council/getbyid?id=" + id);
  }
  getUserByCouncilId(id: number) {
    return this.http.get(this.url + "/api/groupuser/GetByCouncilId?id=" + id);
  }
  search(textsearch: string) {
    return this.http.get(
      this.url + "/api/council/search?searchstring=" + textsearch
    );
  }
}
