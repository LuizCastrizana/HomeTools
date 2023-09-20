import { Injectable } from "@angular/core";
import { ReadCartaoDto } from "src/app/dto/financas/cartoes/readCartaoDto";
import { Cartao } from "src/app/interfaces/financas/Cartao";
import { CreateCartaoDto } from "src/app/dto/financas/cartoes/createCartaoDto";

@Injectable({
  providedIn: "root",
})
export class CartaoMapper {

  constructor() { }

  public static CartaoDtoToCartao(ReadCartaoDto: ReadCartaoDto): Cartao {
    let cartao: Cartao = {
      Id: ReadCartaoDto.Id,
      Nome: ReadCartaoDto.Nome,
      DiaVencimento: ReadCartaoDto.DiaVencimento,
      DiaFechamento: ReadCartaoDto.DiaFechamento,
      Assinaturas: [],
    }
    return cartao;
  }

  public static CartaoToCreateCartaoDto(Cartao: Cartao): CreateCartaoDto {
    let CreateCartaoDto: CreateCartaoDto = {
      Nome: Cartao.Nome,
      DiaVencimento: Cartao.DiaVencimento,
      DiaFechamento: Cartao.DiaFechamento,
    }
    return CreateCartaoDto;
  }

  public static ReadDtoToCreateDto(ReadCartaoDto: ReadCartaoDto): CreateCartaoDto {
    let CreateCartaoDto: CreateCartaoDto = {
      Nome: ReadCartaoDto.Nome,
      DiaVencimento: ReadCartaoDto.DiaVencimento,
      DiaFechamento: ReadCartaoDto.DiaFechamento,
    }
    return CreateCartaoDto;
  }
}
