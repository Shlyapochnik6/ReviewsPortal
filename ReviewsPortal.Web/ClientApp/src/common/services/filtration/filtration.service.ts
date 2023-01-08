import {Injectable} from "@angular/core";
import {filterBy, FilterOperator} from "@progress/kendo-data-query";
import {BriefReviewModel} from "../../models/BriefReviewModel";
import {AllUserReviewsModel} from "../../models/AllUserReviewsModel";

@Injectable({
  providedIn: 'root'
})

export class FiltrationService {
  filterData(fieldName: string, filterText: string,
             data: AllUserReviewsModel[]): AllUserReviewsModel[] {
    return filterBy(data, {
      logic: 'and',
      filters: [
        {field: fieldName, value: filterText, operator: 'contains', ignoreCase: true}
      ]
    })
  }
}
