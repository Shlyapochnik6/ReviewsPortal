import { Component } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import {dataToForm} from "src/common/functions/dataToForm";
import { ReviewFormModel } from "../../common/models/review-form-model";

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['create-review.component.css']
})

export class CreateReviewComponent {

  constructor(private http: HttpClient, private router: Router) {}

  reviewForm : ReviewFormModel = new FormGroup({
    title : new FormControl('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    artName : new FormControl('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    imageUrl : new FormControl('', [
      Validators.required
    ]),
    description : new FormControl('', [
      Validators.required,
      Validators.maxLength(15000)
    ]),
    categoryName : new FormControl('', [
      Validators.required
    ]),
    grade : new FormControl(1),
    tags : new FormControl('', [
      Validators.required
    ])
  })

  onSubmitForm(){
    this.http.post('api/reviews', dataToForm(this.reviewForm.value))
      .subscribe({
        next: _ => this.router.navigate(['/personal-page']),
        error: err => {
          console.error(err);
        }
      })
  }
}
