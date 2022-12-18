import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {ReviewFormComponent} from "./review-form.component";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import {TagInputModule} from 'ngx-chips';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NgxDropzoneModule} from "ngx-dropzone";

@NgModule({
  imports: [
    TagInputModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    NgForOf,
    NgIf,
    NgxDropzoneModule
  ],
  exports: [ReviewFormComponent],
  declarations: [
    ReviewFormComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]})

export class ReviewFormModule {

}
