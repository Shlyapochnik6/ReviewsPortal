import { Component, OnInit } from '@angular/core';
import {ReviewsService} from "../../common/services/reviews/reviews.service";
import {TagService} from "../../common/services/tag/tag.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  tags: any;
  searchTag?: string;

  constructor(public reviewsService: ReviewsService, public tagService: TagService) {
  }

  ngOnInit() {
    this.reviewsService.getAllReviews();
    this.tagService.getAllTags().subscribe({
      next: (data) => this.tags = data
    })
  }

  async getSortedByRating() {
    await this.reviewsService.setParameters("most-rated", this.reviewsService.tag);
    this.reviewsService.getAllReviews();
  }

  async getSortedByUploadTime() {
    await this.reviewsService.setParameters("recently-added", this.reviewsService.tag);
    this.reviewsService.getAllReviews();
  }

  reviewForm = new FormGroup({
    'id': new FormControl(0)
  })

  filterTags(tags: any[], searchTag?: string) {
    return tags.filter((tag) => tag.name.includes(searchTag));
  }

  async onTagSearch(event: any) {
    let selectedTag = event.target.value;
    await this.reviewsService.setParameters(this.reviewsService.sorting, selectedTag);
    await this.reviewsService.getParameters();
    this.reviewsService.getAllReviews();
  }
}
