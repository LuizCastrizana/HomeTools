import { DadosFeedbackAlerta } from './../../interfaces/dadosFeedbackAlerta';
import { Component, Input, OnInit } from '@angular/core';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { FeedbackService } from 'src/app/services/feedback.service';

@Component({
  selector: 'app-feedback-alerta',
  templateUrl: './feedback-alerta.component.html',
  styleUrls: ['./feedback-alerta.component.css']
})
export class FeedbackAlertaComponent implements OnInit {

  @Input() DadosFeedbackAlerta: DadosFeedbackAlerta = {
    Id: "",
    TipoAlerta: TipoAlertaEnum.Sucesso,
    Titulo: "",
    Mensagem: ""
  };

  constructor(private feedbackService: FeedbackService) { }

  ngOnInit(): void {
  }

  tratarTipoAlerta() {
    switch (this.DadosFeedbackAlerta.TipoAlerta) {
      case TipoAlertaEnum.Sucesso:
        return "alerta-sucesso";
      case TipoAlertaEnum.Erro:
        return "alerta-erro";
      case TipoAlertaEnum.Atencao:
        return "alerta-atencao";
      default:
        return "alerta-sucesso";
    }
  }

  ocultar(Id: string) {
    this.feedbackService.ocultarFeedbackAlerta(Id);
  }
}
