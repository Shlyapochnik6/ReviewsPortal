import {Component, OnInit, Input, HostBinding} from "@angular/core";
import {FormControl} from "@angular/forms";
import '@github/markdown-toolbar-element'

@Component({
  selector: 'app-markdown-editor',
  templateUrl: './markdown-editor.component.html',
  styleUrls: ['markdown-editor.component.css']
})

export class MarkdownEditorComponent implements OnInit {

  @HostBinding('class.focus') isFocus?: boolean;
  @Input() control: FormControl = new FormControl();

  constructor() {
  }

  focus() {
    this.isFocus = true;
  }

  blur() {
    this.isFocus = false;
  }

  onChange() {

  }

  ngOnInit() : void {
    this.control = this.control ?? new FormControl();
  }
}
