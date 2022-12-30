import {BrowserModule} from '@angular/platform-browser';
import {APP_INITIALIZER, NgModule, SecurityContext} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {ReviewFormModule} from "./review-form/review-form.module";

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {ReviewComponent} from "./review/review.component";
import {RegistrationComponent} from "./registration/registration.component";
import {LoginComponent} from "./login/login.component";
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LoginCallbackComponent} from "./login-callback/login-callback.component";
import {ExternalLoginComponent} from "./external-login/external-login.component";
import {ThemeToggleComponent} from './theme-toggle/theme-toggle.component';
import {CreateReviewComponent} from "./create-review/create-review.component";
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {NgxDropzoneModule} from 'ngx-dropzone';
import {TagInputModule} from "ngx-chips";
import {MarkdownModule, MarkedOptions} from 'ngx-markdown';
import {MarkdownEditorModule} from "./markdown-editor/markdown-editor.module";
import {TranslateModule, TranslateLoader} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {HttpLoaderFactory} from "../common/functions/httpLoaderFactory";
import {LanguageDropdownComponent} from "./language-dropdown/language-dropdown.component";
import {UpdateReviewComponent} from "./update-review/update-review.component";
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import {PersonalPageComponent} from "./personal-page/personal-page.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ReviewComponent,
    RegistrationComponent,
    LoginComponent,
    LoginCallbackComponent,
    ExternalLoginComponent,
    CreateReviewComponent,
    UpdateReviewComponent,
    PersonalPageComponent,
    CounterComponent,
    FetchDataComponent,
    ThemeToggleComponent,
    LanguageDropdownComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    NgxDropzoneModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'counter', component: CounterComponent},
      {path: 'fetch-data', component: FetchDataComponent},
      {path: 'registration', component: RegistrationComponent},
      {path: 'login', component: LoginComponent},
      {path: 'create-review', component: CreateReviewComponent},
      {path: 'external-login-callback', component: LoginCallbackComponent},
      {path: 'review/:id', component: ReviewComponent},
      {path: 'personal-page', component: PersonalPageComponent},
      {path: 'update-review/:id', component: UpdateReviewComponent}
    ]),
    ReactiveFormsModule,
    ReviewFormModule,
    NgbModule,
    TagInputModule,
    NgxDatatableModule,
    MarkdownEditorModule,
    MarkdownModule.forRoot(({
      markedOptions: {
        provide: MarkedOptions,
        useValue: {
          gfm: true,
          breaks: true,
          pedantic: false,
          smartLists: true,
          smartypants: true,
        },
      },
      sanitize: SecurityContext.NONE
    })),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      useDefaultLang: false
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {

}
