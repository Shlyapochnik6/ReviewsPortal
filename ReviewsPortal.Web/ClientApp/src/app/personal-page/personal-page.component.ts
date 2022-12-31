import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {ColumnMode} from "@swimlane/ngx-datatable";
import {AllUserReviewsModel} from "../../common/models/AllUserReviewsModel";
import {FormControl, FormGroup} from "@angular/forms";
import {filterBy} from "@progress/kendo-data-query";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-personal-page',
  templateUrl: 'personal-page.component.html',
  styleUrls: ['personal-page.component.css']
})

export class PersonalPageComponent implements OnInit {

  reviewsRecords!: AllUserReviewsModel[];
  ColumnMode = ColumnMode;
  reviews!: AllUserReviewsModel[];

  constructor(private http: HttpClient,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.getAllReviews();
  }

  getAllReviews() {
    this.http.get<AllUserReviewsModel[]>(`api/reviews/get-by-user`)
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
    let filtrationField: string = this.filtrationForm.get('filtrationField')?.value;
    console.log(filtrationField)
    let filtrationValue: string = this.filtrationForm.get('filtrationValue')?.value;
    console.log(filtrationValue)
    this.reviewsRecords = filterBy(sortReviews, {
      logic: 'and',
      filters: [
        {field: filtrationField, value: filtrationValue,
          operator: 'contains', ignoreCase: true}
      ]
    })
  }
}
