import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
    name: 'SppcReplaceLineBreaks'
})
export class ReplaceLineBreaksPipe implements PipeTransform {
    transform(value: string): string {
        return value.replace(/\n/g, '<br/>');
    }
}