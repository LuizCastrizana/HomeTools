import { Compra } from './../../interfaces/financas/Compra';
import { Injectable } from "@angular/core";
import { ReadCompraDto } from "src/app/dto/financas/cartoes/readCompraDto";


@Injectable({
  providedIn: "root",
})

export class CompraMapper {

  constructor() { }

  public static CompraDtoToCompra(ReadCompraDto: ReadCompraDto): Compra {
    let compra: Compra = {
      Id: ReadCompraDto.Id,
      Descricao: ReadCompraDto.Descricao,
      ValorInteiro: ReadCompraDto.ValorInteiro,
      ValorCentavos: ReadCompraDto.ValorCentavos,
      DataCompra: ReadCompraDto.DataCompra,
      QtdParcelas: ReadCompraDto.QtdParcelas,
      CartaoId: ReadCompraDto.CartaoId,
      Categoria: ReadCompraDto.Categoria,
    }
    return compra;
  }
}
