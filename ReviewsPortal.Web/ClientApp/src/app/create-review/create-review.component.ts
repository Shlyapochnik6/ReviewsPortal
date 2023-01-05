import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import {dataToForm} from "src/common/functions/dataToForm";
import { ReviewFormModel } from "../../common/models/review-form-model";
import { ReviewsService } from "../../common/services/reviews/reviews.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['create-review.component.css']
})

export class CreateReviewComponent implements OnInit {

  userId: number | null = null;

  constructor(private http: HttpClient, private router: Router,
              private reviewsService: ReviewsService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.subscribe({
      next: value => {
        this.userId = value['userid'];
      }
    })
  }

  reviewForm : ReviewFormModel = new FormGroup({
    title : new FormControl('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    artName : new FormControl('', [
      Validators.required,
      Validators.maxLength(100)
    ]),
    images : new FormControl<File[]>(new Array<File>(), []),
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
    this.reviewsService.createReviewByUserId(this.reviewForm, this.userId)
      .subscribe({
        next: _ => this.router.navigate(['/']),
        error: err => {
          console.error(err);
        }
      })
  }
}
