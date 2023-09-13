import { Injectable } from '@angular/core';
import { StatusContaEnum } from 'src/app/enums/statusContaEnum';
import { CreateContaDto } from 'src/app/dto/financas/contas/createContaDto';
import { CreateContaVariavelDto } from 'src/app/dto/financas/contas/createContaVariavelDto';
import { ReadContaDto } from 'src/app/dto/financas/contas/readContaDto';
import { ReadContaVariavelDto } from 'src/app/dto/financas/contas/readContaVariavelDto';
import { UpdateContaDto } from 'src/app/dto/financas/contas/updateContaDto';
import { UpdateContaVariavelDto } from 'src/app/dto/financas/contas/updateContaVariavelDto';
import { Conta } from 'src/app/interfaces/financas/Conta';
import { PagamentoConta } from 'src/app/interfaces/financas/PagamentoConta';

@Injectable({
  providedIn: 'root'
})
export class ContaMapper {

  constructor() { }

  public static ContaDtoToConta(ReadContaDto: ReadContaDto): Conta {
    let conta: Conta = {
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
        MesReferencia: pagamentoDto.MesReferencia,
        AnoReferencia: pagamentoDto.AnoReferencia,
        ContaId: pagamentoDto.ContaId
      }
      conta.Pagamentos.push(pagamento);
    });
    this.PreecherStatusConta(conta);
    return conta;
  }

  public static ContaVariavelDtoToConta(ReadContaDto: ReadContaVariavelDto): Conta {
    let conta: Conta = {
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
        MesReferencia: pagamentoDto.MesReferencia,
        AnoReferencia: pagamentoDto.AnoReferencia,
        ContaId: pagamentoDto.ContaId
      }
      conta.Pagamentos.push(pagamento);
    });
    this.CalcularValorMedio(conta);
    this.PreecherStatusConta(conta);
    return conta;
  }

  public static ContaToUpdateContaDto(Conta: Conta): UpdateContaDto {
    let conta: UpdateContaDto = {
      Descricao: Conta.Descricao,
      ValorInteiro: Conta.ValorInteiro,
      ValorCentavos: Conta.ValorCentavos,
      DiaVencimento: Conta.DiaVencimento,
      CategoriaId: Conta.Categoria.Id
    };
    return conta;
  }

  public static ContaToUpdateContaVariavelDto(Conta: Conta): UpdateContaVariavelDto {
    let conta: UpdateContaVariavelDto = {
      Descricao: Conta.Descricao,
      DiaVencimento: Conta.DiaVencimento,
      CategoriaId: Conta.Categoria.Id
    };
    return conta;
  }

  public static ContaToCreateContaDto(Conta: Conta): CreateContaDto {
    let conta: CreateContaDto = {
      Descricao: Conta.Descricao,
      ValorInteiro: Conta.ValorInteiro,
      ValorCentavos: Conta.ValorCentavos,
      DiaVencimento: Conta.DiaVencimento,
      CategoriaId: Conta.Categoria.Id
    };
    return conta;
  }

  public static ContaToCreateContaVariavelDto(Conta: Conta): CreateContaVariavelDto {
    let conta: CreateContaVariavelDto = {
      Descricao: Conta.Descricao,
      DiaVencimento: Conta.DiaVencimento,
      CategoriaId: Conta.Categoria.Id
    };
    return conta;
  }

  private static CalcularValorMedio(Conta: Conta) {
    let totalPagamentos = Conta.Pagamentos.reduce((acc, cur) => acc + cur.ValorInteiro, 0) + Conta.Pagamentos.reduce((acc, cur) => acc + cur.ValorCentavos, 0) / 100;
    let mediaPagamentos = totalPagamentos / Conta.Pagamentos.length;
    Conta.ValorInteiro = Math.trunc(mediaPagamentos);
    Conta.ValorCentavos = Math.round((mediaPagamentos - Conta.ValorInteiro) * 100);
    if (Number.isNaN(Conta.ValorInteiro)) Conta.ValorInteiro = 0;
    if (Number.isNaN(Conta.ValorCentavos)) Conta.ValorCentavos = 0;
  }

  private static PreecherStatusConta(Conta: Conta) {
    if (Conta.Pagamentos.length > 0) {
      Conta.UltimoPagamento = Conta.Pagamentos
      .sort((a, b) => new Date(b.DataPagamento != undefined ? b.DataPagamento : 0).getTime() - new Date(a.DataPagamento != undefined ? a.DataPagamento : 0).getTime())[0].DataPagamento;
      let UltimoPagamento = new Date(Conta.UltimoPagamento != undefined ? Conta.UltimoPagamento : 0)
      let mesReferencia = UltimoPagamento.getMonth() + 1;
      let anoReferencia = UltimoPagamento.getFullYear();
      let mesAtual = new Date(Date.now()).getMonth() + 1;
      let anoAtual = new Date(Date.now()).getFullYear();
      let diaAtual = new Date(Date.now()).getDate();
      if (mesReferencia == mesAtual && anoReferencia == anoAtual) {
        Conta.StatusId = StatusContaEnum.Paga;
      }
      else {
        if ((mesReferencia == mesAtual -1 && anoReferencia == anoAtual && diaAtual < Conta.DiaVencimento)
              ||
            (anoReferencia == anoAtual -1 && mesReferencia == 12 && mesAtual == 1 && diaAtual < Conta.DiaVencimento)) {
          Conta.StatusId = StatusContaEnum.Pendente;
        }
        else {
          Conta.StatusId = StatusContaEnum.Atrasada
        }
      }
    }
  }
}
