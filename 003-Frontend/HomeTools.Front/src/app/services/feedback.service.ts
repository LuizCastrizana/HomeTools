import { DadosFeedbackAlerta } from './../interfaces/dadosFeedbackAlerta';
import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  public static FeedbackAlertaEmitter = new EventEmitter<DadosFeedbackAlerta>();

  constructor() { }

  exibirFeedbackAlerta(Id?: string) {
    let alerta = document.getElementById(Id!);
    if (alerta != null) {
      alerta.classList.remove('hide');
      alerta.classList.add('show');
    } else {
      let alertas = document.getElementsByClassName('feedback-alerta');
      for (let i = 0; i < alertas.length; i++) {
        alertas[i].classList.remove('hide');
        alertas[i].classList.add('show');
      }
    }
  }

  ocultarFeedbackAlerta() {
    let alertas = document.getElementsByClassName("feedback-alerta");
    for (let i = 0; i < alertas.length; i++) {
      alertas[i].classList.remove("show");
      alertas[i].classList.add("hide");
    }
  }
}