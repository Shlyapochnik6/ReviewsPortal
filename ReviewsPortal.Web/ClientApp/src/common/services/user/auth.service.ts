﻿import { Injectable } from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {map, Observable} from "rxjs";
import {RoleModel} from "../../models/RoleModel";
import {Roles} from "../../consts/Roles";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  isAuthenticated: boolean = false;
  isAdminUser: boolean = false;

  constructor(private http: HttpClient,
              private router: Router) {
  }

  checkIsAuthenticated(): Observable<boolean>{
    return this.http.get<boolean>(`api/user/check-authentication`)
      .pipe(map((isAuthenticated) => {
        if (!isAuthenticated) {
          this.isAuthenticated = false;
          return false;
        }
        else {
          this.isAuthenticated = true;
          return true;
        }
    }));
  }

  checkUserRole() {
    this.getUserRole()
      .subscribe({
        next: value => {
          this.isAdminUser = value;
        }
      })
  }

  getUserRole(): Observable<boolean> {
    return this.http.get<RoleModel>(`api/user/get-role`)
      .pipe(map((r) => {
        if (r.role !== Roles.admin) {
          this.isAdminUser = false;
          return false;
        } else {
          this.isAdminUser = true;
          return true;
        }
      }))
  }
}