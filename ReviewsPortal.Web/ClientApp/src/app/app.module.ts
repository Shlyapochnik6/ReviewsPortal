import {BrowserModule} from '@angular/platform-browser';
import {NgModule, SecurityContext} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {ReviewFormModule} from "./review-form/review-form.module";

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegistrationComponent,
    LoginComponent,
    LoginCallbackComponent,
    ExternalLoginComponent,
    CreateReviewComponent,
    CounterComponent,
    FetchDataComponent,
    ThemeToggleComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    NgxDropzoneModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: CreateReviewComponent, pathMatch: 'full'},
      {path: 'counter', component: CounterComponent},
      {path: 'fetch-data', component: FetchDataComponent},
      {path: 'registration', component: RegistrationComponent},
      {path: 'login', component: LoginComponent},
      {path: 'create-review', component: CreateReviewComponent},
      {path: 'external-login-callback', component: LoginCallbackComponent}
    ]),
    ReactiveFormsModule,
    ReviewFormModule,
    NgbModule,
    TagInputModule,
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
    }))
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {

}
