import { Injectable } from "@angular/core";
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
}
