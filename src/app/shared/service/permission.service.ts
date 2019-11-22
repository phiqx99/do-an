import { Injectable } from "@angular/core";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Permission } from "../../models/permission.model";

@Injectable({
  providedIn: "root"
})
export class PermissionService {
  readonly rootURL = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(permission: Object): Observable<Permission> {
    return this.http.post<Permission>(
      this.rootURL + "/api/permission/created",
      permission
    );
  }
  edit(id: any, permission: Object): Observable<any> {
    return this.http.put(
      this.rootURL + "/api/permission/update?id=" + id,
      permission
    );
  }
  delete(id: any): Observable<Permission> {
    return this.http.delete<Permission>(
      this.rootURL + "/api/permission/delete?id=" + id
    );
  }
  getall(page: number, pageSize: number) {
    return this.http.get(
      this.rootURL +
        "/api/permission/getall?page=" +
        page +
        "&pageSize=" +
        pageSize
    );
  }
  getbyid(id: number) {
    return this.http.get(this.rootURL + "/api/permission/getbyid?id=" + id);
  }
  getRole() {
    return this.http.get(this.rootURL + "/api/role/getall");
  }
  getGroup() {
    return this.http.get(this.rootURL + "/api/group/getall");
  }
  getallbyid(id: number) {
    return this.http.get(this.rootURL + "/api/permission/getbyperid?id=" + id);
  }
}
