import { Component, OnInit, Renderer2 } from '@angular/core';
import { ThemeService } from "../common/services/theme/theme.service";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../environments/environment";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private themeService: ThemeService, private renderer: Renderer2,
              private translateService: TranslateService) {}

  ngOnInit() : void {
    this.themeService.themeChanges().subscribe(theme => {
      if (theme.oldValue) {
        this.renderer.removeClass(document.body, theme.oldValue);
      }
      this.renderer.addClass(document.body, theme.newValue!);
    })
    this.translateService.use(environment.defaultLocale);
  }
}
