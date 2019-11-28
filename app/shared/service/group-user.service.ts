import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GroupUser } from '../../models/group-user.model';

@Injectable({
  providedIn: 'root'
})
export class GroupUserService {
readonly rootURL = 'https://localhost:44346';
constructor(private fb: FormBuilder, private http: HttpClient) { }
  // formModel = this.fb.group({
  //   Id: [''],
  //   Name: ['', Validators.required],
  //   Description: ['', Validators.required],
  //   Active: ['', Validators.required],
  //   CreatedAt: ['', Validators.required],
  //   UpdateAt: ['', Validators.required],
  //   CreatedUser: ['', Validators.required],
  //   UpdateUser: ['', Validators.required],
  // });

  create(group: Object): Observable<GroupUser> {
    return this.http.post<GroupUser>(this.rootURL + '/api/groupUser/created', group);
  }
  edit(id: any, group: Object): Observable<any> {
    return this.http.put(this.rootURL + '/api/groupUser/update?id=' + id, group);
  }
  delete(id: any): Observable<GroupUser> {
    return this.http.delete<GroupUser>(this.rootURL + '/api/groupUser/delete?id=' + id);
  }
  getall(page: number, pageSize: number) {
    return this.http.get(this.rootURL + '/api/groupUser/getall?page=' + page + '&pageSize=' + pageSize);
  }
  getbyid(id: number) {
    return this.http.get(this.rootURL + '/api/groupUser/getbyid?id=' + id);
  }
}
