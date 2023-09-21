import { Categoria } from './../../../interfaces/categoria';

export interface ReadCompraDto {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DataCompra: Date;
  QtdParcelas: number;
  CartaoId: number;
  Categoria: Categoria;
}
