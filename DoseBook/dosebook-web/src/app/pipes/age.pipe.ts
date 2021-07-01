import { Pipe, PipeTransform } from '@angular/core';
import { toAge } from 'src/app/helpers/util';
@Pipe({
  name: 'age'
})
export class AgePipe implements PipeTransform {

  transform(value: any, ...args: unknown[]): number {
    if (! (value instanceof Date)) {
      value = new Date(value);
    }
    return toAge(value);
  }

}
