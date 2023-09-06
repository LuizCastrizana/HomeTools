import { Injectable } from '@angular/core';
import { StatusContaEnum } from 'src/app/enums/statusContaEnum';
import { ReadContaDto } from 'src/app/interfaces/api-dto/financas/readContaDto';
import { ReadContaVariavelDto } from 'src/app/interfaces/api-dto/financas/readContaVariavelDto';
import { ReadConta } from 'src/app/interfaces/financas/readConta';
import { PagamentoConta } from 'src/app/interfaces/financas/readPagamentoConta';

@Injectable({
  providedIn: 'root'
})
export class ContaMapper {

  constructor() { }

  public static ContaDtoToConta(ReadContaDto: ReadContaDto): ReadConta {
    let conta: ReadConta = {
      Id: ReadContaDto.Id,
      Descricao: ReadContaDto.Descricao,
      ValorInteiro: ReadContaDto.ValorInteiro,
      ValorCentavos: ReadContaDto.ValorCentavos,
      DiaVencimento: ReadContaDto.DiaVencimento,
      Categoria: ReadContaDto.Categoria,
      Pagamentos: [],
      UltimoPagamento: undefined,
      Variavel: false,
      StatusId: StatusContaEnum.Pendente,
    }
    ReadContaDto.Pagamentos.forEach(pagamentoDto => {
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
      let UltimoPagamento = new Date(conta.UltimoPagamento != undefined ? conta.UltimoPagamento : 0);
      if (UltimoPagamento.getMonth() == new Date(Date.now()).getMonth()) {
        conta.StatusId = StatusContaEnum.Paga;
      }
      else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth()
        && conta.DiaVencimento < new Date(Date.now()).getDay()) {
          conta.StatusId = StatusContaEnum.Atrasada;
      }
      else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth() -1) {
        conta.StatusId = StatusContaEnum.Atrasada;
      }
    }
    return conta;
  }

  public static ContaVariavelDtoToConta(ReadContaDto: ReadContaVariavelDto): ReadConta {
    let conta: ReadConta = {
      Id: ReadContaDto.Id,
      Descricao: ReadContaDto.Descricao,
      ValorInteiro: 0,
      ValorCentavos: 0,
      DiaVencimento: ReadContaDto.DiaVencimento,
      Categoria: ReadContaDto.Categoria,
      Pagamentos: [],
      UltimoPagamento: undefined,
      Variavel: true,
      StatusId: StatusContaEnum.Pendente,
    }
    ReadContaDto.Pagamentos.forEach(pagamentoDto => {
      let pagamento: PagamentoConta = {
        Id: pagamentoDto.Id,
        ValorInteiro: pagamentoDto.ValorInteiro,
        ValorCentavos: pagamentoDto.ValorCentavos,
        DataPagamento: pagamentoDto.DataPagamento,
        ContaId: pagamentoDto.ContaId
      }
      conta.Pagamentos.push(pagamento);
    });
    if (conta.Pagamentos.length > 0) {
      conta.UltimoPagamento = conta.Pagamentos.sort((a, b) => b.Id - a.Id)[0].DataPagamento;
      let UltimoPagamento = new Date(conta.UltimoPagamento != undefined ? conta.UltimoPagamento : 0);
      if (UltimoPagamento.getMonth() == new Date(Date.now()).getMonth()) {
        conta.StatusId = StatusContaEnum.Paga;
      }
      else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth()
        && conta.DiaVencimento < new Date(Date.now()).getDay()) {
          conta.StatusId = StatusContaEnum.Atrasada;
      }
      else if (UltimoPagamento.getMonth() < new Date(Date.now()).getMonth() -1) {
        conta.StatusId = StatusContaEnum.Atrasada;
      }
    }
    return conta;
  }

}
