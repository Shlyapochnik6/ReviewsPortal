import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { DetailedReviewModel } from "../../common/models/DetailedReviewModel";
import { CommentModel } from "../../common/models/CommentModel";
import { CommentService } from "../../common/services/hub/comment.service";
import { firstValueFrom } from "rxjs";
import {ReviewsService} from "../../common/services/reviews/reviews.service";

@Component({
  selector: 'app-review',
  templateUrl: 'review.component.html',
  styleUrls: ['review.component.css']
})

export class ReviewComponent implements OnInit {

  btnClass = "btn btn-secondary";
  review!: DetailedReviewModel;
  reviewId: number = 0;
  grade: number = 0;
  loader: boolean = false;

  constructor(private activatedRoute: ActivatedRoute,
              private http: HttpClient,
              public commentService: CommentService,
              private reviewsService: ReviewsService) {
    this.getReview()
    this.getAllComments()
  }

  async ngOnInit(): Promise<void> {
    await this.commentService.startConnection(this.reviewId.toString());
    await this.commentService.getComment();
  }

  onChangeRating(rate: number) {
    this.grade = rate;
    this.http.post('api/ratings', {reviewId: this.reviewId, value: this.grade})
      .subscribe({
        error: err => {
          if (err.status === 404){
            console.log('The review was not found')
          }
        }
      })
  }
  getAllComments() {
    this.http.get(`api/comments/${this.reviewId}`)
      .subscribe({
        next: (comments: any) => {
          this.commentService.comments = comments;
        }
      });
  }

  async sendComment() {
    let comment = (<HTMLTextAreaElement>document.getElementById('commentInput'));
    this.http.post<string>('api/comments', {reviewId: this.reviewId, text: comment.value})
      .subscribe({
        next: async (commentId) => {
          await this.commentService.sendComment(this.reviewId.toString(), commentId);
          this.getAllComments();
      }
      })
    comment.value = '';
  }

  async getReview() {
    this.reviewId = this.activatedRoute.snapshot.params['id']
    this.review = await firstValueFrom(this.reviewsService.getReviewById(this.reviewId));
    this.grade = this.review.userRating;
    this.loader = true;
  }

  onSetLike() {
    this.review.likeStatus = !this.review.likeStatus;
    if (this.review.likeStatus){
      this.review.likesCount += 1;
    } else {
      this.review.likesCount -= 1;
    }
    this.http.post('api/likes', {reviewId: this.reviewId, status: this.review.likeStatus}).subscribe()
  }
}
