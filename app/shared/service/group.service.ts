import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Group } from "../../models/group.model";

@Injectable({
  providedIn: "root"
})
export class GroupService {
  readonly rootURL = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(group: Object): Observable<Group> {
    return this.http.post<Group>(this.rootURL + "/api/group/created", group);
  }
  edit(id: any, group: Object): Observable<any> {
    return this.http.put(this.rootURL + "/api/group/update?id=" + id, group);
  }
  delete(id: any): Observable<Group> {
    return this.http.delete<Group>(this.rootURL + "/api/group/delete?id=" + id);
  }
  getall(page: number, pageSize: number) {
    return this.http.get(
      this.rootURL + "/api/group/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getbyid(id: number) {
    return this.http.get(this.rootURL + "/api/group/getbyid?id=" + id);
  }
  getAllUser(page: number, pagesize: number) {
    return this.http.get(
      this.rootURL +
        "/api/Account/GetAll?page=" +
        page +
        "&pageSize=" +
        pagesize
    );
  }
  getuserbyid() {
    return this.http.get(this.rootURL + "/api/account/getinfor");
  }
  getallbyid(id: number, page: number, pageSize: number) {
    return this.http.get(
      this.rootURL +
        "/api/groupUser/getbygroupid?id=" +
        id +
        "&page=" +
        page +
        "&pageSize" +
        pageSize
    );
  }
  search(textsearch: string) {
    return this.http.get(
      this.rootURL + "/api/group/search?searchstring=" + textsearch
    );
  }
}
