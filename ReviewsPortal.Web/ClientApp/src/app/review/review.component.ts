import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { DetailedReviewModel } from "../../common/models/DetailedReviewModel";
import { CommentModel } from "../../common/models/CommentModel";
import { CommentService } from "../../common/services/hub/comment.service";

@Component({
  selector: 'app-review',
  templateUrl: 'review.component.html',
  styleUrls: ['review.component.css']
})

export class ReviewComponent implements OnInit {

  btnClass = "btn btn-secondary";
  waiter!: Promise<boolean>;
  review!: DetailedReviewModel;
  reviewId: number = 0;
  grade: number = 0;

  constructor(private activatedRoute: ActivatedRoute,
              private http: HttpClient,
              public commentService: CommentService) {
    this.getReview()
    this.getAllComments()
  }

  async ngOnInit(){
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

  getReview() {
    this.reviewId = this.activatedRoute.snapshot.params['id']
    this.http.get<DetailedReviewModel>(`api/reviews?reviewId=${this.reviewId}`)
      .subscribe({
        next: data => {
          this.review = data
          this.waiter = Promise.resolve(true)
        },
        complete: () => {
          this.grade = this.review.userRating
          console.log(this.review.imagesUrl)
        }
      })
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
