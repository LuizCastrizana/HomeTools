import { Router } from '@angular/router';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { PagamentoConta } from '../../../../interfaces/financas/PagamentoConta';
import { Component, Input, OnInit } from '@angular/core';
import { CreatePagamentoContaDto } from 'src/app/dto/financas/contas/createPagamentoContaDto';
import { CreatePagamentoContaVariavelDto } from 'src/app/dto/financas/contas/createPagamentoContaVariavelDto';
import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { Conta } from 'src/app/interfaces/financas/Conta';
import { PagamentoContaVariavelService } from 'src/app/services/Financas/pagamento-conta-variavel.service';
import { PagamentoContaService } from 'src/app/services/Financas/pagamento-conta.service';
import { FeedbackService } from 'src/app/services/feedback.service';
import { ValidacaoService } from 'src/app/services/validacao.service';
import { RespostaApiService } from 'src/app/services/resposta-api.service';


@Component({
  selector: 'app-incluir-pagamento-conta',
  templateUrl: './incluir-pagamento-conta.component.html',
  styleUrls: ['./incluir-pagamento-conta.component.css']
})
export class IncluirPagamentoContaComponent implements OnInit {

  @Input() Conta: Conta = {} as Conta;
  PagamentoConta: CreatePagamentoContaDto = {} as CreatePagamentoContaDto;
  PagamentoContaVariavel: CreatePagamentoContaVariavelDto = {} as CreatePagamentoContaVariavelDto;

  DataPagamento: Date = new Date();
  MesReferencia: number = new Date(Date.now()).getMonth() + 1;
  AnoReferencia: number = new Date(Date.now()).getFullYear();

  constructor(
    private validacaoService: ValidacaoService,
    private pagamentoContaService: PagamentoContaService,
    private pagamentoContaVariavelService: PagamentoContaVariavelService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  incluirPagamento(): void {
    if (!this.validarCamposObrigatorios(this.Conta.Variavel)) {
      return;
    }
    if (this.Conta.Variavel){
      this.PagamentoContaVariavel.ContaVariavelId = this.Conta.Id!;
      this.PagamentoContaVariavel.DataPagamento = this.DataPagamento;
      this.PagamentoContaVariavel.MesReferencia = this.MesReferencia;
      this.PagamentoContaVariavel.AnoReferencia = this.AnoReferencia;
      this.pagamentoContaVariavelService.create(this.PagamentoContaVariavel).subscribe({
        next: (pagamentoContaVariavel) => {
          let pagamento = {
            Id: pagamentoContaVariavel.valor.Id,
            DataPagamento: pagamentoContaVariavel.valor.DataPagamento,
            ValorInteiro: pagamentoContaVariavel.valor.ValorInteiro,
            ValorCentavos: pagamentoContaVariavel.valor.ValorCentavos,
            MesReferencia: pagamentoContaVariavel.valor.MesReferencia,
            AnoReferencia: pagamentoContaVariavel.valor.AnoReferencia,
            ContaId: pagamentoContaVariavel.valor.ContaId
          } as PagamentoConta;
          this.Conta.Pagamentos.push(pagamento);
          let dadosFeedback = {
            Id: 'feedback',
            Titulo: "Sucesso!",
            Mensagem: "Pagamento incluído com sucesso!",
            TipoAlerta: TipoAlertaEnum.Sucesso,
          } as DadosFeedbackAlerta;
          this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/contas']);
          });
        }, error: (error) => {
           this.respostaApiService.tratarRespostaApi(error);
           document.getElementById('modalIncluirPagamento')!.style.display = 'none';
        }
      });
    }
    else {
      this.PagamentoConta.ContaId = this.Conta.Id!;
      this.PagamentoConta.DataPagamento = this.DataPagamento;
      this.PagamentoConta.MesReferencia = this.MesReferencia;
      this.PagamentoConta.AnoReferencia = this.AnoReferencia;
      this.pagamentoContaService.create(this.PagamentoConta).subscribe({
        next: (pagamentoConta) => {
          let pagamento = {
            Id: pagamentoConta.valor.Id,
            DataPagamento: pagamentoConta.valor.DataPagamento,
            ValorInteiro: 0,
            ValorCentavos: 0,
            MesReferencia: pagamentoConta.valor.MesReferencia,
            AnoReferencia: pagamentoConta.valor.AnoReferencia,
            ContaId: pagamentoConta.valor.ContaId
          } as PagamentoConta;
          this.Conta.Pagamentos.push(pagamento);
          let dadosFeedback = {
            Id: 'feedback',
            Titulo: "Sucesso!",
            Mensagem: "Pagamento incluído com sucesso!",
            TipoAlerta: TipoAlertaEnum.Sucesso,
          } as DadosFeedbackAlerta;
          this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/contas']);
          });
        }, error: (error) => {
           this.respostaApiService.tratarRespostaApi(error);
           document.getElementById('modalIncluirPagamento')!.style.display = 'none';
        }
      });
    }
  }

  cancelar() {
    this.removerValidacao();
    document.getElementById('modalIncluirPagamento')!.style.display = 'none';
  }

  apenasNumeros(event: any) {
    if (!this.validacaoService.apenasNumeros(event)) {
      event.preventDefault();
    }
  }

  validarCamposObrigatorios(variavel: boolean): boolean {
    let erros: number = 0;
    let camposObrigatorios = ['txtData', 'txtAno'];
    if (variavel) {
      camposObrigatorios.push('txtValorInteiro');
      camposObrigatorios.push('txtValorCentavos');
    }
    camposObrigatorios.forEach(campo => {
      if (!this.campoObrigatorio(campo)) {
        erros++;
      }
    });
    return erros == 0 ? true : false;
  }

  removerValidacao() {
    let campos = ['txtData', 'txtAno', 'txtValorInteiro', 'txtValorCentavos'];
    campos.forEach(campo => {
      document.getElementById(campo)!.classList.remove('campo-obrigatorio');
      let campoErro = document.getElementById('erro_' + campo)!;
      campoErro.style.display = 'none';
      campoErro.innerHTML = "";
    });
  }

  campoObrigatorio(campoId: string): boolean {
    let campo = <HTMLInputElement>document.getElementById(campoId)!;
    let campoErro = document.getElementById('erro_' + campoId)!;
    if (campo.value === "") {
      campo.classList.add('campo-obrigatorio');
      campoErro.style.display = 'block';
      campoErro.innerHTML = "Campo obrigatório";
      return false;
    }
    else {
      campoErro.style.display = 'none';
      campo.classList.remove('campo-obrigatorio');
      return true;
    }
  }

}
