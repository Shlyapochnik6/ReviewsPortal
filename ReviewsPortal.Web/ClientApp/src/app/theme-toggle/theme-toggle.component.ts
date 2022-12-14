import { Component, OnInit } from '@angular/core';
import { ThemeService } from "../../common/services/theme/theme.service";

@Component({
  selector: 'app-theme-toggle',
  templateUrl: './theme-toggle.component.html',
  styleUrls: ['./theme-toggle.component.css']
})
export class ThemeToggleComponent implements OnInit {
  currentTheme = '';
  theme: string = 'bootstrap-dark';

  constructor(private themeService: ThemeService) {
    this.getCurrentTheme();
  }

  ngOnInit(): void {
  }

  toggleTheme() {
    if (this.theme === 'bootstrap') {
      this.theme = 'bootstrap-dark';
      localStorage.setItem('theme', 'dark');
    } else {
      this.theme = 'bootstrap';
      localStorage.setItem('theme', 'light');
    }
    this.themeService.setTheme(this.theme)
  }

  getCurrentTheme() {
    this.currentTheme = localStorage.getItem('theme')!;
    if (this.currentTheme !== undefined) {
      switch (this.currentTheme) {
        case 'light':
          this.theme = 'bootstrap';
          break;
        case 'dark':
          this.theme = 'bootstrap-dark';
          break;
      }
    }
    else {
      console.log('Theme is undefined')
    }
  }
}
