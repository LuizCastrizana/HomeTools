import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidacaoService {

  constructor() { }

  apenasNumeros(event: any): boolean {
    const pattern = /[0-9]/;
    let inputChar = String.fromCharCode(event.charCode);
    if (!pattern.test(inputChar))
      return false;
    else
      return true;
  }

  diaMes(val: string): boolean {
    const pattern = /[0-9]*/;
    if(pattern.test(val) && parseInt(val) <= 31 && parseInt(val) > 0) {
      return true;
    }
    return false;
  }
}