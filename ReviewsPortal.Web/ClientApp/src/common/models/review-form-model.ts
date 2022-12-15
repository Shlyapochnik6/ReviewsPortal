import { FormControl, FormGroup } from "@angular/forms";

export interface ReviewFormModel extends FormGroup {
  controls: {
    title: FormControl,
    artName: FormControl,
    category: FormControl,
    tags: FormControl,
    description: FormControl,
    grade: FormControl
  }
}
