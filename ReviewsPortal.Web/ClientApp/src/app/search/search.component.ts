import {Component, OnInit} from "@angular/core";
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {FormControl, FormGroup} from "@angular/forms";
import {BriefReviewModel} from "../../common/models/BriefReviewModel";

@Component({
  selector: 'app-search',
  templateUrl: 'search.component.html',
  styleUrls: ['search.component.css']
})

export class SearchComponent implements OnInit {

  searchValue!: string | null;

  constructor(public reviewsService: ReviewsService) {
  }

  ngOnInit() {
    this.reviewsService.getAllReviews();
  }

  reviewForm = new FormGroup({
    'id': new FormControl(0)
  })

  searchForm = new FormGroup({
    'searchValue': new FormControl('')
  })

  getReviews() {
    this.reviewsService.getReviewsBySearch(this.searchValue)
      .subscribe({
        next: value => {
          console.log(this.searchValue);
          this.reviewsService.reviews = value;
        }
      })
  }
}
