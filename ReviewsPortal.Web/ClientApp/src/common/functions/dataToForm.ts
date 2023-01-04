export function dataToForm(formValue: any) {
  const formData = new FormData();

  for (const key of Object.keys(formValue)) {
    const value = formValue[key];
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
