import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})

export class CategoryService {

  constructor(private http: HttpClient) {
  }

  getAllCategories() {
    console.log("getAllCategoriesService")
    return this.http.get<any>('api/categories');
  }
}
