import {CommonModule} from "@angular/common";
import {ReactiveFormsModule} from "@angular/forms";
import {MarkdownEditorComponent} from "./markdown-editor.component";
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from "@angular/core";
import {MarkdownModule} from "ngx-markdown";

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, MarkdownModule],
  exports: [MarkdownEditorComponent],
  declarations: [MarkdownEditorComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class MarkdownEditorModule {

}
