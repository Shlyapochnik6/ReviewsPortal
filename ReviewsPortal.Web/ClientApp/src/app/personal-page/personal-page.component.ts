import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ColumnMode} from "@swimlane/ngx-datatable";
import {AllUserReviewsModel} from "../../common/models/AllUserReviewsModel";
import {FormControl, FormGroup} from "@angular/forms";
import {filterBy} from "@progress/kendo-data-query";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {FiltrationService} from "../../common/services/filtration/filtration.service";

@Component({
  selector: 'app-personal-page',
  templateUrl: 'personal-page.component.html',
  styleUrls: ['personal-page.component.css']
})

export class PersonalPageComponent implements OnInit {

  reviewsRecords!: AllUserReviewsModel[];
  ColumnMode = ColumnMode;
  reviews!: AllUserReviewsModel[];
  userId?: number | null = null

  constructor(private http: HttpClient,
              private route: ActivatedRoute,
              private reviewsService: ReviewsService,
              private filtrationService: FiltrationService) {
  }

  ngOnInit() {
    this.route.params.subscribe({
      next: value => {
        this.userId = value['userid'];
      }
    })
    this.getAllReviews();
  }

  getAllReviews() {
    this.reviewsService.getReviewsByUserId(this.userId)
      .subscribe({
        next: data => {
          this.reviews = data;
          this.reviewsRecords = this.reviews;
        }
      });
  }

  onDeleteReviewRow(reviewId: number){
    this.http.delete(`api/reviews/${reviewId}`)
      .subscribe({
        complete: () => this.getAllReviews()
      })
  }

  filtrationForm = new FormGroup({
    filtrationField: new FormControl(),
    filtrationValue: new FormControl()
  })

  onFilter() {
    let sortReviews = this.reviews;
    let filtrationField: string = this.filtrationForm.get('filtrationField')?.value!;
    let filtrationValue: string = this.filtrationForm.get('filtrationValue')?.value!;
    this.reviewsRecords = this.filtrationService.filterData(filtrationField, filtrationValue, this.reviews);
  }
}
