import { Injectable } from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {map, Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  isAuthenticated: boolean = false;
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
}
