import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class TopicService {
  readonly url = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(form: Object) {
    return this.http.post(this.url + "/api/topic/created", form);
  }
  edit(id: any, form: Object) {
    return this.http.put(this.url + "/api/topic/update?id=" + id, form);
  }
  delete(id: any) {
    return this.http.delete(this.url + "/api/topic/delete?id=" + id);
  }
  getAll(page: number, pageSize: number) {
    return this.http.get(
      this.url + "/api/topic/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "/api/topic/getbyid?id=" + id);
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
      this.url + "/api/topic/search?searchstring=" + textsearch
    );
  }
  searchSchool(textsearch: string) {
    return this.http.get(
      this.url + "/api/school/search?searchstring=" + textsearch
    );
  }
  getEditById(id: number) {
    return this.http.get(this.url + "/api/topic/geteditbyid?id=" + id);
  }
}
