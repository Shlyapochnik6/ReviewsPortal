import {FormGroup} from "@angular/forms";

export function dataToForm(form: FormGroup) {

  const formData = new FormData();

  if (form.get('tags')?.value) {
    const tags = <string[]>form.get('tags')?.value;
    formData.delete('tags');
    tags.forEach(t => formData.append('tags', t));
  }

  for (const key of Object.keys(form.value)) {
    const value = form.value[key];
    if (value instanceof Array){
      for (const elem of value){
        formData.append(key, elem);
      }
    }
    else {
      formData.append(key, value);
    }
  }
  return formData;
}
