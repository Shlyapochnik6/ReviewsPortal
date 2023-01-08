import {Component, OnInit} from "@angular/core";
import {AuthService} from "../../common/services/user/auth.service";

@Component({
  selector: 'app-logout',
  templateUrl: 'logout.component.html',
  styleUrls: ['logout.component.css']
})

export class LogoutComponent implements OnInit {

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
    this.logout();
  }

  logout() {
    this.authService.isAuthenticated = false;
    this.authService.logout();
  }
}
