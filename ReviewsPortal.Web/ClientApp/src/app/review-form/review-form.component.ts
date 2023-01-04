import {Component, EventEmitter, Input, Output} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { CategoryService } from "../../common/services/category/category.service";
import { TagService } from "../../common/services/tag/tag.service";
import { ReviewFormModel } from "../../common/models/review-form-model";
import { TagModel } from "ngx-chips/core/tag-model";
import { map, Observable } from "rxjs";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-review-form',
  templateUrl: 'review-form.component.html',
  styleUrls: ['review-form.component.css']
})

export class ReviewFormComponent {

  grade = 1;
  @Input() @Output() tags!: string[];
  categories!: string[];
  @Input() @Output() files?: File[] = [];

  @Output() onSubmitReview = new EventEmitter<boolean>();
  @Input() @Output() reviewForm!: ReviewFormModel;
  @Input() @Output() categoryName?: string;

  constructor(private http: HttpClient, private router: Router,
              private categoryService: CategoryService,
              private tagService: TagService) {
    this.getAllCategories();
  }

  getAllCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: value => this.categories = value.map(function (a: any) {
        return a.categoryName;
      }),
      error: err => {
        console.log(err)
      }
    });
  }

  onAddTag(tag: TagModel) {
    this.tags.push((<any>tag).value);
    this.reviewForm.patchValue({
      tags: this.tags
    });
  }

  onRemoveTag(tag: any){
    let tagIndex = this.tags.indexOf(tag);
    if (tagIndex != -1){
      this.tags.slice(tagIndex);
    }
    this.reviewForm.patchValue({
      tags: this.tags
    });
  }

  onSelectImage(event: any) {
    if (event.addedFiles[0] === undefined) return;
    this.files!.push(<File>event.addedFiles[0]);
    this.reviewForm.patchValue({
      images: <any>this.files
    })
  }

  onRemoveImage(event: any){
    let fileIndex = this.files!.indexOf(event);
    this.files!.splice(fileIndex, 1);
  }

  requestAutocompleteTags = (text: any): Observable<any> => {
    return this.tagService.getAllTags().pipe(
      map((data: any) => {
        return data;
      })
    )
  }

  onSubmit() {
    this.onSubmitReview.emit();
  }

  onGradeChange(e: number) {
    this.grade = e;
    this.reviewForm.patchValue({
      grade: this.grade
    });
  }
}
