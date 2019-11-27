import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class SchoolService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/school/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/school/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/school/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/school/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/school/getbyid?id=" + id);
  }
  passTopic(id: number, form: any) {
    return this.http.put(this.url + "/api/topic/passTopic?id=" + id, form);
  }
  getTopicBySchoolId(id: number) {
    return this.http.get(this.url + "/api/topic/GetTopicBySchoolId?id=" + id);
  }
  updateTopic(id: number, form: Object) {
    return this.http.put(this.url + "/api/topic/update?id=" + id, form);
  }
}
