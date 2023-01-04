import { FormControl, FormGroup } from "@angular/forms";

export interface ReviewFormModel extends FormGroup {
  controls: {
    title: FormControl,
    artName: FormControl,
    categoryName: FormControl,
    tags: FormControl,
    description: FormControl,
    grade: FormControl,
    images: FormControl
  }
}
