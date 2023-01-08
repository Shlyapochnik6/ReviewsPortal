import {Component, OnInit} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {AuthService} from "../../common/services/user/auth.service";
import {UserModel} from "../../common/models/UserModel";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-admin',
  templateUrl: 'admin.component.html',
  styleUrls: ['admin.component.css']
})

export class AdminComponent implements OnInit {

  users!: UserModel[];
  loader: boolean = false;

  constructor(private http: HttpClient,
              private router: Router,
              private userService: AuthService) {
  }

  ngOnInit(): void {
    this.getAllUsers();
  }

  async getAllUsers() {
    this.users = await firstValueFrom(this.userService.getAllUsers());
    this.loader = true;
  }

  deleteUser(userId: number) {
    this.userService.deleteUser(userId);
    this.users = this.users.filter(user => user.id !== userId);
    this.loader = true;
  }

  async blockUser(userId: number) {
    let user = this.users.find(user => user.id === userId);
    if (user) {
      user.accessLevels = 'Blocked';
    }
    await firstValueFrom(this.userService.blockUser(userId));
    this.getAllUsers();
  }

  async unBlockUser(userId: number) {
    let user = this.users.find(user => user.id === userId);
    if (user) {
      user.accessLevels = 'Unblocked';
    }
    await firstValueFrom(this.userService.unBlockUser(userId));
    this.getAllUsers();
  }
}
