import {Component, OnInit} from "@angular/core";
import '@github/markdown-toolbar-element'
import {TranslateService} from "@ngx-translate/core";
import {environment} from "../../environments/environment";

@Component({
  selector: 'app-language-dropdown',
  templateUrl: 'language-dropdown.component.html',
  styleUrls: ['language-dropdown.component.css']
})

export class LanguageDropdownComponent implements OnInit{

  constructor(private translateService: TranslateService) {
  }

  ngOnInit() : void {
    let language: string | null = this.getStorageLanguage();
    if (language == null) {
      this.translateService.use(environment.defaultLocale);
    } else {
      this.changeCurrentLanguage(language);
    }
  }

  changeCurrentLanguage(lang: string) {
    this.translateService.use(lang);
    this.saveLanguageInStorage(lang);
  }

  getStorageLanguage() : string | null {
    return localStorage.getItem('language');
  }

  saveLanguageInStorage(lang: string) {
    localStorage.setItem('language', lang);
  }
}
