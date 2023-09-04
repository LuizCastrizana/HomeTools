import { RespostaApi } from './../../../interfaces/api-dto/respostaApi';
import { DadosPaginador } from '../../../interfaces/paginacao/dadosPaginador';
import { Component, OnInit } from '@angular/core';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { DadosPaginados } from '../../../interfaces/paginacao/dadosPaginados';
import { ItemPagina } from 'src/app/interfaces/paginacao/itemPagina';
import { ContaService } from 'src/app/services/Financas/conta.service';
import { ReadContaDto } from 'src/app/interfaces/api-dto/Financas/readConta';
import { PagamentoConta } from 'src/app/interfaces/financas/readPagamentoConta';
import { ContaVariavelService } from 'src/app/services/Financas/conta-variavel.service';
import { ReadContaVariavelDto } from 'src/app/interfaces/api-dto/Financas/readContaVariavel';

@Component({
  selector: 'app-painel-contas',
  templateUrl: './painel-contas.component.html',
  styleUrls: ['./painel-contas.component.css'],
})
export class PainelContasComponent implements OnInit {

  constructor(private contaService: ContaService, private contaVariavelService: ContaVariavelService) {}

  Ordem: string = 'asc';
  NomeCampo: string = '';
  DadosPaginador: DadosPaginador = {
    PaginaAtual: 1,
    ItensPorPagina: 2,
    TotalItens: 0,
  };
  DadosPaginados: DadosPaginados<ReadConta> = {
    Pagina: 1,
    ItensPorPagina: 15,
    TotalItens: 0,
    Itens: [],
  };
  Contas: ReadConta[] = [
    // {
    //   Id: 1,
    //   Descricao: 'Conta de Luz',
    //   ValorInteiro: 100,
    //   ValorCentavos: 0,
    //   DiaVencimento: 10,
    //   Categoria: {
    //     Id: 1,
    //     Descricao: 'Casa',
    //   },
    // },
    // {
    //   Id: 2,
    //   Descricao: 'Conta de Água',
    //   ValorInteiro: 50,
    //   ValorCentavos: 0,
    //   DiaVencimento: 15,
    //   Categoria: {
    //     Id: 1,
    //     Descricao: 'Casa',
    //   },
    // },
    // {
    //   Id: 3,
    //   Descricao: 'Intenet',
    //   ValorInteiro: 120,
    //   ValorCentavos: 0,
    //   DiaVencimento: 12,
    //   Categoria: {
    //     Id: 1,
    //     Descricao: 'Casa',
    //   },
    // },
    // {
    //   Id: 4,
    //   Descricao: 'Vigia',
    //   ValorInteiro: 40,
    //   ValorCentavos: 0,
    //   DiaVencimento: 15,
    //   Categoria: {
    //     Id: 2,
    //     Descricao: 'Segurança',
    //   },
    // },
  ];

  ngOnInit(): void {
    this.contaService.listar().subscribe((respostaApi) => {
      this.incluirContas(respostaApi);
      this.contaVariavelService.listar().subscribe((respostaApi2) => {
        this.incluirContasVariaveis(respostaApi2);
        this.DadosPaginador.TotalItens = this.Contas.length;
        this.ordenarContas();
        this.paginarContas();
      });
    });
  }

  incluirContas(respostaApi: RespostaApi<ReadContaDto[]>) {
    let index = 0;
    respostaApi.Valor.forEach((contaDto) => {
      index++;
      let conta: ReadConta = {
        Id: index,
        Descricao: contaDto.Descricao,
        ValorInteiro: contaDto.ValorInteiro,
        ValorCentavos: contaDto.ValorCentavos,
        DiaVencimento: contaDto.DiaVencimento,
        Categoria: contaDto.Categoria,
        Pagamentos: [],
        UltimoPagamento: undefined,
        Variavel: false
      }
      contaDto.Pagamentos.forEach(pagamentoDto => {
        let pagamento: PagamentoConta = {
          Id: pagamentoDto.Id,
          ValorInteiro: 0,
          ValorCentavos: 0,
          DataPagamento: pagamentoDto.DataPagamento,
          ContaId: pagamentoDto.ContaId
        }
        conta.Pagamentos.push(pagamento);
      });
      if (conta.Pagamentos.length > 0) {
        conta.UltimoPagamento = conta.Pagamentos.sort((a, b) => b.Id - a.Id)[0].DataPagamento;
      }
      this.Contas.push(conta);
    });
    this.DadosPaginador.TotalItens = this.Contas.length;
  }

