import { Injectable } from '@angular/core';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { FeedbackService } from '../services/feedback.service';

@Injectable({
  providedIn: 'root'
})
export class RespostaApiService {

  constructor(
    private FeedbackService: FeedbackService,
  ) { }

  tratarRespostaApi(resposta: any) {
    let DadosFeedbackAlerta = {} as DadosFeedbackAlerta;
    switch(resposta.status) {
      case 200:
        DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Sucesso,
          Titulo: 'Sucesso!',
          Mensagem: resposta.mensagem,
        } as DadosFeedbackAlerta;
        break;
      case 204:
        DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Sucesso,
          Titulo: 'Sucesso!',
          Mensagem: '',
        } as DadosFeedbackAlerta;
        break;
      case 400:
        DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Atencao,
          Titulo: 'Validação rejeitada!',
          Mensagem: 'Erros: ' + resposta.error.erros.join(', ')
        } as DadosFeedbackAlerta;
        break;
      case 404:
        DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: 'Erro!',
          Mensagem: 'Recurso não encontrado.',
        } as DadosFeedbackAlerta;
        break;
      case 500:
        DadosFeedbackAlerta = {
          Id: 'feedback',
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: 'Erro!',
          Mensagem: resposta.error.mensagem,
        };
        break;
        default:
        DadosFeedbackAlerta = {
            Id: 'feedback',
            TipoAlerta: TipoAlertaEnum.Erro,
            Titulo: 'Erro!',
            Mensagem: 'Não foi possível completar a operação',
          } as DadosFeedbackAlerta;
          break;
      }
      this.FeedbackService.gerarFeedbackAlerta(DadosFeedbackAlerta);
  }
}
