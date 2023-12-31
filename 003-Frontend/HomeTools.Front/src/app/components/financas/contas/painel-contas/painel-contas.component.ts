import { FeedbackService } from '../../../../services/feedback.service';
import { RespostaApi } from '../../../../dto/respostaApi';
import { DadosPaginador } from '../../../../interfaces/paginacao/dadosPaginador';
import { Component, OnInit } from '@angular/core';
import { Conta } from 'src/app/interfaces/financas/Conta';
import { DadosPaginados } from '../../../../interfaces/paginacao/dadosPaginados';
import { ItemPagina } from 'src/app/interfaces/paginacao/itemPagina';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { ReadContaDto } from 'src/app/dto/financas/contas/readContaDto';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ReadContaVariavelDto } from 'src/app/dto/financas/contas/readContaVariavelDto';
import { ContaMapper } from 'src/app/mappers/financas/ContaMapper';
import { TipoAlertaEnum } from 'src/app/enums/tipoAlertaEnum';
import { DadosFeedbackAlerta } from 'src/app/interfaces/dadosFeedbackAlerta';
import { RespostaApiService } from 'src/app/services/resposta-api.service';

@Component({
  selector: 'app-painel-contas',
  templateUrl: './painel-contas.component.html',
  styleUrls: ['./painel-contas.component.css'],
})
export class PainelContasComponent implements OnInit {

