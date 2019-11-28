import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class UserService {
  constructor(private http: HttpClient) {}
  readonly url = "https://localhost:44346/api/account/";

  login(formData: any) {
    return this.http.post(this.url + "login", formData);
  }
  getUserProfile() {
    return this.http.get(this.url + "getinfor");
  }
  edit(id: any, user: Object) {
    return this.http.put(this.url + "Update?id=" + id, user);
  }
  delete(id: number) {
    return this.http.delete(this.url + "Delete?id=" + id);
  }
  create(user: Object) {
    return this.http.post(this.url + "Register", user);
  }
  getAll(page: number, pagesize: number) {
    return this.http.get(
      this.url + "GetAll?page=" + page + "&pageSize=" + pagesize
    );
  }
  getById(id: number) {
    return this.http.get(this.url + "GetById?id=" + id);
  }
  search(textsearch: string) {
    return this.http.get(this.url + "search?searchstring=" + textsearch);
  }
  checkvalid(textsearch: string) {
    return this.http.get(this.url + "check?check=" + textsearch);
  }
  isLoggedIn() {
    return localStorage.getItem("userToken") != null;
  }
}
