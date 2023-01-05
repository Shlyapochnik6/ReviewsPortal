import {Component, OnInit} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AuthService} from "../../common/services/user/auth.service";
import {UserModel} from "../../common/models/UserModel";

@Component({
  selector: 'app-admin',
  templateUrl: 'admin.component.html',
  styleUrls: ['admin.component.css']
})

export class AdminComponent implements OnInit {

  users!: UserModel[];

  constructor(private http: HttpClient,
              private router: Router,
              private userService: AuthService) {
  }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers() {
    this.http.get<UserModel[]>(`api/user/get-all`)
      .subscribe({
        next: value => {
          this.users = value;
        }
      })
  }
}
