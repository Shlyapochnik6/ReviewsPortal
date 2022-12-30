import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {UpdatedReviewModel} from "../../common/models/UpdatedReviewModel";
import {ReviewFormModel} from "../../common/models/review-form-model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {dataToForm} from "../../common/functions/dataToForm";

@Component({
  selector: 'app-update-review',
  templateUrl: 'update-review.component.html',
  styleUrls: ['update-review.component.css']
})

export class UpdateReviewComponent implements OnInit{

  reviewForm!: ReviewFormModel;
  review!: UpdatedReviewModel;
  reviewId: number = 0;
  promise!: Promise<boolean>;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  async ngOnInit() {
    this.reviewId = this.activatedRoute.snapshot.params['id'];
    await this.getReviewForm();
  }

  async getReviewForm(){
    this.http.get<UpdatedReviewModel>(`api/reviews/get-updated/${this.reviewId}`)
      .subscribe({
        next: data => {
          this.review = data
          this.reviewForm = new FormGroup({
            reviewId: new FormControl(this.reviewId),
            title: new FormControl(data.title, [
              Validators.required,
              Validators.maxLength(100)
            ]),
            artName: new FormControl(data.artName, [
              Validators.required,
              Validators.maxLength(100)
            ]),
            categoryName: new FormControl(data.categoryName, [
              Validators.required
            ]),
            tags: new FormControl(data.tags, [
              Validators.required
            ]),
            description: new FormControl(data.description, [
              Validators.required,
              Validators.maxLength(15000)
            ]),
            grade: new FormControl(data.grade),
            imageUrl: new FormControl(new File([], ''))
          })
          this.promise = Promise.resolve(true)
        }
      })
  }

  onSubmit() {
    this.http.put('api/reviews', dataToForm(this.reviewForm.value))
      .subscribe({
        next: _ => this.router.navigate(['/']),
        error: err => {
          console.log(err)
        }
      })
  }
}