  Ordem: string = '';
  NomeCampo: string = '';

  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 15,
    TotalItens: 0,
  };

  DadosPaginados: DadosPaginados<Conta> = { } as DadosPaginados<Conta>;
  ContaAcao: Conta = { } as Conta;
  Contas: Conta[] = [];

  constructor(
    private contaService: ContaService,
    private contaVariavelService: ContaVariavelService,
    private feedbackService: FeedbackService,
    private respostaApiService: RespostaApiService,
    ) {}

  ngOnInit(): void {
    this.contaService.listar().subscribe({
      next: (respostaApi) => {
        this.incluirContas(respostaApi);
        this.ordenarContas();
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback1",
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter as contas."
        } as DadosFeedbackAlerta;
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      }
    });
    this.contaVariavelService.listar().subscribe({
      next: (respostaApi2) => {
        this.incluirContasVariaveis(respostaApi2);
        this.ordenarContas();
      }, error: (err) => {
        let dadosFeedback = {
          Id: "feedback2",
          TipoAlerta: TipoAlertaEnum.Erro,
          Titulo: "Erro!",
          Mensagem: "Não foi possível obter as contas variáveis."
        } as DadosFeedbackAlerta;
        this.feedbackService.gerarFeedbackAlerta(dadosFeedback);
      }
    });
  }

  incluirContas(respostaApi: RespostaApi<ReadContaDto[]>) {
    respostaApi.valor.forEach((contaDto) => {
      let conta = ContaMapper.ContaDtoToConta(contaDto);
      this.Contas.push(conta);
    });
    this.DadosPaginador.TotalItens = this.Contas.length;
  }

  incluirContasVariaveis(respostaApi: RespostaApi<ReadContaVariavelDto[]>) {
    respostaApi.valor.forEach((contaDto) => {
      let conta = ContaMapper.ContaVariavelDtoToConta(contaDto);
      this.Contas.push(conta);
    });
    this.DadosPaginador.TotalItens = this.Contas.length;
  }

  buscarContas() {
    let busca = (document.getElementById('txtBusca') as HTMLInputElement).value;
    this.Contas = [];
    this.contaService.buscar(busca).subscribe({
      next: (respostaApi) => {
        this.incluirContas(respostaApi);
        this.ordenarContas();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
    this.contaVariavelService.buscar(busca).subscribe({
      next: (respostaApi2) => {
        this.incluirContasVariaveis(respostaApi2);
        this.ordenarContas();
      }, error: (err) => {
        this.respostaApiService.tratarRespostaApi(err);
      }
    });
  }

  ordenarContas(nomeCampo?: string) {
    if (nomeCampo == this.NomeCampo) {
      if (this.Ordem == 'desc') {
        this.Ordem = 'asc';
      } else if (this.Ordem == 'asc') {
        this.Ordem = '';
        this.NomeCampo = '';
        nomeCampo = '';
      }
    } else {
      this.Ordem = '';
    }

    switch (nomeCampo) {
      case 'Descricao':
        this.NomeCampo = 'Descricao';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) =>
            a.Descricao.localeCompare(b.Descricao, 'pt-BR')
          );
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) =>
            b.Descricao.localeCompare(a.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Categoria':
        this.NomeCampo = 'Categoria';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) =>
            a.Categoria.Descricao.localeCompare(b.Categoria.Descricao, 'pt-BR')
          );
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) =>
            b.Categoria.Descricao.localeCompare(a.Categoria.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Valor':
        this.NomeCampo = 'Valor';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.ValorInteiro - b.ValorInteiro && a.ValorCentavos - b.ValorCentavos);
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) => b.ValorInteiro - a.ValorInteiro && b.ValorCentavos - a.ValorCentavos);
        }
        break;
      case 'Vencimento':
        this.NomeCampo = 'Vencimento';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.DiaVencimento - b.DiaVencimento);
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) => b.DiaVencimento - a.DiaVencimento);
        }
        break;
      case 'UltimoPagamento':
        this.NomeCampo = 'UltimoPagamento';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => new Date(a.UltimoPagamento != undefined ? a.UltimoPagamento : 0).getTime() - new Date(b.UltimoPagamento != undefined ? b.UltimoPagamento : 0).getTime());
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) => new Date(b.UltimoPagamento != undefined ? b.UltimoPagamento : 0).getTime() - new Date(a.UltimoPagamento != undefined ? a.UltimoPagamento : 0).getTime());
        }
        break;
      case  'Status':
        this.NomeCampo = 'Status';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.StatusId - b.StatusId);
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) => b.StatusId - a.StatusId);
        }
        break;
      case 'Tipo':
        this.NomeCampo = 'Tipo';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.Variavel == b.Variavel ? 0 : a.Variavel ? -1 : 1);
        } else {
          this.Ordem = 'desc';
          this.Contas.sort((a, b) => a.Variavel == b.Variavel ? 0 : a.Variavel ? 1 : -1);
        }
        break;
      default:
        this.NomeCampo = '';
        this.Contas.sort((a, b) => b.Id - a.Id);
        break;
    }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.paginarContas();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" >";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" >";
    let imgDescricao = document.getElementById('imgDescricao');
    let imgCategoria = document.getElementById('imgCategoria');
    let imgValor = document.getElementById('imgValor');
    let imgVencimento = document.getElementById('imgVencimento');
    let imgUltPagamento = document.getElementById('imgUltPagamento');
    let imgStatus = document.getElementById('imgStatus');
    let imgTipo = document.getElementById('imgTipo');

    imgDescricao!.innerHTML = '';
    imgCategoria!.innerHTML = '';
    imgValor!.innerHTML = '';
    imgVencimento!.innerHTML = '';
    imgUltPagamento!.innerHTML = '';
    imgStatus!.innerHTML = '';
    imgTipo!.innerHTML = '';

    switch (this.NomeCampo) {
      case 'Descricao':
        if (this.Ordem == 'asc') {
          imgDescricao!.innerHTML = imgAsc;
        } else {
          imgDescricao!.innerHTML = imgDesc;
        }
        break;
      case 'Categoria':
        if (this.Ordem == 'asc') {
          imgCategoria!.innerHTML = imgAsc;
        } else {
          imgCategoria!.innerHTML = imgDesc;
        }
        break;
      case 'Valor':
        if (this.Ordem == 'asc') {
          imgValor!.innerHTML = imgAsc;
        }
        else {
          imgValor!.innerHTML = imgDesc;
        }
        break;
      case 'Vencimento':
        if (this.Ordem == 'asc') {
          imgVencimento!.innerHTML = imgAsc;
        }
        else {
          imgVencimento!.innerHTML = imgDesc;
        }
        break;
      case 'UltimoPagamento':
        if (this.Ordem == 'asc') {
          imgUltPagamento!.innerHTML = imgAsc;
        }
        else {
          imgUltPagamento!.innerHTML = imgDesc;
          }
        break;
      case 'Status':
        if (this.Ordem == 'asc') {
          imgStatus!.innerHTML = imgAsc;
        }
        else {
          imgStatus!.innerHTML = imgDesc;
        }
        break;
      case 'Tipo':
          if (this.Ordem == 'asc') {
            imgTipo!.innerHTML = imgAsc;
          }
          else {
            imgTipo!.innerHTML = imgDesc;
          }
          break;
      default:
        break;
      }
  }

  paginarContas() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<Conta>[] = [];
    let pagina = 1;
    this.Contas.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(item => item.Pagina == this.DadosPaginados.Pagina).map(item => item.Item);
  }

  exibirModalExcluir(Conta: Conta) {
    this.ContaAcao = Conta;
    document.getElementById('modalExcluir')!.style.display = 'block';
  }

  exibirModalIncluirPagamento(Conta: Conta) {
    this.ContaAcao = Conta;
    document.getElementById('modalIncluirPagamento')!.style.display = 'block';
  }

  receiveMessage($event: DadosPaginador) {
    this.DadosPaginador = $event
    this.paginarContas();
  }

}


