import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface ThemeObject {
  oldValue?: string;
  newValue?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  initialSetting: ThemeObject = {
    oldValue: '',
    newValue: 'bootstrap'
  };
  currentTheme = '';

  themeSelection: BehaviorSubject<ThemeObject> =  new BehaviorSubject<ThemeObject>(this.initialSetting);

  constructor() {
    this.currentTheme = localStorage.getItem('theme')!
    if(this.currentTheme == 'dark'){
      this.initialSetting.newValue = 'bootstrap-dark';
    }
    else {
      this.initialSetting.newValue = 'bootstrap';
    }
  }

  setTheme(theme: string) {

    this.themeSelection.next(
      {
        oldValue: this.themeSelection.value.newValue,
        newValue: theme
      });
  }

  themeChanges(): Observable<ThemeObject> {
    return this.themeSelection.asObservable();
  }
}
