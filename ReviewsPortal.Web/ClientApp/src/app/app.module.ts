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
import {AuthGuard} from "../common/guards/auth.guard";
import {AuthErrorsInterceptor} from "../common/interceptors/auth.errors.interceptor";
import {AdminComponent} from "./admin/admin.component";
import {RoleGuard} from "../common/guards/role.guard";
import {SearchComponent} from "./search/search.component";
import {LogoutComponent} from "./logout/logout.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ReviewComponent,
    RegistrationComponent,
    LoginComponent,
    LoginCallbackComponent,
    LogoutComponent,
    ExternalLoginComponent,
    CreateReviewComponent,
    UpdateReviewComponent,
    PersonalPageComponent,
    SearchComponent,
    CounterComponent,
    FetchDataComponent,
    ThemeToggleComponent,
    LanguageDropdownComponent,
    AdminComponent
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
      {path: 'logout', component: LogoutComponent},
      {path: 'create-review', component: CreateReviewComponent, canActivate: [AuthGuard]},
      {path: 'create-review/:userid', component: CreateReviewComponent, canActivate: [AuthGuard, RoleGuard]},
      {path: 'external-login-callback', component: LoginCallbackComponent},
      {path: 'review/:id', component: ReviewComponent},
      {path: 'personal-page', component: PersonalPageComponent, canActivate: [AuthGuard]},
      {path: 'personal-page/:userid', component: PersonalPageComponent, canActivate: [AuthGuard, RoleGuard]},
      {path: 'update-review/:id', component: UpdateReviewComponent, canActivate: [AuthGuard]},
      {path: 'admin-profile', component: AdminComponent, canActivate: [AuthGuard, RoleGuard]},
      {path: 'search', component: SearchComponent}
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
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthErrorsInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule {

}
