import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class TopicService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/topicall/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/topicall/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/topicall/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/topicall/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/topicall/getbyid?id=" + id);
  }
  getCouncilByTopicId(id: number) {
    return this.http.get(
      this.url + "/api/topiccouncil/GetCouncilByTopicId?id=" + id
    );
  }
  createTopicCouncil(form: object) {
    return this.http.post(this.url + "/api/topiccouncil/created", form);
  }
  deleteTopicCouncil(id: number) {
    return this.http.delete(this.url + "/api/topiccouncil/delete?id=" + id);
  }
  search(textsearch: string) {
    return this.http.get(
      this.url + "/api/topicall/search?searchstring=" + textsearch
    );
  }
  searchSchool(textsearch: string) {
    return this.http.get(
      this.url + "/api/school/search?searchstring=" + textsearch
    );
  }
  getEditById(id: number) {
    return this.http.get(this.url + "/api/topicall/geteditbyid?id=" + id);
  }
}
