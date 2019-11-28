import { Injectable } from "@angular/core";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Role } from "../../models/role.model";

@Injectable({
  providedIn: "root"
})
export class RoleService {
  readonly rootURL = "https://localhost:44346";
  constructor(private http: HttpClient) {}

  create(role: Object): Observable<Role> {
    return this.http.post<Role>(this.rootURL + "/api/role/created", role);
  }
  edit(id: any, role: Object) {
    return this.http.put(this.rootURL + "/api/role/update?id=" + id, role);
  }
  delete(id: any): Observable<Role> {
    return this.http.delete<Role>(this.rootURL + "/api/role/delete?id=" + id);
  }
  getall(page: number, pageSize: number) {
    return this.http.get(
      this.rootURL + "/api/role/getall?page=" + page + "&pageSize=" + pageSize
    );
  }
  getbyid(id: number) {
    return this.http.get(this.rootURL + "/api/role/getbyid?id=" + id);
  }
}
