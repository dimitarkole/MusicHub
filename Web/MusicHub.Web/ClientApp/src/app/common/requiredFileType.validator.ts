import { FormControl } from "@angular/forms";

export function requiredFileType(type: string) {
  return function (control: FormControl) {
    const file = control.value;
    if (file) {
      const extension = file.split(';')[0].toLowerCase();
      if (extension.indexOf(type.toLowerCase()) == -1 ) {
        return {
          requiredFileType: true
        };
      }

      return null;
    }

    return null;
  };
}
