import { Injectable } from "@angular/core";
import { CreateAssinaturaDto } from "src/app/dto/financas/cartoes/createAssinaturaDto";
import { ReadAssinaturaDto } from "src/app/dto/financas/cartoes/readAssinaturaDto";
import { Assinatura } from "src/app/interfaces/financas/Assinatura";


@Injectable({
  providedIn: "root",
})

export class AssinaturaMapper {

  constructor() { }

  public static AssinaturaDtoToAssinatura(ReadAssinaturaDto: ReadAssinaturaDto): Assinatura {
    let assinatura: Assinatura = {
      Id: ReadAssinaturaDto.Id,
      Descricao: ReadAssinaturaDto.Descricao,
      ValorInteiro: ReadAssinaturaDto.ValorInteiro,
      ValorCentavos: ReadAssinaturaDto.ValorCentavos,
      DiaVencimento: ReadAssinaturaDto.DiaVencimento,
      CartaoId: ReadAssinaturaDto.CartaoId,
    }
    return assinatura;
  }

  public static AssinaturaToCreateAssinaturaDto(Assinatura: Assinatura): CreateAssinaturaDto {
    let createAssinaturaDto: CreateAssinaturaDto = {
      Descricao: Assinatura.Descricao,
      ValorInteiro: Assinatura.ValorInteiro,
      ValorCentavos: Assinatura.ValorCentavos,
      DiaVencimento: Assinatura.DiaVencimento,
      CartaoId: Assinatura.CartaoId,
    }
    return createAssinaturaDto;
  }
}
