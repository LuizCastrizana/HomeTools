import { DadosFeedbackAlerta } from './../interfaces/dadosFeedbackAlerta';
import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  public FeedbackAlertaEmitter = new EventEmitter<DadosFeedbackAlerta>();
  public FecharAletaEmitter = new EventEmitter<string>();

  constructor() { }

  gerarFeedbackAlerta(Dados: DadosFeedbackAlerta) {
    this.FeedbackAlertaEmitter.emit(Dados);
  }

  ocultarFeedbackAlerta(Id: string) {
    this.FecharAletaEmitter.emit(Id);
  }
}
