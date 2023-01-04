import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {BriefReviewModel} from "../../models/BriefReviewModel";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {AllUserReviewsModel} from "../../models/AllUserReviewsModel";

@Injectable({
  providedIn: 'root'
})

export class ReviewsService {

  public reviews: any;
  promise!: Promise<boolean>;
  tag: string | undefined;
  sorting?: string | null = 'recently-added';

  constructor(private http: HttpClient, private route: ActivatedRoute,
              private router: Router) {
  }

  getAllReviews() {
    this.getParameters();
    let url = this.tag === undefined
      ? `api/reviews/get-all?sorting=${this.sorting}`
      : `api/reviews/get-all?sorting=${this.sorting}&tag=${this.tag}`;
    this.http.get<BriefReviewModel>(url)
      .subscribe({
        next: data => {
          console.log(data)
          this.reviews = data;
          this.promise = Promise.resolve(true);
        }
      });
  }

  getParameters() {
    this.route.queryParams.subscribe(params => {
      this.sorting = params['sorting'];
      this.tag = params['tag'];
    })
    if (this.sorting === undefined){
      this.sorting = 'recently-added';
      this.tag = undefined;
    }
  }

  async setParameters(sorting?: string | null, tag?: string | undefined) {
    this.promise = Promise.resolve(false);
    this.sorting = sorting;
    this.tag = tag;
    await this.setRoute();
  }

  async setRoute() {
    await this.router.navigate(['/'], {
      queryParams: {
        'sorting': this.sorting,
        'tag': this.tag
      }
    })
  }
}