  incluirContasVariaveis(respostaApi: RespostaApi<ReadContaVariavelDto[]>) {
    let index = 0;
    respostaApi.Valor.forEach((contaDto) => {
      index++;
      let conta: ReadConta = {
        Id: index,
        Descricao: contaDto.Descricao,
        ValorInteiro: 0,
        ValorCentavos: 0,
        DiaVencimento: contaDto.DiaVencimento,
        Categoria: contaDto.Categoria,
        Pagamentos: [],
        UltimoPagamento: undefined,
        Variavel: true
      }
      contaDto.Pagamentos.forEach(pagamentoDto => {
        let pagamento: PagamentoConta = {
          Id: pagamentoDto.Id,
          ValorInteiro: pagamentoDto.ValorInteiro,
          ValorCentavos: pagamentoDto.ValorCentavos,
          DataPagamento: pagamentoDto.DataPagamento,
          ContaId: pagamentoDto.ContaId
        }
        conta.Pagamentos.push(pagamento);
      });
      conta.ValorInteiro = conta.Pagamentos.reduce((total, pagamento) => total + pagamento.ValorInteiro, 0) / conta.Pagamentos.length;
      conta.ValorCentavos = conta.Pagamentos.reduce((total, pagamento) => total + pagamento.ValorCentavos, 0) / conta.Pagamentos.length;
      if (conta.Pagamentos.length > 0) {
        conta.UltimoPagamento = conta.Pagamentos.sort((a, b) => b.Id - a.Id)[0].DataPagamento;
      }
      this.Contas.push(conta);
    });
    this.DadosPaginador.TotalItens = this.Contas.length;
  }
  ordenarContas(nomeCampo?: string) {
    if (this.Ordem == 'asc') {
      this.Ordem = 'desc';
    }
    else {
      this.Ordem = 'asc';
    }

    switch (nomeCampo) {
      case 'Descricao':
        this.NomeCampo = 'Descricao';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) =>
            a.Descricao.localeCompare(b.Descricao, 'pt-BR')
          );
        } else {
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
          this.Contas.sort((a, b) =>
            b.Categoria.Descricao.localeCompare(a.Categoria.Descricao, 'pt-BR')
          );
        }
        break;
      case 'Valor':
        this.NomeCampo = 'Valor';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.ValorInteiro - b.ValorInteiro);
        } else {
          this.Contas.sort((a, b) => b.ValorInteiro - a.ValorInteiro);
        }
        break;
      case 'Vencimento':
        this.NomeCampo = 'Vencimento';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => a.DiaVencimento - b.DiaVencimento);
        } else {
          this.Contas.sort((a, b) => b.DiaVencimento - a.DiaVencimento);
        }
        break;
      case 'UltimoPagamento':
        this.NomeCampo = 'UltimoPagamento';
        if (this.Ordem == 'asc') {
          this.Contas.sort((a, b) => new Date(a.UltimoPagamento != undefined ? a.UltimoPagamento : 0).getTime() - new Date(b.UltimoPagamento != undefined ? b.UltimoPagamento : 0).getTime());
        } else {
          this.Contas.sort((a, b) => new Date(b.UltimoPagamento != undefined ? b.UltimoPagamento : 0).getTime() - new Date(a.UltimoPagamento != undefined ? a.UltimoPagamento : 0).getTime());
        }
        break;
      default:
        this.NomeCampo = 'Id';
        this.Ordem = 'desc';
        this.Contas.sort((a, b) => b.Id - a.Id);
        break;
    }
    this.tratarIconeOrdenacao();
    this.DadosPaginador.PaginaAtual = 1;
    this.paginarContas();
  }

  tratarIconeOrdenacao() {
    let imgAsc = "<img src=\"../../../../assets/img/asc.png\" class=\"img-ordenacao\">";
    let imgDesc = "<img src=\"../../../../assets/img/desc.png\" class=\"img-ordenacao\">";
    let imgDescricao = document.getElementById('imgDescricao');
    let imgCategoria = document.getElementById('imgCategoria');
    let imgValor = document.getElementById('imgValor');
    let imgVencimento = document.getElementById('imgVencimento');
    let imgUltPagamento = document.getElementById('imgUltPagamento');

    imgDescricao!.innerHTML = '';
    imgCategoria!.innerHTML = '';
    imgValor!.innerHTML = '';
    imgVencimento!.innerHTML = '';
    imgUltPagamento!.innerHTML = '';

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
      default:
        break;
      }
  }

  paginarContas() {
    this.DadosPaginados.Pagina = this.DadosPaginador.PaginaAtual;
    this.DadosPaginados.ItensPorPagina = this.DadosPaginador.ItensPorPagina;
    this.DadosPaginados.TotalItens = this.DadosPaginador.TotalItens;

    let itensPagina: ItemPagina<ReadConta>[] = [];
    let pagina = 1;
    this.Contas.forEach(item => {
      itensPagina.push({  Pagina: pagina, Item: item });
      if (itensPagina.length == this.DadosPaginados.ItensPorPagina * pagina) {
        pagina++;
      }
    });

    this.DadosPaginados.Itens = itensPagina.filter(item => item.Pagina == this.DadosPaginados.Pagina).map(item => item.Item);
  }

  receiveMessage($event: DadosPaginador) {
    this.DadosPaginador = $event
    this.paginarContas();
  }
}
