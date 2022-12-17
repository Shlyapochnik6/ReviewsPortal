﻿import {Component, Input, Output} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { CategoryService } from "../../common/services/category/category.service";
import { TagService } from "../../common/services/tag/tag.service";
import { ReviewFormModel } from "../../common/models/review-form-model";
import { TagModel } from "ngx-chips/core/tag-model";
import { map, Observable } from "rxjs";

@Component({
  selector: 'app-review-form',
  templateUrl: 'review-form.component.html',
  styleUrls: ['review-form.component.css']
})

export class ReviewFormComponent {

  grade = 1;
  tags: string[] = [];
  categories!: string[];
  file?: File;

  @Input() @Output() reviewForm!: ReviewFormModel;
  @Input() onSubmitForm!: Function;

  constructor(private http: HttpClient, private router: Router,
              private categoryService: CategoryService,
              private tagService: TagService) {
    this.getAllCategories();
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

  requestAutocompleteTags = (text: any): Observable<any> => {
    return this.tagService.getAllTags().pipe(
      map((data: any) => {
        return data;
      })
    )
  }

  getAllCategories() {
    this.categoryService.getAllCategories().subscribe({
      next: value => this.categories == value.map(function(category: any) {
        return category.name;
      })
    });
  }

  onGradeChange(e: number) {
    this.grade = e;
    this.reviewForm.patchValue({
      grade: this.grade
    });
  }

  onSubmit() {
    this.onSubmitForm();
  }
}
