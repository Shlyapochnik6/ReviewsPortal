import {HttpClient} from "@angular/common/http";
import {TranslateLoader} from "@ngx-translate/core";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";

export function HttpLoaderFactory(http: HttpClient) : TranslateLoader {
  return new TranslateHttpLoader(http, './assets/locale/', '.json');
}
