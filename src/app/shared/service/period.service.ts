import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class PeriodService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/period/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/period/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/period/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/period/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/period/getbyid?id=" + id);
  }
  passTopic(id: number, form: any) {
    return this.http.put(this.url + "/api/topicall/passTopic?id=" + id, form);
  }
  getTopicByPeriodId(id: number) {
    return this.http.get(
      this.url + "/api/topicall/GetTopicByPeriodId?id=" + id
    );
  }
  updateTopicAll(id: number, form: Object) {
    return this.http.put(this.url + "/api/topicall/update?id=" + id, form);
  }
}
